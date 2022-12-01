using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AdventOfCode2022.Days;

public static class Day01
{
    public static void ReadLines(string file)
    {
        var highThree = new int[] { 0, 0, 0 };
        var current = 0;
        var lines = File.ReadAllLines(file);
        for (int i = 0; i < lines.Length; i++)
        {
            if (!string.IsNullOrEmpty(lines[i]))
            {
                current += int.Parse(lines[i]);
                if (i != lines.Length - 1) continue;
            }

            CheckHighest(highThree, current);
            current = 0;
        }

        Console.WriteLine(highThree.Sum());
    }

    public static void Stream(string file)
    {
        using var stream = new StreamReader(file);
        var highThree = new int[] { 0, 0, 0 };
        int current = 0;
        while (true)
        {
            var line = stream.ReadLine();
            if (line is null)
            {
                CheckHighest(highThree, current);
                break;
            }
            if (line != "")
            {
                current += int.Parse(line);
                continue;
            }

            CheckHighest(highThree, current);
            current = 0;
        }

        Console.WriteLine(highThree.Sum());
    }

    private static void CheckHighest(IList<int> highThree, int current)
    {
        for (var index = 0; index < highThree.Count; index++)
            if (highThree[index] < current)
                (highThree[index], current) = (current, highThree[index]);
    }
}