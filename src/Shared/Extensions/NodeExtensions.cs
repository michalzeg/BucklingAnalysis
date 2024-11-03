using Shared.Contracts;

namespace Shared.Extensions;

public static class NodeExtensions
{
    public static Node Move(this Node node, double dx, double dy, double dz) => new()
    {
        Id = node.Id,
        Coordinates = node.Coordinates.Move(dx, dy, dz)
    };

    public static Node Move(this Node node, int dx, int dy, int dz) => new()
    {
        Id = node.Id,
        Coordinates = node.Coordinates.Move(dx, dy, dz)
    };
}
