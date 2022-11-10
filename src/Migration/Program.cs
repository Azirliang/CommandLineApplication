using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migration.CLI.Command.BpkSetMRemovalItemCommand;
using Migration.CLI.Data;
using Migration.CLI.Utils;
using System.Data.SqlClient;

namespace Migration.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Program().Run(args);
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public Program()
        {
            _serviceProvider = ConfigureServices();
            _configuration = ConfigurationBuild();
        }

        private int Run(string[] args)
        {
            try
            {
                var app = _serviceProvider.GetRequiredService<CommandLineApplication>();
                return app.Execute(args);
            }
            catch (Exception e)
            {
                ConsoleUtils.WriteLine($"异常. {e.Message}", ConsoleColor.Red);
                return 1;
            }
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //因为是exe文件，用户能够看到配置文件，所以数据库连接就不写到配置文件里了。
            //理论上该程序的数据的访问或者回写，客户端通过配置OAUTH2.0信息来访问服务器上的RPC或者HTTP服务。
            string connectionString = "";

            services.AddSingleton(new DapperDbAdapter(() => new SqlConnection(connectionString)));

            services.AddSingleton<CommandLineApplicationFactory>();

            services.AddSingleton(p => p.GetRequiredService<CommandLineApplicationFactory>().Create());

            services.AddSingleton<IMigrationCommand, BpkCodeGenerateCommand>();

            services.AddSingleton<IMigrationCommand, BpkFileTemplateCommand>();

            services.AddSingleton<IMigrationCommandResolver, MigrationCommandResolver>();

            return services.BuildServiceProvider();
        }

        private IConfiguration ConfigurationBuild()
        {
            var build = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables();

            return build.Build();
        }
    }
}

