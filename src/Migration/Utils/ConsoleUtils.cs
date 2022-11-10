namespace Migration.CLI.Utils
{
    public class ConsoleUtils
    {
        public static void WriteLine(string message, ConsoleColor foregroundColor)
        {
            var currentForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ForegroundColor = currentForegroundColor;
        }

        public static void Error(string message)
        {
            WriteLine(message, ConsoleColor.Red);
        }
    }
}
