import { ActivityProgress } from "../../shared/models/activity-progress";
import { ActivityProgressState } from "../../shared/types/activity-progress-state";
import { ActivityType } from "../../shared/types/activity-type";

export const setActivityState = (activityProgress: Array<ActivityProgress>, activityType: ActivityType, newProgressState: ActivityProgressState): Array<ActivityProgress> => {

  const result = activityProgress.map(e => e.type === activityType ? ({ ...e, state: newProgressState }) : e)

  return result;
}
