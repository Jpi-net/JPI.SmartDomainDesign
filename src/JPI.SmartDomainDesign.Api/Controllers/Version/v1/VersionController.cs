using JPI.SmartDomainDesign.Api.Controllers.Abstractions;
using JPI.SmartDomainDesign.Api.Controllers.Version.v1.Dto;
using JPI.SmartDomainDesign.Api.Helpers;
using JPI.SmartDomainDesign.Application;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace JPI.SmartDomainDesign.Api.Controllers.Version.v1;

[Route("api/v1/version")]
public class VersionController
    : AppControllerBase
{
    public VersionController()
        : base()
    { 
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetVersionDto), StatusCodes.Status200OK)]
    public ActionResult<GetVersionDto> GetVersion()
        => Ok(new GetVersionDto()
        {
            Version = VersionHelper.GetVersion(),
            ApplicationName = ApplicationConstants.AppName,
            BuildDate = GetBuildDate(),
            CurrentDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
            Environment = GetEnvironment(),
        });

    private static string? GetBuildDate()
        => System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);

    private static string? GetEnvironment()
        => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
}
