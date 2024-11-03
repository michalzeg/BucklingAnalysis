import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { DestroyableComponent } from '../../../shared/destroyable-component';
import { Store } from '@ngrx/store';
import { takeUntil, tap, map, filter, } from 'rxjs';
import { Drawing2dDirective } from '../../../shared/drawing/drawing2d.directive';
import { Section } from '../../../shared/drawing/section';
import { SectionDrawing } from '../../../shared/drawing/section-drawing';
import { selectGeometry } from '../../../../store/selectors';

@Component({
  selector: 'app-drawing',
  templateUrl: './drawing.component.html',
  styleUrl: './drawing.component.scss'
})
export class DrawingComponent extends DestroyableComponent implements AfterViewInit {
  @ViewChild(Drawing2dDirective)
  drawing2dElement!: Drawing2dDirective;

  private sectionDrawing!: SectionDrawing;

  constructor(private readonly store: Store) {
    super();
  }

  ngAfterViewInit(): void {
    const canvasId = this.drawing2dElement.getCanvasId();
    this.sectionDrawing = new SectionDrawing(canvasId);

    this.store.select(selectGeometry).pipe(
      takeUntil(this.destroyed$),
      tap(() => this.sectionDrawing?.reset()),
      filter(e => !!e),
      map(e => new Section(e!.width, e!.flangeThickness, e!.webThickness, e!.height, e!.width, e!.flangeThickness)),
      tap(e => this.sectionDrawing?.draw(e))
    ).subscribe();
  }
}
