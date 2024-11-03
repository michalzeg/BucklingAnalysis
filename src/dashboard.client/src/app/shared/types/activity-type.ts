import { humanize } from "../utils/humanize";

export type ActivityType = 'TriangulateActivity'
  | 'GenerateStructureActivity'
  | 'GenerateFacadeActivity'
  | 'GenerateLinearAnalysisCalculixInputActivity'
  | 'RunLinearAnalysisActivity'
  | 'ParseLinearAnalysisResultsActivity'
  | 'FilterLinearAnalysisResultsActivity'
  | 'GenerateBucklingAnalysisCalculixInputActivity'
  | 'RunBucklingAnalysisActivity'
  | 'ParseBucklingAnalysisResultsActivity'
  | 'FilterBucklingAnalysisResultsActivity'
  | 'NormalizeBucklingShapeActivity'
  | 'GenerateImperfectionsActivity'
  | 'GenerateNonLinearAnalysisCalculixInputActivity'
  | 'RunNonLinearAnalysisActivity'
  | 'ParseNonLinearAnalysisResultsActivity'
  | 'FilterNonLinearAnalysisResultsActivity'
  ;

export const activityTypes: Array<ActivityType> = [
  'TriangulateActivity',
  'GenerateStructureActivity',
  'GenerateFacadeActivity',
  'GenerateLinearAnalysisCalculixInputActivity',
  'RunLinearAnalysisActivity',
  'ParseLinearAnalysisResultsActivity',
  'FilterLinearAnalysisResultsActivity',
  'GenerateBucklingAnalysisCalculixInputActivity',
  'RunBucklingAnalysisActivity',
  'ParseBucklingAnalysisResultsActivity',
  'FilterBucklingAnalysisResultsActivity',
  'NormalizeBucklingShapeActivity',
  'GenerateImperfectionsActivity',
  'GenerateNonLinearAnalysisCalculixInputActivity',
  'RunNonLinearAnalysisActivity',
  'ParseNonLinearAnalysisResultsActivity',
  'FilterNonLinearAnalysisResultsActivity'

];

export const normalizeActivityName = (value: ActivityType): string => humanize(value.replace('Activity', '')).trim();

export const getNormalizedActivityTypes = (): Array<string> => activityTypes.map(e => normalizeActivityName(e));
