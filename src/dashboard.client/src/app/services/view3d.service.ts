import { Injectable } from '@angular/core';
import * as THREE from 'three';
import { getStatisticsTypeSelector, getStressTypeSelector, StressType } from '../shared/types/stress-type';
import { Facade } from '../shared/models/facade';
import { CalculationResults } from '../shared/models/results/calculation-results';
import { Point } from '../shared/models/point';
import { ColorService } from './color.service';

const wireframeColor = 0x808080;
const scaleFactor = 10;

@Injectable({
  providedIn: 'root'
})
export class View3dService {
  private scene!: THREE.Scene;

  private baseFacade: THREE.Mesh | null = null;
  private facade: THREE.Mesh | null = null;
  private wireframe: THREE.LineSegments<THREE.WireframeGeometry<THREE.BufferGeometry<THREE.NormalBufferAttributes>>, THREE.LineBasicMaterial, THREE.Object3DEventMap> | null = null;

  constructor(private readonly colorService: ColorService) { }

  public injectScene(scene: THREE.Scene) {
    this.scene = scene;
  }

  public clean() {
    if (this.wireframe) {
      this.scene.remove(this.wireframe);
      this.wireframe = null;
    }
    if (this.facade) {
      this.scene.remove(this.facade);
      this.facade = null;
      this.baseFacade = null;
    }
  }

  public createFacade(facade: Facade | null): void {
    if (!facade) {
      return;
    }

    const geometry = new THREE.BufferGeometry();
    const basicColor = this.colorService.getBasicColor();
    const nodeIds = facade?.nodes.map(e => e.id) ?? [];
    const nodeCoords = facade?.nodes.map(e => e.coordinates) ?? [];
    const nodeCoordinatesMap = new Map<number, Point>()

    for (let index = 0; index < nodeIds.length; index++) {
      nodeCoordinatesMap.set(nodeIds[index], nodeCoords[index]);
    }

    const vertices = [];
    const colors = [];

    for (let index = 0; index < facade.faces.length; index++) {
      const element = facade.faces[index];
      const v1 = nodeCoordinatesMap.get(element.node1);
      const v2 = nodeCoordinatesMap.get(element.node2);
      const v3 = nodeCoordinatesMap.get(element.node3);

      vertices.push(v1!.x, v1!.z, v1!.y)
      vertices.push(v2!.x, v2!.z, v2!.y)
      vertices.push(v3!.x, v3!.z, v3!.y)

      colors.push(basicColor.r, basicColor.g, basicColor.b);
      colors.push(basicColor.r, basicColor.g, basicColor.b);
      colors.push(basicColor.r, basicColor.g, basicColor.b);
    }

    const verticesArray = new Float32Array(vertices);
    const colorsArray = new Float32Array(colors);


    const positionAttribute = new THREE.BufferAttribute(verticesArray, 3);
    positionAttribute.setUsage(THREE.DynamicDrawUsage);
    geometry.setAttribute('position', positionAttribute);

    const colorAttribute = new THREE.BufferAttribute(colorsArray, 3);
    colorAttribute.setUsage(THREE.DynamicDrawUsage);
    geometry.setAttribute('color', colorAttribute);
    const material = new THREE.MeshBasicMaterial({ vertexColors: true, side: THREE.DoubleSide });

    this.facade = new THREE.Mesh(geometry, material);
    this.baseFacade = new THREE.Mesh(geometry.clone(), material.clone());
    this.scene.add(this.facade);
  }

  public createWireframe(): void {
    if (!this.facade) {
      return;
    }
    const materialLine = new THREE.LineBasicMaterial({ color: wireframeColor });
    const wireframeGeometry = new THREE.WireframeGeometry(this.facade.geometry);
    this.wireframe = new THREE.LineSegments(wireframeGeometry, materialLine);
    this.scene.add(this.wireframe);
  }

  public toggleWireframe(visible: boolean): void {
    if (this.wireframe && !visible) {
      this.scene.remove(this.wireframe);
      this.wireframe = null;
    } else if (visible) {
      this.createWireframe();
    }
  }


  public generateResults(results: CalculationResults | null, stressType: StressType, scale: number): void {
    if (!results) {
      return;
    }

    if (!this.facade || !this.baseFacade) {
      return;
    }

    const triangleResults = results!.triangleResults;
    const basePositions = [...this.baseFacade.geometry.attributes['position'].array];
    const colors = this.facade.geometry.attributes['color'].array;
    const positions = this.facade.geometry.attributes['position'].array;

    const statisticsSelector = getStatisticsTypeSelector(stressType);
    const max = statisticsSelector(results).percentile095
    const min = statisticsSelector(results).percentile005;
    this.colorService.setExtremes(min, max);

    const factor = 1 / results.maxDisplacement * scale * scaleFactor;

    const stressSelector = getStressTypeSelector(stressType);
    let counter = 0;
    for (let i = 0; i < triangleResults.length; i++) {
      const result = triangleResults[i];

      const c1 = this.colorService.getColor(stressSelector(result.vertex1Stresses));
      const c2 = this.colorService.getColor(stressSelector(result.vertex2Stresses));
      const c3 = this.colorService.getColor(stressSelector(result.vertex3Stresses));

      colors[counter + 0] = c1.r;    // Red component
      colors[counter + 1] = c1.g; // Green component
      colors[counter + 2] = c1.b; // Blue component

      colors[counter + 3] = c2.r;    // Red component
      colors[counter + 4] = c2.g; // Green component
      colors[counter + 5] = c2.b; // Blue component

      colors[counter + 6] = c3.r;    // Red component
      colors[counter + 7] = c3.g; // Green component
      colors[counter + 8] = c3.b; // Blue component

      positions[counter + 0] = basePositions[counter + 0] + result.vertex1Displacements.dx * factor;
      positions[counter + 1] = basePositions[counter + 1] + result.vertex1Displacements.dz * factor;
      positions[counter + 2] = basePositions[counter + 2] + result.vertex1Displacements.dy * factor;

      positions[counter + 3] = basePositions[counter + 3] + result.vertex2Displacements.dx * factor;
      positions[counter + 4] = basePositions[counter + 4] + result.vertex2Displacements.dz * factor;
      positions[counter + 5] = basePositions[counter + 5] + result.vertex2Displacements.dy * factor;

      positions[counter + 6] = basePositions[counter + 6] + result.vertex3Displacements.dx * factor;
      positions[counter + 7] = basePositions[counter + 7] + result.vertex3Displacements.dz * factor;
      positions[counter + 8] = basePositions[counter + 8] + result.vertex3Displacements.dy * factor;

      counter = counter + 9;
    }

    this.facade.geometry.attributes['color'].needsUpdate = true;
    this.facade.geometry.attributes['position'].needsUpdate = true;
    if (this.wireframe) {
      this.scene.remove(this.wireframe);
      this.createWireframe();
    }
  }

}
