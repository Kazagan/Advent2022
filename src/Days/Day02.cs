using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AdventOfCode2022.Days;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Day02
{
    // private const string MyFile = "./src/TextFiles/Day02/Input.txt";
    private const string MyFile = "./TextFiles/Day02/Input.txt";
    private static readonly int[,] Rules =
    {
        //r  p  s   
        { 3, 6, 0 }, //r
        { 0, 3, 6 }, //p
        { 6, 0, 3 } // s
    };

    private enum Play { Rock, Paper, Scissors }

    [Benchmark]
    public int Game()
    {
        using var reader = new StreamReader(MyFile);
        var currentScore = 0;
        while (reader.ReadLine() is { } line)
        {
            var opponent = GetElvesPlay(line[0]);
            var mine = GetMyPlay(opponent, line[2]);
            currentScore += (int)mine + 1 + Rules[(int)opponent, (int)mine];
        }
        return currentScore;
    }
    
    [Benchmark]
    public int LinqGame()
    {
        return File.ReadLines(MyFile)
            .Sum(x => (int)GetMyPlay(GetElvesPlay(x[0]), x[2]) + 1 +
                      Rules[(int)GetElvesPlay(x[0]), (int)GetMyPlay(GetElvesPlay(x[0]), x[2])]);
    }

    private static Play GetElvesPlay(char play)
    {
        return play switch
        {
            'A' or 'X' => Play.Rock,
            'B' or 'Y' => Play.Paper,
            'C' or 'Z' => Play.Scissors,
            _ => throw new Exception("Invalid Play")
        };
    }

    private static Play GetMyPlay(Play opponentPlay, char strategy)
    {
        var goal = strategy switch
        {
            'X' => 0,
            'Y' => 3,
            'Z' => 6,
            _ => throw new Exception("invalid strategy")
        };
        int i;
        for (i = 0; i < 3; i++)
        {
            if (Rules[(int)opponentPlay, i] == goal)
                break;
        }

        return (Play)i;
    }
}