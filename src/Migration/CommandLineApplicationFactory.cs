using Microsoft.Extensions.CommandLineUtils;
using Migration.CLI.Extensions;

namespace Migration.CLI
{
    public class CommandLineApplicationFactory
    {
        private readonly IMigrationCommandResolver _migrationCommandResolver;

        public CommandLineApplicationFactory(IMigrationCommandResolver migrationCommandResolver)
        {
            _migrationCommandResolver = migrationCommandResolver;
        }

        public CommandLineApplication Create()
        {
            var app = new CommandLineApplication
            {
                Name = "migration",
                FullName = "数据迁移"
            };

            app.HelpOption();

            app.VersionOptionFromAssemblyAttributes(typeof(Program).Assembly);

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 2;
            });

            _migrationCommandResolver.Resolve(app);

            return app;
        }
    }
}
