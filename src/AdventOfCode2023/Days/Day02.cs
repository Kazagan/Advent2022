using System.Xml;

namespace AdventOfCode2023.Days;

public static class Day02
{
    private static readonly Dictionary<string, int> Maximums = new() {{"red", 12}, {"green", 13}, {"blue", 14}}; 

    public static int Solution(string file)
    {
        return File.ReadLines(file)
            .Select(line =>
            {
                var sections = line.Split(":");
                return new Game()
                {
                    Id = int.Parse(sections[0].Split(" ")[1]),
                    Rounds = MaxRound(sections[1])
                };
            })
            .Select(x => x.Rounds.Blue  * x.Rounds.Green * x.Rounds.Red).Sum();

    }

    private static Round MaxRound(string line)
    {
        var rounds = line.Split(';');
        var draws = rounds.SelectMany(round => round.Split(",").Select(draw => draw.Trim()).ToList()).ToList();
        var grouped = draws.Select(x =>
            {
                var split = x.Split(" ");
                return new { Count = int.Parse(split[0]), Color = split[1] };
            })
            .GroupBy(x => x.Color)
            .ToDictionary(group => group.Key, group => group.MaxBy(x => x.Count)?.Count ?? 0);
        
        
        return new Round
        {
            Red = grouped["red"],
            Green = grouped["green"],
            Blue = grouped["blue"]
        };
    }
}

public class Game
{
    public int Id { get; set; }
    public Round Rounds { get; set; } = new();
}

public class Round
{
    public int Red { get; set; }
    public int Green { get; set; }
    public int Blue { get; set; }
}
