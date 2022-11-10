using Microsoft.Extensions.CommandLineUtils;

namespace Migration.CLI
{
    public class MigrationCommandResolver : IMigrationCommandResolver
    {
        private readonly IEnumerable<IMigrationCommand> _migrationCommands;

        public MigrationCommandResolver(IEnumerable<IMigrationCommand> migrationCommands)
        {
            _migrationCommands = migrationCommands;
        }

        public void Resolve(CommandLineApplication application)
        {
            foreach (var command in _migrationCommands)
            {
                application.Command(command.Name, command.Execute);
            }
        }
    }
}
