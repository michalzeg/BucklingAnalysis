
export class CanvasHelper {
  private canvas: HTMLElement;
  public widthHeightRatio: number;

  public width!: number;
  public height!: number;

  constructor(canvas: HTMLElement | null, widthHeightRatio: number) {
    if (!canvas) {
      throw new Error("Empty canvas");
    }
    this.canvas = canvas;
    this.widthHeightRatio = widthHeightRatio;

    this.refresh();
  }

  public refresh(): void {
    this.canvas.setAttribute('style', 'height:' + (this.canvas.offsetWidth / this.widthHeightRatio).toFixed(0) + 'px');
    this.width = this.canvas.clientWidth;
    this.height = this.canvas.clientHeight;
  }
}
