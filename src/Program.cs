using AdventOfCode2022.Days;
using FluentAssertions;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        var day = new Day11();
        var x = day.Solution1();
        // x.Should().Be(1642503);
        Console.WriteLine(x);
        var y = day.Solution2();
        Console.WriteLine(y);
        // y.Should().Be(3476);
        // BenchmarkRunner.Run<Day06>();
    }
}
