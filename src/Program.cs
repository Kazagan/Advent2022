using System.Runtime.InteropServices;
using AdventOfCode2022.Days;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        // var day2 = new Day02();
        // Console.WriteLine(day2.Game());
        // Console.WriteLine(day2.LinqGame());
        BenchmarkRunner.Run<Day02>();
    }
}