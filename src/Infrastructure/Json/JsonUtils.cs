using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Json;

public static class JsonUtils
{
    public static JsonSerializerSettings JsonSerializerSettings => new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Converters = Converters
    };

    public static JsonSerializer JsonSerializer => JsonSerializer.CreateDefault(JsonSerializerSettings);

    public static JsonConverter[] Converters => [new ElementIdConverter(), new NodeIdConverter(), new StringEnumConverter()];

    public static string ToJson<T>(this T value)
    {
        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = Converters
        };
        return JsonConvert.SerializeObject(value, Formatting.Indented, serializerSettings);
    }

    public static T? FromJson<T>(this string value)
    {
        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = Converters
        };
        return JsonConvert.DeserializeObject<T>(value, serializerSettings);
    }
}