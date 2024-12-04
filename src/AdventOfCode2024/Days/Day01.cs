using Days;

namespace Advent.Days;

public class Day01 : AdventDay
{
    public int Day => 1;

    public int First(string filePath)
    {
        var left = new List<int>();
        var right = new List<int>();

        var file = File.ReadLines(filePath);
        var i = 0;
        foreach (var line in file)
        {
            var split = line.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            left.Add(int.Parse(split[0]));
            right.Add(int.Parse(split[1]));
            i++;
        }

        left = left.Order().ToList();
        right = right.Order().ToList();

        var zip = left.Zip(right, (x, y) => Math.Abs(x - y)).ToList();
        return zip.Sum();
    }
    
    public int Second(string filePath)
    {
        var left = new List<int>();
        var right = new List<int>();

        var file = File.ReadLines(filePath);
        foreach (var line in file)
        {
            var split = line.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            left.Add(int.Parse(split[0]));
            right.Add(int.Parse(split[1]));
        }

        return left.Select((x, y) => right.Count(a => a == x) * x).Sum();
    }
}
