import { ActivityProgress } from "../../shared/models/activity-progress";
import { ActivityType } from "../../shared/types/activity-type";
import { AppState } from "../state";

export const setActivityCompletedActionReducer = (state: AppState, activityType: ActivityType): AppState => {
  const updatedElementIndex = state.activityProgress.map(e => e.type).indexOf(activityType);

  const activityProgress: Array<ActivityProgress> = [];
  for (let index = 0; index < state.activityProgress.length; index++) {
    const element = state.activityProgress[index];
    if (element.type === activityType) {
      activityProgress.push(<ActivityProgress>({ ...element, state: 'done' }));
    }
    else if (index === updatedElementIndex + 1) {
      activityProgress.push(<ActivityProgress>({ ...element, state: 'ongoing' }));
    }
    else {
      activityProgress.push(element);
    }
  }

  return <AppState>({
    ...state,
    activityProgress
  });
}
