import { ActivityProgressState } from "../types/activity-progress-state";
import { ActivityType, activityTypes, normalizeActivityName } from "../types/activity-type";

export interface ActivityProgress {
  type: ActivityType;
  state: ActivityProgressState;
  displayName: string;
}

export const create = (): Array<ActivityProgress> => activityTypes.map(e => <ActivityProgress>({ type: e, state: 'waiting', displayName: normalizeActivityName(e) }));
