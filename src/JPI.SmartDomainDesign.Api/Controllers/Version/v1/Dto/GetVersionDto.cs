namespace JPI.SmartDomainDesign.Api.Controllers.Version.v1.Dto;

public sealed class GetVersionDto
{
    public required string Version { get; init; }

    public required string ApplicationName { get; init; }

    public string? BuildDate { get; init; }

    public required string CurrentDate { get; init; }

    public string? Environment {  get; init; }
}
