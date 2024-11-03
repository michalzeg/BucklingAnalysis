using Newtonsoft.Json;
using Shared.Contracts;

namespace Infrastructure.Json;

public class ElementIdConverter : JsonConverter<ElementId>
{
    public override ElementId ReadJson(JsonReader reader, Type objectType, ElementId existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var res = (int)(long)reader.Value!;
        return new ElementId(res);
    }

    public override void WriteJson(JsonWriter writer, ElementId value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Value);
    }
}
