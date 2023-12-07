using System.Text;

namespace AdventOfCode2023.Days;

public static class Day04
{
    public static int Part1(string file)
    {
        var cards = File.ReadLines(file)
            .Select(line => line.Split(":")[1])
            .Select(card => card.Split("|"));

        var sides = cards
            .Select(card => card.Select(side => side.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x))).ToArray());

        var matches = sides.Select(side => side[0].Intersect(side[1]));
        var points = matches.Select(match => (int)Math.Pow(2, match.Count() - 1));

        return points.Sum();
    } 
    
    public static int Part2(string file)
    {
        var cards = File.ReadLines(file)
            .Select(line => line.Split(":")[1])
            .Select(card => new Ticket
            {
                Count = 1,
                Card = card.Split("|").Select(side => side.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)))
                    .ToArray()
            }).ToList();

        for (var i = 0; i < cards.Count; i++)
        {
            for (var j = 0; j < cards[i].Count; j++)
            {
                var currentCard = cards[i];
                int count = currentCard.MatchCount ?? currentCard.Card[0].Count(x => currentCard.Card[1].Contains(x));
                currentCard.MatchCount = count;
                for (int index = 1; index <= count; index++)
                {
                    cards[i+index].Count++;
                }
            }
        }
        
        return cards.Sum(x => x.Count);
    }
}

public class Ticket
{
    public int Count { get; set; }
    public IEnumerable<string>[] Card { get; set; } = null!;
    public int? MatchCount { get; set; }
}