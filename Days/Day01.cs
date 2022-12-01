using System;
using System.IO;

namespace AdventOfCode2022.Days;

public static class Day01 
{
    public static void ReadLines(string file)
    {
        var highest = 0;
        var current = 0;
        foreach (var t in File.ReadAllLines(file))
        {
            if (!string.IsNullOrEmpty(t))
            {
                current += int.Parse(t);
                continue;
            }

            if (current > highest)
                highest = current;
            current = 0;
        }

        Console.WriteLine(highest);
    }

    public static void Stream(string file)
    {
        using var stream = new StreamReader(file);
        int highest = 0, current = 0;
        while (stream.ReadLine() is { } line)
        {
            if (line != "")
            {
                current += int.Parse(line);
                continue;
            }

            if (current > highest)
                highest = current;
            current = 0;
        }

        Console.WriteLine(highest);
    }
}
