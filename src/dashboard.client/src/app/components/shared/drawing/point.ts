export interface Point {
  x: number;
  y: number;
}
export const mirrorVertical = (point: Point): Point => {
  const result = {
    x: -point.x,
    y: point.y
  }
  return result;
}
