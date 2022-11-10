using Microsoft.Extensions.CommandLineUtils;

namespace Migration.CLI
{
    public interface IMigrationCommandResolver
    {
        void Resolve(CommandLineApplication application);
    }
}
