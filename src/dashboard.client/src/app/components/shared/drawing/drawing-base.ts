import * as  SVG from 'svg.js';
import { CanvasHelper } from './canvas-helper';
import { DrawingSettings } from './drawing-settings';
import { Point } from './point';


export abstract class DrawingBase {
  private settings: DrawingSettings;

  private canvas: SVG.Doc;
  private canvasHelper: CanvasHelper;

  protected abstract drawingHeight: number;
  protected abstract drawingWidth: number;
  protected abstract drawingCentre: Point;

  constructor(canvasId: string, settings: DrawingSettings) {
    this.settings = settings;
    const canvasObject = document.getElementById(canvasId);
    this.canvasHelper = new CanvasHelper(canvasObject, settings.widthHeightRatio);
    this.canvas = SVG(canvasId);
    this.settings.backgroundDrawingFunction(this.canvas);
  }

  protected drawRectangle(bottomLeftPoint: Point, topRightPoint: Point): void {
    const p1 = bottomLeftPoint;
    const p2 = { x: topRightPoint.x, y: bottomLeftPoint.y };
    const p3 = topRightPoint;
    const p4 = { x: bottomLeftPoint.x, y: topRightPoint.y };

    this.drawLine(p1, p2);
    this.drawLine(p2, p3);
    this.drawLine(p3, p4);
    this.drawLine(p4, p1);
  }

  protected drawLine(startPoint: Point, endPoint: Point): void {
    const start = this.transferToCanvasSystem(startPoint);
    const end = this.transferToCanvasSystem(endPoint);

    this.canvas.line(start.x, start.y, end.x, end.y)
      .stroke({ width: this.settings.lineStrokeWidth, color: this.settings.lineColor });
  }
  protected drawPolygon(points: Array<Point>): void {
    const coordinates = points
      .map(e => this.transferToCanvasSystem(e))
      .map(e => e.x + ' ' + e.y)
      .reduce((a, b) => ' ' + a + ' ' + b)
      .trim();

    this.canvas.polygon(coordinates)
      .fill({ color: this.settings.polygonFillColor, opacity: this.settings.polygonFillOpacity })
      .stroke({ width: this.settings.polygonStrokeWidth, color: this.settings.polygonStrokeColor });
  }

  public reset(): void {
    this.clear();
    this.canvasHelper.refresh();
    this.settings.backgroundDrawingFunction(this.canvas);
  }

  public clear(): void {
    this.canvas.clear();
  }

  private transferToCanvasSystem(point: Point): Point {
    const scale = this.calculateScale();
    const x = ((point.x - this.drawingCentre.x) * scale) + this.canvasHelper.width / 2;
    const y = (-(point.y - this.drawingCentre.y) * scale) + this.canvasHelper.height / 2;
    return { x, y };
  }

  private calculateScale(): number {
    const scale1 = this.canvasHelper.height / this.drawingHeight / this.settings.scaleFactor;
    const scale2 = this.canvasHelper.width / this.drawingWidth / this.settings.scaleFactor;

    const scale = Math.min(scale1, scale2);
    return scale;
  }
}
