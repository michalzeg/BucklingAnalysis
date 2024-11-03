using Infrastructure.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared;
using Shared.Storage;
using System.Net.Http.Headers;

namespace Infrastructure.Storage;

public class WebStorage : StorageBase
{
    private readonly ILogger<WebStorage> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public WebStorage(ILogger<WebStorage> logger, IConfiguration configuration, HttpClient httpClient)
    {
        _logger = logger;
        _url = configuration["WEB_STORAGE_URL"] ?? throw new ArgumentNullException("WEB_STORAGE_URL");
        _httpClient = httpClient;
    }

    public override async Task<Stream> GetStreamAsync(string name, Guid trackingNumber = default)
    {
        var fullName = GetFullName(trackingNumber, name);
        var response = await _httpClient.GetAsync($"http://{_url}/Storage/{fullName}", HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync();
    }

    public override async Task<T> GetAsync<T>(string name, Guid trackingNumber = default)
    {
        var fullName = GetFullName(trackingNumber, name);
        var response = await _httpClient.GetAsync($"http://{_url}/Storage/{fullName}", HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
       
        await using var ms = await response.Content.ReadAsStreamAsync();
        using var sw = new StreamReader(ms);
        await using var writer = new JsonTextReader(sw);
        var result = JsonUtils.JsonSerializer.Deserialize<T>(writer);
        return result;
    }

    public override async Task SetAsync<T>(string name, T obj, Guid trackingNumber = default)
    {
        var fullName = GetFullName(trackingNumber, name);
        
        await using var ms = new MemoryStream();
        await using var sw = new StreamWriter(ms);
        await using var writer = new JsonTextWriter(sw);
        JsonUtils.JsonSerializer.Serialize(writer, obj);
        await sw.FlushAsync();

        ms.Seek(0, SeekOrigin.Begin);
        using var content = new StreamContent(ms);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        var res = await _httpClient.PutAsync($"http://{_url}/Storage/{fullName}", content);

        res.EnsureSuccessStatusCode();
    }

    public override Task<T> GetAsync<T>(ActivityArgumentType name, Guid trackingNumber = default) => GetAsync<T>(name.ToString(), trackingNumber);
    public override Task SetAsync<T>(ActivityArgumentType name, T obj, Guid trackingNumber = default) => SetAsync<T>(name.ToString(), obj, trackingNumber);
}
