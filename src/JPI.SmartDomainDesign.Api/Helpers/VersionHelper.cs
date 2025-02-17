using System.Reflection;

namespace JPI.SmartDomainDesign.Api.Helpers;

internal static class VersionHelper
{
    private const string Default = "Unknown";

    public static string GetVersion() 
        => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? Default;
}
