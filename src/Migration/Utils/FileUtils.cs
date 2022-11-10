namespace Migration.CLI.Utils
{
    public class FileUtils
    {
        public static string ReadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }

            throw new FileNotFoundException($"文件：{filePath}不存在");
        }

    }
}
