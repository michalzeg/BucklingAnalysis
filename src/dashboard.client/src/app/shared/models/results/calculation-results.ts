import { Statistics } from "./statistics"
import { TriangleResult } from "./triangle-result"

export interface CalculationResults {
  triangleResults: TriangleResult[]
  sxx: Statistics
  syy: Statistics
  szz: Statistics
  sxy: Statistics
  sxz: Statistics
  syz: Statistics
  maxDisplacement: number;
}


