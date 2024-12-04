using Days;

namespace Advent;

public static class Program
{
    public static void Main(string[] args)
    {
        var arguments = ParseArgs(args);

        var day = AdventDays.Days().Single(x => x.Day == arguments.Day);
        
        Console.WriteLine(day.Solution(arguments.Path, arguments.Step));
    }

    private static ConsoleArguments ParseArgs(string[] args)
    {
        var result = new ConsoleArguments();
        for (var i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--day" or "-d":
                    result.Day = int.Parse(args[++i]);
                    break;
                case "--file" or "-f":
                    result.Path = args[++i];
                    break;
                case "--step" or "-s":
                    result.Step = int.Parse(args[++i]);
                    break;
                default:
                    PrintUsages();
                    throw new ArgumentException($"Invalid Argument: {args[i]}");
            }
        }

        return result;
    }

    private static void PrintUsages()
    {
        Console.Write("Run app with: \n --file(-f) <filePath> \n --day(-d) <day> \n --step(-s) <step> (1 or 2) ");
    }

    private class ConsoleArguments
    {
        public int Day { get; set; }
        public int Step { get; set; }
        public string Path { get; set; }
    }
}