using Microsoft.Extensions.CommandLineUtils;

namespace Migration.CLI
{
    public interface IMigrationCommand
    {
        string Name { get; }

        void Execute(CommandLineApplication command);
    }
}
