using AdventOfCode2022.Extensions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AdventOfCode2022.Days;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Day01
{
    private const string MyFile = "./TextFiles/Day01/Input.txt";

    [Benchmark]
    public int LinqSolution()
    {
        return File.ReadAllText(MyFile).Split("\n\n")
            .Select(x => x.Split("\n").Sum(int.Parse))
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
    }
    
    [Benchmark]
    public int ReadLines()
    {
        var highThree = new[] { 0, 0, 0 };
        var current = 0;
        var lines = File.ReadAllLines(MyFile);
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

        return highThree.Sum();
    }
    
    [Benchmark]
    public int Stream()
    {
        using var stream = new StreamReader(MyFile);
        var highThree = new[] { 0, 0, 0 };
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

        return highThree.Sum();
    }

    private static void CheckHighest(IList<int> highThree, int current)
    {
        
        for (var index = 0; index < highThree.Count; index++)
            if (highThree[index] < current)
                (highThree[index], current) = (current, highThree[index]);
    }
    
    [Benchmark]
    //The following isn't mine, and was found online, want to bench mark it.
    public int OnlineAnswer()
    {
        return File.ReadLines(MyFile)
            .ChunkBy(string.IsNullOrWhiteSpace)
            .Select(x => x.Sum(int.Parse))
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
    }
}