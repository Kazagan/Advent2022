using Advent.Extensions;
using Days;

namespace Advent.Days;

public class Day02 : AdventDay
{
    public int Day => 2;

    public int First(string filePath)
    {
        var lines = File.ReadLines(filePath).Select(x => x.Split(' ').Select(int.Parse).ToArray());
        return lines.Select(IsSafe).Count(result => result.All(x => x));
    }
    
    public int Second(string filePath)
    {
        var lines = File.ReadLines(filePath).Select(x => x.Split(' ').Select(int.Parse).ToArray());
        return lines.Select(IsSafeWithDamper).Count(result => result.All(x => x));
    }

    private static IEnumerable<bool> IsSafe(int[] line)
    {
        var state = line[0] - line[1] < 0;
        for (int i = 0; i < line.Length - 1; i++)
        {
            var difference = line[i] - line[i + 1];
            if (Math.Abs(difference) <= 3 && state == difference < 0 && difference != 0)
            {
                yield return true;
                continue;
            }
            yield return false;
        }
    }
    
    private static bool IsSafeWithDamper(int[] line)
    {
        var result = new List<bool>();
        var toCheck = (new List<int>(line)).ToArray();
        var state = line[0] - line[1] < 0;

        for (int i = 0; i < line.Length - 1; i++)
        {
            var difference = line[i] - line[i + 1];
            if (Math.Abs(difference) <= 3 && state == difference < 0 && difference != 0)
            {
                yield return true;
                continue;
            }
            yield return false;
        }
    }
}