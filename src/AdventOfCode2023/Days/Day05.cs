using System.Security.AccessControl;
using System.Security.Cryptography;

namespace AdventOfCode2023.Days;

public static class Day05
{
    public static long Part1(string file)
    {
        var sections = File.ReadAllText(file)
            .Split("\n\n");

        var seeds = sections.First(x => x.Contains("seeds:"))
            .Split(":")[1].Trim()
            .Split(" ")
            .Select(long.Parse);

        var maps = sections.Skip(1)
            .Select(section => section.Split("\n").Where(line => !line.Contains(':')))
            .Select(line => line.Select(CreateMap));

        var locations = seeds.Select(seed => ProcessMaps(seed, maps));
        
        return locations.Min();
    }


    public static long Part2(string file)
    {
        var sections = File.ReadAllText(file)
            .Split("\n\n");
        
        List<List<Map>> maps = sections.Skip(1)
            .Select(section => section.Split("\n").Where(line => !line.Contains(':')))
            .Select(line => line.Select(CreateMap).ToList()).ToList();

        var seeds = sections.First(x => x.Contains("seeds:"))
            .Split(":")[1].Trim()
            .Split(" ")
            .Select(long.Parse)
            .ProcessMaps(maps);
        
        return seeds.Min();
    }

    private static long ProcessMaps(long seed, IEnumerable<IEnumerable<Map>> maps)
    {
        long currentValue = seed;
        foreach (var map in maps)
        {
            Map? corresponding = map.SingleOrDefault(rule => currentValue >= rule.Start && currentValue <= rule.End);
            if (corresponding is null) continue;
            currentValue += corresponding.Rule;
        }

        return currentValue;
    }
    
    private static IEnumerable<long> ProcessMaps(this IEnumerable<long> seeds, List<List<Map>> maps)
    {
        while (seeds.Any())
        {
            IEnumerable<long> range = seeds.Take(2);
            
            long min = long.MaxValue;

            long seedStart = range.First();
            long seedEnd = seedStart + range.LastOrDefault() - 1;
            List<(long start, long end)> seedRanges = new() { (seedStart, seedEnd)};
            
            foreach (var map in maps)
            {
                seedRanges = seedRanges.SelectMany(x => NewRanges(x, map))
                    .Where(x => x.Item1 != 0 && x.Item2 != 0).ToList();
            }

            yield return seedRanges.MinBy(x => x.start).start;
            seeds = seeds.Skip(2);
        }
    }

    private static IEnumerable<(long, long)> NewRanges((long start, long end) range, IEnumerable<Map> maps)
    {
        List<(long, long)> result = new();
        long? minStart = null;
        long? maxEnd = null;
        
        foreach (var map in maps)
        {
            long start = Math.Max(range.start, map.Start);
            long end = Math.Min(range.end, map.End);

            if (start > end) continue;
            
            var overLap = (start, end);
            minStart = Math.Min(minStart ?? long.MaxValue, overLap.start);
            maxEnd = Math.Max(maxEnd ?? long.MinValue, overLap.end);
            result.Add((overLap.start + map.Rule, overLap.end + map.Rule));
        }
        
        if(!result.Any()) result.Add((range.start, range.end));
        else
        {
            if(minStart is not null && minStart != range.start) 
                result.Add((range.start, minStart.Value));
            
            if(maxEnd is not null && maxEnd != range.end)
                result.Add((maxEnd.Value, range.end));
        }
        return result;
    }

    private static Map CreateMap(string input)
    {
        long[] numbers = input.Split(" ").Select(long.Parse).ToArray();
        return new Map(numbers[1], numbers[1] + numbers[2] - 1, numbers[0] - numbers[1]);
    }

}

public record Map(long Start, long End, long Rule);