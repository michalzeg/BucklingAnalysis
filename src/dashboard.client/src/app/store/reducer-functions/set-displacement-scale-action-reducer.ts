import { AppState } from "../state";


export const setDisplacementScaleActionReducer = (state: AppState, increase: number): AppState => {

  const newDisplacement = state.view3DState.displacementScale <= 1 && increase === -1
    ? state.view3DState.displacementScale
    : state.view3DState.displacementScale + increase

  return <AppState>({
    ...state,
    view3DState:
    {
      ...state.view3DState,
      displacementScale: newDisplacement
    }
  });
}
