using Microsoft.Extensions.CommandLineUtils;
using System.Reflection;

namespace Migration.CLI.Extensions
{
    public static class CommandLineApplicationExtensions
    {
        public static CommandOption HelpOption(this CommandLineApplication app)
           => app.HelpOption("-?|-h|--help");

        public static void VersionOptionFromAssemblyAttributes(this CommandLineApplication app, Assembly assembly)
           => app.VersionOption("--version", GetInformationalVersion(assembly));

        private static string GetInformationalVersion(Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            var versionAttribute = attribute == null
                ? assembly.GetName().Version?.ToString() ?? "1.0.0"
                : attribute.InformationalVersion;

            return versionAttribute;
        }
    }
}
