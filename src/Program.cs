using AdventOfCode2022.Days;
using BenchmarkDotNet.Running;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        // var day03 = new Day03();
        // Console.WriteLine(day03.Solution());
        // Console.WriteLine(day03.LingSolution());
        BenchmarkRunner.Run<Day03>();
    }
}