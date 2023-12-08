using System.ComponentModel.Design;
using System.Runtime.Intrinsics.X86;

namespace AdventOfCode2023.Days;

public static class Day07
{
    public static int Part1(string file)
    {
        var lines = File.ReadLines(file);
        
        var hands = lines.Select(x =>
        {
            var temp = x.Split(" ");
            return (temp[0], int.Parse(temp[1]));
        });

        var ordered = hands.OrderBy(x => x.Item1, new CardComparer())
            .Select((x, y) => x.Item2 * (y + 1)).ToList();
        
        return ordered.Aggregate((a, b) => a + b);
    }

    public static int Part2(string file)
    {
        var lines = File.ReadLines(file);
        
        var hands = lines.Select(x =>
        {
            var temp = x.Split(" ");
            return (temp[0].Replace("J", "X"), int.Parse(temp[1]));
        });

        var orderedEnumerable = hands.OrderBy(x => x.Item1, new CardComparer()).ToList();
        var x = hands.OrderBy(x => x.Item1, new CardComparer()).Select((x, i) => (x.Item1, i)).ToList();
        var ordered = orderedEnumerable
            .Select((x, y) => x.Item2 * (y + 1)).ToList();
        
        return ordered.Aggregate((a, b) => a + b);
    }
}

public class CardComparer : IComparer<string>
{
    private static Dictionary<char, int> _heiarchy = new()
    {
        {'A', 14}, {'K', 13}, {'Q', 12}, {'J', 11}, {'T', 10}, {'9', 9}, {'8', 8}, {'7', 7}, {'6', 6}, {'5', 5}, 
        {'4', 4}, {'3', 3}, {'2', 2}, {'X', 0}
    };
    
    public int Compare(string? x, string? y)
    {
        var xGrouped = x!.GroupBy(a => a).OrderByDescending(a => a.Count()).ToList();
        var yGrouped = y!.GroupBy(a => a).OrderByDescending(a => a.Count()).ToList();

        IGrouping<char, char>? xBest = xGrouped.MaxBy(a => a.Count());
        IGrouping<char, char>? yBest = yGrouped.MaxBy(a => a.Count());
        
        decimal xCount = xBest.Count();
        decimal yCount = yBest.Count();

        if (xCount == 1 && xGrouped.Select(x => x.Key).Contains('X'))
            xCount++;
        if (yCount == 1 && yGrouped.Select(x => x.Key).Contains('X'))
            yCount++;
        
        if (xBest.Key != 'X')
            xCount += xGrouped.SingleOrDefault(a => a.Key == 'X')?.Count() ?? 0;
        if (yBest.Key != 'X')
            yCount += yGrouped.SingleOrDefault(a => a.Key == 'X')?.Count() ?? 0;

        var firstRunnerUp = xGrouped.Skip(1).FirstOrDefault(x => x.Key != 'X')?.Count() ?? 0;
        var secondRunnerUp = yGrouped.Skip(1).FirstOrDefault(x => x.Key != 'X')?.Count() ?? 0;
        
        if (xCount != 5 && firstRunnerUp == 2) xCount += (decimal).5;
        if (yCount != 5 && secondRunnerUp == 2) yCount += (decimal).5;

        if (xCount > yCount)
            return 1;
        if (xCount != yCount) return -1;
        var index = 0;
        while (index < x.Length)
        {
            if (_heiarchy[x[index]] > _heiarchy[y[index]])
                return 1;
            if (_heiarchy[x[index]] < _heiarchy[y[index]]) return -1;
            index++;
        }
        
        return 0;

    }
}

//245181604
//245181604
//245383453