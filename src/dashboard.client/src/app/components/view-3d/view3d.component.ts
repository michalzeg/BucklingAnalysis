import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';
import { combineLatest, filter, map, takeUntil, tap, withLatestFrom } from 'rxjs';
import { View3dService } from '../../services/view3d.service';
import { Store } from '@ngrx/store';
import { selectLinearAnalysisResults, selectFacade, selectStressType, selectMeshVisible, selectDisplacementScale, selectTrackingNumber, selectAnalysisType, selectNonLinearAnalysisResults, selectBucklingAnalysisResults } from '../../store/selectors';
import { DestroyableComponent } from '../shared/destroyable-component';


const backgroundColor = 'white';
@Component({
  selector: 'app-view3d',
  templateUrl: './view3d.component.html',
  styleUrls: ['./view3d.component.scss'],
  //changeDetection: ChangeDetectionStrategy.OnPush
})
export class View3dComponent extends DestroyableComponent implements OnInit, AfterViewInit {
  @ViewChild('canvas') canvasRef!: ElementRef;

  private scene!: THREE.Scene;
  private camera!: THREE.PerspectiveCamera;
  private renderer!: THREE.WebGLRenderer;
  private orbitControls!: OrbitControls;

  constructor(private readonly store: Store, private readonly view3DService: View3dService) { super(); }
  ngAfterViewInit(): void {
    this.initThreeJs();
    this.animate();
  }

  ngOnInit(): void {
    this.store.select(selectTrackingNumber).pipe(
      takeUntil(this.destroyed$),
      filter(e => e === ''),
      tap(() => this.view3DService.clean())
    ).subscribe();

    this.store.select(selectFacade).pipe(
      takeUntil(this.destroyed$),
      tap(e => this.view3DService.createFacade(e)),
      tap(() => this.view3DService.createWireframe())
    ).subscribe();


    combineLatest([
      this.store.select(selectStressType),
      this.store.select(selectAnalysisType),
      this.store.select(selectDisplacementScale),
      this.store.select(selectLinearAnalysisResults),
      this.store.select(selectBucklingAnalysisResults),
      this.store.select(selectNonLinearAnalysisResults)
    ]).pipe(
      takeUntil(this.destroyed$),
      map(e => ({ stressType: e[0], analysisType: e[1], scale: e[2] })),
      withLatestFrom(
        this.store.select(selectLinearAnalysisResults),
        this.store.select(selectBucklingAnalysisResults),
        this.store.select(selectNonLinearAnalysisResults)
      ),
      map(([types, linear, buckling, nonLinear]) => {
        const result = types.analysisType === 'linear' ? linear : types.analysisType === 'buckling' ? buckling : nonLinear;
        return {
          result,
          stressType: types.stressType,
          scale: types.scale
        };
      }),
      tap(e => this.view3DService.generateResults(e.result, e.stressType, e.scale))
    ).subscribe();


    this.store.select(selectMeshVisible).pipe(
      takeUntil(this.destroyed$),
      tap(e => this.view3DService.toggleWireframe(e))
    ).subscribe();


  }

  private initThreeJs(): void {
    this.scene = new THREE.Scene();

    const canvasElement = this.canvasRef.nativeElement as HTMLElement;
    const widthHeightFactor = 0.75;

    const width = canvasElement.clientWidth;
    const height = widthHeightFactor * canvasElement.clientWidth;
    this.camera = new THREE.PerspectiveCamera(75, width / height, 1, 1000_000);
    this.camera.position.z = 1000;
    this.camera.position.x = -2000;
    this.camera.position.y = 2000;


    this.renderer = new THREE.WebGLRenderer();
    this.scene.background = new THREE.Color(backgroundColor);
    this.renderer.setSize(width, height);
    this.canvasRef.nativeElement.appendChild(this.renderer.domElement);

    const axesHelper = new THREE.AxesHelper(500); this.scene.add(axesHelper);

    this.orbitControls = new OrbitControls(this.camera, this.renderer.domElement);
    this.view3DService.injectScene(this.scene);
  }


  private animate = () => {
    requestAnimationFrame(this.animate);

    this.orbitControls.update();
    this.renderer.render(this.scene, this.camera);
  }
}
