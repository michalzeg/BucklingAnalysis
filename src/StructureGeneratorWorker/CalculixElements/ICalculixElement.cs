using Shared.Contracts;
namespace StructureGeneratorWorker.CalculixElements;
public interface ICalculixElement
{
    PointD Center();
    PointD[] GetVertexCoordinates();
    CalculixElementFace[] GetCalculixElementFaces();
}