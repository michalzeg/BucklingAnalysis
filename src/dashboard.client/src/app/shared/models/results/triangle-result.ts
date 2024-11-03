import { VertexDisplacements } from "./vertex-displacements";
import { VertexStresses } from "./vertex-stresses";


export interface TriangleResult {
  vertex1Displacements: VertexDisplacements;
  vertex2Displacements: VertexDisplacements;
  vertex3Displacements: VertexDisplacements;
  vertex1Stresses: VertexStresses;
  vertex2Stresses: VertexStresses;
  vertex3Stresses: VertexStresses;
}
