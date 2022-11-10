using Microsoft.Extensions.CommandLineUtils;
using Migration.CLI.Command.BpkSetMRemovalItemCommand.JsonData;
using Migration.CLI.Extensions;
using Migration.CLI.Utils;
using System.Text.Json;
using System.Text;

namespace Migration.CLI.Command.BpkSetMRemovalItemCommand
{
    public class BpkCodeGenerateCommand : IMigrationCommand
    {
        public string Name => "BpkCodeGenerate";

        public void Execute(CommandLineApplication command)
        {
            command.Description = "生成低代码2.0中流程节点含有Bpk作业票M栏填充需要撤除的D栏安全措施子表公式的代码";

            command.HelpOption();

            var directoryPath = command.Option("-d|--dir-path", "[必填] 含有BpkFileTemplate相关的三个Json文件的目录,或者使用BpkFileTemplate生成含有三个空Json文件的目录", CommandOptionType.SingleValue);

            command.OnExecute(() =>
            {
                if (string.IsNullOrEmpty(directoryPath.Value()))
                {
                    command.ShowHelp();

                    return 1;
                }
                else
                {
                    ConsoleUtils.WriteLine($"dirPath:{directoryPath.Value()}", ConsoleColor.Green);

                    try
                    {
                        GeneratorLowCode2Code(directoryPath.Value());
                    }
                    catch (Exception ex)
                    {
                        ConsoleUtils.Error(ex.Message);
                    }

                    return 0;
                }
            });
        }

        private void GeneratorLowCode2Code(string dirPath)
        {
            string lowCode1CodeFilePath = $@"{dirPath}\{BpkFileDescription.lowCode1Code}";

            string lowCode1FormFilPath = $@"{dirPath}\{BpkFileDescription.lowCode1Form}";

            string lowCode2FormFilePath = $@"{dirPath}\{BpkFileDescription.lowCode2Form}";

            string lowCode1CodeData = FileUtils.ReadFile(lowCode1CodeFilePath);

            string lowCode1FormData = FileUtils.ReadFile(lowCode1FormFilPath);

            string lowCode2FormData = FileUtils.ReadFile(lowCode2FormFilePath);

            if (string.IsNullOrEmpty(lowCode1CodeData))
            {
                throw new Exception($"文件:{lowCode1CodeFilePath}内容为空");
            }

            if (string.IsNullOrEmpty(lowCode1FormData))
            {
                throw new Exception($"文件:{lowCode1FormFilPath}内容为空");
            }

            if (string.IsNullOrEmpty(lowCode2FormData))
            {
                throw new Exception($"文件:{lowCode2FormFilePath}内容为空");
            }

            LowCodeFormDesign? lowCode1FormDesign = JsonSerializer.Deserialize<LowCodeFormDesign>(lowCode1FormData);
            if (lowCode1FormDesign == null)
            {
                throw new Exception($"文件:{lowCode1FormFilPath}内容格式有误");
            }

            LowCodeFormDesign? lowCode2FormDesign = JsonSerializer.Deserialize<LowCodeFormDesign>(lowCode2FormData);

            if (lowCode2FormDesign == null)
            {
                throw new Exception($"文件:{lowCode2FormFilePath}内容格式有误");
            }

            LowCode1Code? lowCode1Code = JsonSerializer.Deserialize<LowCode1Code>(lowCode1CodeData);

            if (lowCode1Code == null)
            {
                throw new Exception($"文件:{lowCode1CodeFilePath}内容格式有误");
            }

            string lowCode2CodeFilePath = $@"{dirPath}\{BpkFileDescription.lowCode2Code}";

            List<LowCode2CodeMRemovalItem> mRemovalItems = new List<LowCode2CodeMRemovalItem>();

            LowCode2Code lowCode2Code = new LowCode2Code()
            {
                MRemovalSubTableFieldCode = lowCode1Code.MRemovalTableName,
                SubTableNameFieldCode = lowCode1Code.MRemovalFieldDisplayName,
                SubTableSignCodeFieldCode = lowCode1Code.MRemoveUserFieldName,
                MRemovalItems = mRemovalItems
            };

            foreach (var item in lowCode1Code.MRemovalItemList ?? new List<LowCode1CodeMRemovalItem>())
            {
                LowCode2CodeMRemovalItem lowCode2CodeMRemovalItem = new LowCode2CodeMRemovalItem()
                {
                    DisplayName = item.FieldDisplayName,
                    SignFieldCode = item.RemoveUserFieldName
                };

                string? itemFormId = item.FormId;

                if (itemFormId != null)
                {
                    var lowCode1Form = lowCode1FormDesign?.Fields?.FirstOrDefault(f => f.FormId?.ToString() == itemFormId);

                    if (lowCode1Form != null)
                    {
                        var lowCode2Form = lowCode2FormDesign?.Fields?.FirstOrDefault(f => f.Config != null && f.Config?.Label == lowCode1Form.Config?.Label);

                        if (lowCode2Form == null)
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(lowCode1Form.Config?.DataTypeCode))
                        {
                            lowCode2CodeMRemovalItem.DynamicDataId = lowCode2Form.FormId.ToString();
                        }
                        else
                        {
                            lowCode2CodeMRemovalItem.DynamicDataId = lowCode2Form.VModel;
                        }

                        mRemovalItems.Add(lowCode2CodeMRemovalItem);
                    }
                }
            }

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string lowCode2CodeData = JsonSerializer.Serialize(lowCode2Code, jsonSerializerOptions);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var fileStream = new FileStream(lowCode2CodeFilePath, FileMode.OpenOrCreate, FileAccess.Write);

            fileStream.Write(Encoding.UTF8.GetBytes(lowCode2CodeData));

            ConsoleUtils.WriteLine($"低代码2.0的Code已经写入到：{lowCode2CodeFilePath}文件中，请及时查看，并且赋值到对应流程节点中", ConsoleColor.Green);
        }
    }
}
