using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebStorage.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController : ControllerBase
{
    private readonly ILogger<StorageController> _logger;

    public StorageController(ILogger<StorageController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{name}")]
    public async Task GetFileStream(string name)
    {
        var fileName = $"/storage/{name}";
        HttpContext.Response.ContentType = "application/json";
        HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        await using var fileStream = new FileStream(fileName, FileMode.Open,FileAccess.Read);

        await fileStream.CopyToAsync(HttpContext.Response.Body);
    }

    [HttpPut]
    [Route("{name}")]
    public async Task PutFileStream(string name)
    {
        HttpContext.Request.EnableBuffering();

        HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
        await using var fileStream = new FileStream($"/storage/{name}", FileMode.OpenOrCreate, FileAccess.Write);
        HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
        await HttpContext.Request.Body.CopyToAsync(fileStream);
    }

    [HttpGet]
    [Route("files")]
    public IActionResult GetFiles()
    {
        var result = Directory.GetFiles("/storage", "*.*", SearchOption.AllDirectories).ToArray();
        return Ok(result);
    }
}
