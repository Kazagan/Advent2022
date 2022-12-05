using AdventOfCode2022.Days;
using BenchmarkDotNet.Running;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        var day = new Day04();
        Console.WriteLine(day.Solution());
        Console.WriteLine(day.LinqSolution());
        // BenchmarkRunner.Run<Day03>();
    }
}