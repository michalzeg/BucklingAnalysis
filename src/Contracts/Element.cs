using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public record Element
    {
        public required ElementId Id { get; init; }
        public required ElementType ElementType { get; init; }
        public required IReadOnlyCollection<TriangularFace> Faces { get; set; } = [];
        public required IReadOnlyCollection<Node> Nodes { get; set; } = [];
    }
}
