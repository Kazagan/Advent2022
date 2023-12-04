

using AdventOfCode2023.Days;

namespace AdventOfCode2023;

public static class Program
{
    public static void Main()
    {
        var day = "01";
        var directory = $"TextFiles/Day{day}/";
        var files = Directory.GetFiles(directory);

        var results = files.ToDictionary(file => file.Split("/").Last(), Day01.Solution);
        foreach (var result in results)
        {
            Console.WriteLine($"{result.Key}: {result.Value}");
        }
    }
}