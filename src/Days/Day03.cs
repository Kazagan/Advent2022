using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AdventOfCode2022.Days;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Day03
{
    private const string MyFile = "./TextFiles/Day03/Input.txt";

    [Benchmark]
    public int LingSolution()
    {
        return File.ReadAllLines(MyFile)
            .Select(line => line.Take(line.Length / 2).Intersect(line.TakeLast(line.Length / 2)).First())
            .Sum(x => x < 91 ? x - 38 : x - 96);
    }

    [Benchmark]
    public int Solution()
    {
        using var reader = new StreamReader(MyFile);
        var prioritySum = 0;
        while (reader.ReadLine() is { } line)
        {
            var middle = line.Length / 2;
            var found = false;
            for (var i = 0; i < middle; i++)
            {
                for (var j = middle; j < line.Length; j++)
                {
                    if (line[i] != line[j]) continue;
                    prioritySum += line[i] < 91 ? line[i] - 38 : line[i] - 96;
                    found = true;
                    break;
                }

                if (found)
                    break;
            }
        }

        return prioritySum;
    }
}