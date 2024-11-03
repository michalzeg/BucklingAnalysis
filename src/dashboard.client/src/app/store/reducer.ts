import { createReducer, on } from '@ngrx/store';
import * as actions from './actions';
import { AppState, initialState } from './state';
import { setActivityCompletedActionReducer } from './reducer-functions/set-activity-completed-action-reducer';
import { setDisplacementScaleActionReducer } from './reducer-functions/set-displacement-scale-action-reducer';
import { setActivityState } from './reducer-functions/set-activity-state';

export type AppStateReducer = (state: AppState) => AppState;


export const appReducer = createReducer(
  initialState,
  on(actions.setGeometry, (state, { geometry }) => ({ ...state, geometry })),

  on(actions.startCalculations, (state) => ({ ...state, ...initialState, geometry: state.geometry })),
  on(actions.stressTypeChanged, (state, { stressType }) => ({ ...state, stressType })),
  on(actions.analysisTypeChanged, (state, { analysisType }) => ({ ...state, analysisType })),
  on(actions.downloadFacade, (state) => ({ ...state, activityProgress: setActivityState(state.activityProgress, 'GenerateFacadeActivity', 'downloading') })),
  on(actions.setFacade, (state, { facade }) => ({ ...state, facade, activityProgress: setActivityState(state.activityProgress, 'GenerateFacadeActivity', 'done') })),
  on(actions.downloadLinearAnalysisResults, (state) => ({ ...state, activityProgress: setActivityState(state.activityProgress, 'FilterLinearAnalysisResultsActivity', 'downloading') })),
  on(actions.setLinearAnalysisResults, (state, { results }) => ({ ...state, linearAnalysisResults: results, activityProgress: setActivityState(state.activityProgress, 'FilterLinearAnalysisResultsActivity', 'done') })),
  on(actions.downloadBucklingAnalysisResults, (state) => ({ ...state, activityProgress: setActivityState(state.activityProgress, 'FilterBucklingAnalysisResultsActivity', 'downloading') })),
  on(actions.setBucklingAnalysisResults, (state, { results }) => ({ ...state, bucklingAnalysisResults: results, activityProgress: setActivityState(state.activityProgress, 'FilterBucklingAnalysisResultsActivity', 'done') })),
  on(actions.downloadNonLinearAnalysisResults, (state) => ({ ...state, activityProgress: setActivityState(state.activityProgress, 'FilterNonLinearAnalysisResultsActivity', 'downloading') })),
  on(actions.setNonLinearAnalysisResults, (state, { results }) => ({ ...state, nonLinearAnalysisResults: results, activityProgress: setActivityState(state.activityProgress, 'FilterNonLinearAnalysisResultsActivity', 'done') })),

  on(actions.setActivityCompleted, (state, { activityType }) => setActivityCompletedActionReducer(state, activityType)),
  on(actions.setTrackingNumber, (state, { trackingNumber }) => ({ ...state, trackingNumber })),
  on(actions.setCalculixProgess, (state, { progress }) => ({ ...state, calculixProgress: [...state.calculixProgress, progress] })),


  on(actions.setMeshVisible, (state, { visible }) => ({ ...state, view3DState: { ...state.view3DState, meshVisible: visible } })),
  on(actions.setDisplacementScale, (state, { increase }) => setDisplacementScaleActionReducer(state, increase)),

);
