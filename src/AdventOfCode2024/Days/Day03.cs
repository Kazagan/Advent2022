using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Days;

namespace Advent.Days;

public class Day03 : AdventDay
{
    private readonly Regex _regex = new(@"mul\((\d{1,3}),(\d{1,3})\)");
    
    public int Day => 3;
    
    public long First(string filePath)
    {
        var file = File.ReadAllText(filePath);

         return Mul(file);
    }

    public long Second(string filePath)
    {
        var file = File.ReadAllText(filePath);
        var sectionRegex = new Regex(@"(?>^|don't\(\)|do\(\))(.*?)(?=(?:don't\(\)|do\(\)|$))");
        
        var y = sectionRegex.Matches(file)
            .Select(x => x.Groups[0].Value)
            .Where(x => !x.StartsWith("don't()", StringComparison.Ordinal));
        y.ToList().ForEach(Console.WriteLine);
        var join = string.Join("",y);
        return Mul(join);
    }
    
    private long Mul(string file)
    {
        return _regex.Matches(file)
            .Select(x => x.Groups.Values
                .Select(x => x.Value)
                .ToArray()[1..]
                .Select(long.Parse))
            .Sum(x => x.Aggregate((a, b) => a * b));
    }

}