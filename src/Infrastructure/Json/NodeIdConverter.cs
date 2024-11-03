using Newtonsoft.Json;
using Shared.Contracts;

namespace Infrastructure.Json;

public class NodeIdConverter : JsonConverter<NodeId>
{
    public override NodeId ReadJson(JsonReader reader, Type objectType, NodeId existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var res = (int)(long)reader.Value!;
        return new NodeId(res);
    }

    public override void WriteJson(JsonWriter writer, NodeId value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Value);
    }
}
