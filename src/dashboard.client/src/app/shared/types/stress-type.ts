import { CalculationResults } from "../models/results/calculation-results";
import { Statistics } from "../models/results/statistics";
import { VertexStresses } from "../models/results/vertex-stresses";

export type StressType = 'sxx' | 'syy' | 'szz' | 'sxy' | 'sxz' | 'syz';


export const stressTypes: Array<StressType> = ['sxx', 'syy', 'szz', 'sxy', 'sxz', 'syz'];

export function getStressTypeSelector(type: StressType): (s: VertexStresses) => number {

  switch (type) {
    case 'sxx':
      return c => c.sxx;
    case 'syy':
      return c => c.syy;
    case 'szz':
      return c => c.szz;
    case 'sxy':
      return c => c.sxy;
    case 'sxz':
      return c => c.sxz;
    case 'syz':
      return c => c.syz;
    default:
      return c => c.sxx;
  }

}

export function getStatisticsTypeSelector(type: StressType): (s: CalculationResults) => Statistics {

  switch (type) {
    case 'sxx':
      return c => c.sxx;
    case 'syy':
      return c => c.syy;
    case 'szz':
      return c => c.szz;
    case 'sxy':
      return c => c.sxy;
    case 'sxz':
      return c => c.sxz;
    case 'syz':
      return c => c.syz;
    default:
      return c => c.sxx;
  }

}
