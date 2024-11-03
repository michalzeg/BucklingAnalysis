import { Injectable } from '@angular/core';
import { Lut } from "three/examples/jsm/math/Lut";
import * as THREE from 'three';


const colorMap = "rainbow";
const numberOfColors = 100;
const basicColor = new THREE.Color(66 / 255, 161 / 255, 245 / 255);

@Injectable({
  providedIn: 'root'
})
export class ColorService {

  private lut = new Lut(colorMap, numberOfColors);

  constructor() { }

  setExtremes(min: number, max: number): void {

    this.lut.setMin(min);
    this.lut.setMax(max);
  }

  getColor(value: number): THREE.Color {

    if (value === 0)
      return basicColor;

    return this.lut.getColor(value);
  }

  getBasicColor(): THREE.Color {
    return basicColor;
  }

  getColorRange(segments: number = 10): string[] {

    const range = (this.lut.maxV - this.lut.minV) / segments;
    const min = this.lut.minV;

    const result = new Array(segments + 1).fill(0)
      .map((e, i) => min + i * range)
      .map(e => this.lut.getColor(e))
      .map(e => e?.getHexString())
      .map(e => `#${e}`);

    return result;
  }

}
