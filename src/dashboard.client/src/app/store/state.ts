import { ActivityProgress, create } from "../shared/models/activity-progress";
import { CalculixProgress } from "../shared/models/calculix-progress";
import { Facade } from "../shared/models/facade";
import { Geometry } from "../shared/models/geometry";
import { CalculationResults } from "../shared/models/results/calculation-results";
import { AnalysisType } from "../shared/types/analysis-type";
import { StressType } from "../shared/types/stress-type";

export interface AppState {
  facade: Facade | null;
  linearAnalysisResults: CalculationResults | null;
  bucklingAnalysisResults: CalculationResults | null;
  nonLinearAnalysisResults: CalculationResults | null;
  stressType: StressType;
  analysisType: AnalysisType;
  activityProgress: Array<ActivityProgress>;
  trackingNumber: string;
  calculixProgress: CalculixProgress[];

  view3DState: View3DState;
  geometry: Geometry;
}


export interface View3DState {
  meshVisible: boolean;
  displacementScale: number;
}

export const initialState: AppState = {
  facade: null,
  linearAnalysisResults: null,
  bucklingAnalysisResults: null,
  nonLinearAnalysisResults: null,
  stressType: 'sxx',
  analysisType: 'linear',
  activityProgress: create(),
  trackingNumber: '',
  calculixProgress: [],

  view3DState: {
    meshVisible: true,
    displacementScale: 2
  },

  geometry: <Geometry>{
    flangeThickness: 10,
    webThickness: 10,
    height: 200,
    width: 200,
    length: 2000,
    uniformLoad: -100,
    imperfection: 30
  }
};
