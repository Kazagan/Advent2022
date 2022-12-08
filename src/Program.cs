using AdventOfCode2022.Days;
using FluentAssertions;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        var day = new Day07();
        var x = day.Solution1();
        x.Should().Be(1642503);
        Console.WriteLine(x);
        var y = day.Solution2();
        // y.Should().Be(3476);
        Console.WriteLine(y);
        // BenchmarkRunner.Run<Day06>();
    }
}