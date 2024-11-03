using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public readonly record struct GeometryDescription
    {
        public required int FlangeThickness { get; init; }
        public required int WebThickness { get; init; }
        public required int Height { get; init; }
        public required int Width { get; init; }
        public required int Length { get; init; }
        public required double PointLoad { get; init; }

    }
}
