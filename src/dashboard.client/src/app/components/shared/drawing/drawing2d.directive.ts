import { Directive, ElementRef } from '@angular/core';
import { v4 as uuidv4 } from 'uuid';

@Directive({
  selector: '[appDrawing2d]'
})
export class Drawing2dDirective {

  constructor(private elementRef: ElementRef) { }

  public getCanvasId(): string {
    const canvasObject = this.elementRef;
    canvasObject.nativeElement.id = uuidv4();

    return canvasObject.nativeElement.id.toString();
  }

}
