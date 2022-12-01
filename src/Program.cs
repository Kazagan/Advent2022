using AdventOfCode2022.Days;

namespace AdventOfCode2022;

public static class Program
{
    public static void Main()
    {
        var file = "./TextFiles/Day01/Input.txt";
        Day01.ReadLines(file);
        Console.WriteLine();
        Day01.Stream(file);
    }
}