namespace AdventOfCode2023.Days;

public static class Day06
{
    public static long Part1(string file)
    {
        var lines = File.ReadAllLines(file);
        var times = lines[0].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Skip(1).ToArray();
        var distance = lines[1].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Skip(1).ToArray();
        IEnumerable<(long time, long distance)> races = times.Select((t, i) => (long.Parse(t), long.Parse(distance[i])));

        var wins = races.Select(WinCount).ToList();
        
        return wins.Aggregate((a, b) => a * b);
    }

    public static long Part2(string file)
    {
        var lines = File.ReadAllLines(file);
        var times = lines[0].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Skip(1);
        var distances = lines[1].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Skip(1);

        var time = string.Join("", times.ToArray());
        var distance = string.Join("", distances.ToArray());
        
        return WinCount((long.Parse(time), long.Parse(distance)));
    }
    
    private static long WinCount((long time, long distance) x)
    {
        var winCount = 0;
        for (long i = 1; i < x.time - 1; i++)
        {
            var distance = i * (x.time - i);
            if (distance > x.distance) winCount++;
        }

        return winCount;
    }

}