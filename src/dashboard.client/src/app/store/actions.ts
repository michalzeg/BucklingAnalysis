import { createAction, props } from '@ngrx/store';
import { StressType } from '../shared/types/stress-type';
import { Facade } from '../shared/models/facade';
import { CalculationResults } from '../shared/models/results/calculation-results';
import { ActivityType } from '../shared/types/activity-type';
import { CalculixProgress } from '../shared/models/calculix-progress';
import { AnalysisType } from '../shared/types/analysis-type';
import { Geometry } from '../shared/models/geometry';

export const emptyAction = createAction('Empty');
export const loadTrackingNumber = createAction('[App] load tracking number');

export const setGeometry = createAction('[Creator] set geoemtry', props<{ geometry: Geometry }>());

export const connectSignalR = createAction('[SignalR] connect');
export const reconnectSignalR = createAction('[SignalR] reconnect');

export const startCalculations = createAction('[SignalR] start calculations');
export const setTrackingNumber = createAction('[SignalR] set tracking number', props<{ trackingNumber: string }>());

export const setCalculixProgess = createAction('[SignalR] set calculux progress', props<{ progress: CalculixProgress }>());
export const setActivityCompleted = createAction('[SignalR] set activity completed', props<{ activityType: ActivityType }>());


export const downloadFacade = createAction('[Http] download facade');
export const downloadLinearAnalysisResults = createAction('[Http] download linear analysis results');
export const downloadBucklingAnalysisResults = createAction('[Http] download buckling analysis results');
export const downloadNonLinearAnalysisResults = createAction('[Http] download nonLinear analysis results');


export const setFacade = createAction('[SignalR] set facade', props<{ facade: Facade }>());
export const setLinearAnalysisResults = createAction('[SignalR] set linear analysis results', props<{ results: CalculationResults }>());
export const setBucklingAnalysisResults = createAction('[SignalR] set buckling analysis results', props<{ results: CalculationResults }>());
export const setNonLinearAnalysisResults = createAction('[SignalR] set non linear analysis results', props<{ results: CalculationResults }>());


export const stressTypeChanged = createAction('[View3D] Stress type changed', props<{ stressType: StressType }>());
export const analysisTypeChanged = createAction('[View3D] Analysis type changed', props<{ analysisType: AnalysisType }>());

export const setMeshVisible = createAction('[View3D] Mesh visible changed', props<{ visible: boolean }>());
export const setDisplacementScale = createAction('[View3D] Displacement scale changed', props<{ increase: number }>());



