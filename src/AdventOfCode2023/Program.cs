

using AdventOfCode2023.Days;

namespace AdventOfCode2023;

public static class Program
{
    public static void Main()
    {
        const string day = "02";
        const string directory = $"TextFiles/Day{day}/";
        var files = Directory.GetFiles(directory);

        var results = files.ToDictionary(file => file.Split("/").Last(), Day02.Solution);
        foreach (var result in results)
        {
            Console.WriteLine($"{result.Key}: {result.Value}");
        }
    }
}