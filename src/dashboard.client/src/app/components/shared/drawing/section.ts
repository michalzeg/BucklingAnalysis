import { isEqual } from "../../../shared/utils/is-equal";
import { Point } from "./point";

export class Section {
  private coordinates: Point[];

  private bottomFlangeWidth: number;
  private bottomFlangeThickness: number;
  private webThickness: number;
  private webHeight: number;
  private topFlangeWidth: number;
  private topFlangeThickness: number;

  constructor(bottomFlangeWidth: number,
    bottomFlangeThickness: number,
    webThickness: number,
    webHeight: number,
    topFlangeWidth: number,
    topFlangeThickness: number) {
    this.bottomFlangeThickness = bottomFlangeThickness;
    this.bottomFlangeWidth = bottomFlangeWidth;
    this.webThickness = webThickness;
    this.webHeight = webHeight;
    this.topFlangeThickness = topFlangeThickness;
    this.topFlangeWidth = topFlangeWidth;

    this.coordinates = [
      { x: 0, y: 0 },
      { x: bottomFlangeWidth / 2, y: 0 },
      { x: bottomFlangeWidth / 2, y: bottomFlangeThickness },
      { x: webThickness / 2, y: bottomFlangeThickness },
      { x: webThickness / 2, y: bottomFlangeThickness + webHeight },
      { x: topFlangeWidth / 2, y: bottomFlangeThickness + webHeight },
      { x: topFlangeWidth / 2, y: bottomFlangeThickness + webHeight + topFlangeThickness },
      { x: -topFlangeWidth / 2, y: bottomFlangeThickness + webHeight + topFlangeThickness },
      { x: -topFlangeWidth / 2, y: bottomFlangeThickness + webHeight },
      { x: -webThickness / 2, y: bottomFlangeThickness + webHeight },
      { x: -webThickness / 2, y: bottomFlangeThickness },
      { x: -bottomFlangeWidth / 2, y: bottomFlangeThickness },
      { x: -bottomFlangeWidth / 2, y: 0 },
    ]
  }

  public getHeight(): number {
    const ys = this.getCoordinates().map(e => e.y);
    const max = Math.max(...ys);
    const min = Math.min(...ys);
    const height = max - min;
    return height;
  }

  public getWidth(): number {
    const xs = this.getCoordinates().map(e => e.x);
    const max = Math.max(...xs);
    const min = Math.min(...xs);
    const width = max - min;
    return width;
  }

  public getCentre(): Point {
    const height = this.getHeight();
    const width = this.getWidth();

    const xs = this.getCoordinates().map(e => e.x);
    const ys = this.getCoordinates().map(e => e.y);
    const minX = Math.min(...xs);
    const minY = Math.min(...ys);

    const x = minX + width / 2;
    const y = minY + height / 2;

    return { x, y };
  }

  public getBottomWidth(): number {
    const coordinates = this.getCoordinates();
    const sortedVertically = coordinates.sort((a, b) => a.y - b.y);
    const minY = coordinates[0].y;
    const bottomXs = sortedVertically.filter(e => isEqual(e.y, minY)).map(e => e.x);
    const maxX = Math.max(...bottomXs);
    const minX = Math.min(...bottomXs);

    const width = maxX - minX;
    return width;
  }

  public getCoordinates(): Array<Point> {
    const coord = this.coordinates.map(e => ({ x: e.x, y: e.y }));
    return coord;
  }

  public equals(other: Section): boolean {
    return isEqual(this.bottomFlangeThickness, other.bottomFlangeThickness)
      && isEqual(this.bottomFlangeWidth, other.bottomFlangeWidth)
      && isEqual(this.webThickness, other.webThickness)
      && isEqual(this.webHeight, other.webHeight)
      && isEqual(this.topFlangeThickness, other.topFlangeThickness)
      && isEqual(this.topFlangeWidth, other.topFlangeWidth);
  }
}
