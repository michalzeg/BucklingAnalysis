import { ExtremeValues } from "../models/extreme-values";
import { CalculationResults } from "../models/results/calculation-results";
import { AnalysisType } from "../types/analysis-type";
import { StressType, getStatisticsTypeSelector } from "../types/stress-type";

export const getExtremeValues = (stressType: StressType, analysis: AnalysisType, results: CalculationResults): ExtremeValues => {
  const selector = getStatisticsTypeSelector(stressType);

  const max = selector(results).percentile095;
  const min = selector(results).percentile005;

  return <ExtremeValues>{
    type: analysis,
    max,
    min
  };
}
