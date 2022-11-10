using Microsoft.Extensions.CommandLineUtils;
using Migration.CLI.Extensions;
using Migration.CLI.Utils;

namespace Migration.CLI.Command.BpkSetMRemovalItemCommand
{
    internal class BpkFileTemplateCommand : IMigrationCommand
    {
        public string Name => "BpkFileTemplate";

        public void Execute(CommandLineApplication command)
        {
            command.Description = "生成Bpk代码文件模板";

            command.HelpOption();

            command.OnExecute(() =>
            {
                string dirPath = $@"c:\BpkFileTemplate_{DateTime.Now.ToString("yyyyMMddHHmmssffff")}";

                Directory.CreateDirectory(dirPath);

                File.Create($@"{dirPath}\{BpkFileDescription.lowCode1Form}");

                ConsoleUtils.WriteLine($@"生成{dirPath}\{BpkFileDescription.lowCode1Form}文件,请填写低代码1.0表单设计Json数据", ConsoleColor.Green);

                File.Create($@"{dirPath}\{BpkFileDescription.lowCode2Form}");

                ConsoleUtils.WriteLine($@"生成{dirPath}\{BpkFileDescription.lowCode2Form}文件,请填写低代码2.0表单设计Json数据", ConsoleColor.Green);

                File.Create($@"{dirPath}\{BpkFileDescription.lowCode1Code}");

                ConsoleUtils.WriteLine($@"生成{dirPath}\{BpkFileDescription.lowCode1Code}文件,请填写低代码1.0的代码数据", ConsoleColor.Green);

                return 0;
            });
        }
    }
}
