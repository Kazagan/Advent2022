using AdventOfCode2022.Days;
using BenchmarkDotNet.Running;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        var day = new Day05();
        Console.WriteLine(day.Solution());
        // BenchmarkRunner.Run<Day03>();
    }
}