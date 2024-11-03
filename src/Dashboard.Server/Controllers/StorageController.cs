using Microsoft.AspNetCore.Mvc;
using Shared.Storage;
using Shared;
using System.Net;

namespace Dashboard.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StorageController : ControllerBase
{
    private readonly ILogger<StorageController> _logger;
    private readonly IStorage _storage;

    public StorageController(ILogger<StorageController> logger, IStorage storage)
    {
        _logger = logger;
        _storage = storage;
    }

    [HttpGet]
    [Route("{trackingNumber:Guid}/FacadeDetails")]
    public async Task GetFacadeDetails(Guid trackingNumber) => await CopyStreamToResponse(trackingNumber, ActivityArgumentType.FacadeDetails);


    [HttpGet]
    [Route("{trackingNumber:Guid}/FilteredLinearAnalysisResults")]
    public async Task GetFilteredLinearAnalysisResults(Guid trackingNumber) => await CopyStreamToResponse(trackingNumber, ActivityArgumentType.FilteredLinearAnalysisResults);

    [HttpGet]
    [Route("{trackingNumber:Guid}/FilteredNonLinearAnalysisResults")]
    public async Task GetFilteredNonLinearAnalysisResults(Guid trackingNumber) => await CopyStreamToResponse(trackingNumber, ActivityArgumentType.FilteredNonLinearAnalysisResults);

    [HttpGet]
    [Route("{trackingNumber:Guid}/BucklingAnalysisResults")]
    public async Task GetBucklingAnalysisResults(Guid trackingNumber) => await CopyStreamToResponse(trackingNumber, ActivityArgumentType.FilteredBucklingAnalysisResults);


    private async Task CopyStreamToResponse(Guid trackingNumber, ActivityArgumentType activity)
    {
        HttpContext.Response.ContentType = "application/json";
        HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        await using var result = await _storage.GetStreamAsync(activity.ToString(), trackingNumber);
        await result.CopyToAsync(HttpContext.Response.Body);
    }

}
