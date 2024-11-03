import { drawBackgroundPattern } from './background-pattern';
import * as  SVG from 'svg.js';


export interface DrawingSettings {
  widthHeightRatio: number;
  scaleFactor: number;

  polygonFillColor: string;
  polygonFillOpacity: number;
  polygonStrokeColor: string;
  polygonStrokeWidth: number;

  lineStrokeWidth: number;
  lineColor: string;

  backgroundDrawingFunction: (canvas: SVG.Doc) => void;
}

export const SectionDrawingSettings: DrawingSettings = {
  widthHeightRatio: 1.3,
  scaleFactor: 1.5,

  polygonFillColor: '#3276b1',
  polygonFillOpacity: 0.9,
  polygonStrokeColor: '#054072',
  polygonStrokeWidth: 3,

  lineColor: '#f06',
  lineStrokeWidth: 1,

  backgroundDrawingFunction: drawBackgroundPattern
};


