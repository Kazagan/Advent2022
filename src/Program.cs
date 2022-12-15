using AdventOfCode2022.Days;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        var day = new Day13();
        var x = day.Solution();
        var y = day.Solution2();
        Console.WriteLine(x);
        Console.WriteLine(y);
    }
}
