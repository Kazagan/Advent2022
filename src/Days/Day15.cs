using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days;

public class Day15
{
    private const string MyFile = "./TextFiles/Day15/Input.txt";
    private readonly Regex _regex = new("[0-9]+|-[0-9]+");
    private const int Number = 10;
    
    public int Solution()
    {
        var sensors = ParseFile();

        var xMin = sensors.Select(x => x.XRange.Min).Min();
        var xMax = sensors.Select(x => x.XRange.Max).Max();
        var ymin = sensors.Select(x => x.XRange.Min).Min();
        var yMax = sensors.Select(x => x.XRange.Max).Max();
        var count = 0;
        for (var y = ymin; y < yMax; y++)
        {
            if(y != Number) continue;
            for (var x = xMin; x < xMax; x++)
            {
                if (!sensors.Any(s
                        => ManhattanDistance((s.X, x), (s.Y, y)) <= s.Distance)) continue;
                if (sensors.Any(b => b.Beacon.X == x && b.Beacon.Y == y)) continue;
                count++;
            }
        }

        return count;
    }

    public long Solution2()
    {
        var sensors = ParseFile();
        for (var y = 0; y <= 4000000; y++)
        {
            var slice  = sensors.Select(x => x.Slice(y));
            var gap = Gap(slice, new Range(0, 4000000));
            if (gap != -1)
                return (gap * 4000000L) + y;
        }
        
        return 0;
    }

    private List<Signal> ParseFile()
    {
        using var reader = new StreamReader(MyFile);
        var sensors = new List<Signal>();
        while (reader.ReadLine() is { } line)
        {
            var split = line.Split(":");
            var sensor = ParseLine(split[0]);
            sensor.Beacon = ParseLine(split[1]);
            sensors.Add(sensor);
        }

        return sensors;
    }

    private Signal ParseLine(string line)
    {
        var matches = _regex.Matches(line);
        return new Signal(int.Parse(matches[0].Value), int.Parse(matches[1].Value));
    }
    
    static int Gap(IEnumerable<Range> ranges, Range limit)
    {
        var ordered = ranges.Select(range => range.Intersect(limit))
            .Where(range => !range.IsEmpty)
            .OrderBy(range => range.Min)
            .ThenBy(range => range.Max);

        var max = limit.Min - 1;
        foreach (var r in ordered)
        {
            if (max + 1 < r.Min)
                return max + 1;

            max = Math.Max(max, r.Max);
        }

        if (max < limit.Max) 
            return max + 1;
        
        return -1;
    }

    private int ManhattanDistance((int a, int b) x, (int a, int b) y) =>
        Math.Abs(x.a - x.b) + Math.Abs(y.a - y.b);
}

internal class Signal
{
    public Signal(int x, int y) => (X, Y) = (x, y);
    public int X { get; set; }
    public int Y { get; set; }
    public Range XRange => new Range(X - Distance, X + Distance);
    public Range YRange => new Range(Y - Distance, Y + Distance);
    public Signal Beacon { get; set; }
    public int Distance => Math.Abs(X - Beacon.X) + Math.Abs(Y - Beacon.Y);
    
    public Range Slice(int input)
    {
        var distance = Math.Abs(input - Y);
        if (distance > Distance)
            return Range.Empty;

        var dx = Distance - distance;
        return new Range(X - dx, X + dx);
    }
}

internal struct Range
{
    public Range(int min, int max) => (Min, Max) = (min, max);
    public int Min { get; set; }
    public int Max { get; set; }
    public static Range Empty = new(0, -1);

    public bool IsEmpty => Min > Max;

    public IEnumerable<int> Values => IsEmpty ? Enumerable.Empty<int>() : Enumerable.Range(Min, Max - Min + 1);

    public bool Overlaps(Range other)
    {
        return !IsEmpty
               && !other.IsEmpty
               && Min <= other.Max
               && Max >= other.Min;
    }

    public Range Intersect(Range other)
    {
        return Overlaps(other) ? new Range(Math.Max(Min, other.Min), Math.Min(Max, other.Max)) : Empty;
    }
}
