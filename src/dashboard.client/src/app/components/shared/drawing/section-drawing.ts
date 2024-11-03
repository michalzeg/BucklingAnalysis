
import { DrawingBase } from "./drawing-base";
import { SectionDrawingSettings } from "./drawing-settings";
import { Point } from "./point";
import { Section } from "./section";


export class SectionDrawing extends DrawingBase {

  protected drawingCentre!: Point;
  protected drawingHeight!: number;
  protected drawingWidth!: number;


  private section!: Section;

  constructor(canvasId: string) {
    super(canvasId, SectionDrawingSettings);
  }

  draw(section: Section): void {
    this.section = section;
    this.drawingCentre = this.section.getCentre();
    this.drawingHeight = this.section.getHeight();
    this.drawingWidth = this.section.getWidth();
    this.reset();

    this.drawPolygon(this.section.getCoordinates());
  }
}
