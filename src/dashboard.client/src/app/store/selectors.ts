import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AppState } from './state';


export const selectFeature = createFeatureSelector<AppState>('app');

export const selectGeometry = createSelector(selectFeature, (state: AppState) => state.geometry);

export const selectStressType = createSelector(selectFeature, (state: AppState) => state.stressType);
export const selectAnalysisType = createSelector(selectFeature, (state: AppState) => state.analysisType);

export const selectFacade = createSelector(selectFeature, (state: AppState) => state.facade);
export const selectLinearAnalysisResults = createSelector(selectFeature, (state: AppState) => state.linearAnalysisResults);
export const selectBucklingAnalysisResults = createSelector(selectFeature, (state: AppState) => state.bucklingAnalysisResults);
export const selectNonLinearAnalysisResults = createSelector(selectFeature, (state: AppState) => state.nonLinearAnalysisResults);

export const selectActivityProgress = createSelector(selectFeature, (state: AppState) => state.activityProgress);
export const selectTrackingNumber = createSelector(selectFeature, (state: AppState) => state.trackingNumber);
export const selectCalculixLinearAnalysisProgress = createSelector(selectFeature, (state: AppState) => state.calculixProgress);


export const selectMeshVisible = createSelector(selectFeature, (state: AppState) => state.view3DState.meshVisible);
export const selectDisplacementScale = createSelector(selectFeature, (state: AppState) => state.view3DState.displacementScale);
