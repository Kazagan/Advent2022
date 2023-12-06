

using AdventOfCode2023.Days;

namespace AdventOfCode2023;

public static class Program
{
    public static void Main()
    {
        const string day = "04";
        const string directory = $"TextFiles/Day{day}/";
        var files = Directory.GetFiles(directory);

        WritePart("Part 1");
        var part1 = files.ToDictionary(file => file.Split("/").Last(), Day04.Part1);
        foreach (var result in part1)
        {
            Console.WriteLine($"{result.Key}: {result.Value}");
        }
        
        WritePart("Part 2");
        var part2 = files.ToDictionary(file => file.Split("/").Last(), Day04.Part2);
        foreach (var result in part2)
        {
            Console.WriteLine($"{result.Key}: {result.Value}");
        }
    }

    private static void WritePart(string part)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(part);
        Console.ResetColor();
    }
}
//12263631