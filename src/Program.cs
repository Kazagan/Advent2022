using AdventOfCode2022.Days;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        var day = new Day12();
        var x = day.Solution();
        Console.WriteLine(x);
        var y = day.Solution2();
        Console.WriteLine(y);
        // BenchmarkRunner.Run<Day06>();
    }
}
