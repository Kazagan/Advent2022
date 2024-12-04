using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days;

public static class Day08
{
    public static int Part1(string file)
    {
        var lines = File.ReadLines(file).ToArray();

        using var directions = lines.First().GetEnumerator();
        

        var locations = CreateLocations(lines.Skip(2));

        const string start = "AAA";
        const string end = "ZZZ";
        var current = locations.SingleOrDefault(x => x.Title == start);
        if (current is null)
        {
            return -1;
        }
        
        var count = 0;
        while (current.Title != end)
        {
            if (!directions.MoveNext())
            {
                directions.Reset();
                directions.MoveNext();
            }
            current = directions.Current == 'L' ? current.Left : current.Right;
            count++;
        }
        
        return count;
    }
    
    public static long Part2(string file)
    {
        var lines = File.ReadLines(file).ToArray();

        var directions = lines.First();
        
        var locations = CreateLocations(lines.Skip(2));
        var startLocations = locations.Where(x => x.Title.EndsWith('A')).ToList();

        var cycles = new Dictionary<Location, long>();
        foreach (var location in startLocations)
        {
            var count = StepsToEnd(location, directions);
            cycles[location] = count;
        }

        long result = cycles.Values.Aggregate(LCM);
        
        return result;
    }

    private static long StepsToEnd(Location location, string directions)
    {
        var count = 0;
        var current = location;
        while (!current.Title.EndsWith('Z'))
        {
            var left = directions[count % directions.Length] == 'L';
            current = left ? current.Left! : current.Right!;
            count++;
        }

        return count;
    }

    private static IEnumerable<Location> CreateLocations(IEnumerable<string> lines)
    {
        var locations = new List<Location>();
        foreach (var line in lines)
        {
            CreateLocations(line, locations);
        }

        return locations;
    }

    private static void CreateLocations(string line, List<Location> locations)
    {
        var regex = new Regex(@"\w{3}");
        var locals = regex.Matches(line).Select(x => x.Value).ToArray();

        var left = locations.SingleOrDefault(x => x?.Title == locals[1], null);
        if (left is null)
        {
            left = new Location(locals[1]);
            locations.Add(left);
        }

        var right = locations.SingleOrDefault(x => x?.Title == locals[2], null);
        if (right is null)
        {
            right = new Location(locals[2]);
            locations.Add(right);
        }
        
        var current = locations.SingleOrDefault(x => x?.Title == locals[0], null);
        
        if (current is null)
        {
            current = new Location(locals[0], left, right);
            locations.Add(current);
            return;
        }
        
        current.Left = left;
        current.Right = right;
    }

    private static long GCD(long x, long y)
    {
        while(y != 0)
        {
            var temp = y;
            y = x % y;
            x = temp;
        }

        return x;
    }
    
    private static long LCM(long x, long y) => x / GCD(x, y) * y;
}

public sealed class Location
{
    public Location(string title)
    {
        Title = title;
    }

    public Location(string title, Location left, Location right)
    {
        Title = title;
        Left = left;
        Right = right;
    }
    public string Title { get; init; }
    public Location? Left { get; set; }
    public Location? Right { get; set; }
}


// TODO:
// Get Directions, string is probably enough to contain it.
// Build out all locations
// Build out map, ideally referencing the object that is needed