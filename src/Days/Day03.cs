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
    public int LinqSolution()
    {
        return File.ReadLines(MyFile)
            .Chunk(3)
            .Select(x => x[0].Intersect(x[1].Intersect(x[2])).First()).ToList()
            .Sum(x => x < 91 ? x - 38 : x - 96);
    }

    [Benchmark]
    public int Solution()
    {
        using var reader = new StreamReader(MyFile);
        var prioritySum = 0;
        while (reader.ReadLine() is { } line)
        {
            var line2 = reader.ReadLine();
            var line3 = reader.ReadLine();
            var found = false;
            for (var i = 0; i < line.Length; i++)
            {
                for (var j = 0; j < line2?.Length; j++)
                {
                    if(line[i] != line2[j])
                        continue;
                    for (var k = 0; k < line3?.Length; k++)
                    {
                        if(line[i] != line3[k])
                            continue;
                        found = true;
                        prioritySum += line[i] < 91 ? line[i] - 38 : line[i] - 96;
                        break;
                    }

                    if (found)
                        break;
                }
                if (found)
                    break;
            }
        }

        return prioritySum;
    }
}