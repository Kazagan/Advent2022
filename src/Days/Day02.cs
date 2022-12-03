using System.IO;
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

    private enum Play { Rock = 0, Paper = 1, Scissors = 2 }

    [Benchmark]
    public int Game()
    {
        using var reader = new StreamReader(MyFile);
        var currentScore = 0;
        while (reader.ReadLine() is { } line)
        {
            var opponent = GetElvesPlay(line[0]);
            var mine = GetElvesPlay(line[2]);
            currentScore += (int)mine + 1 + Rules[(int)opponent, (int)mine];
        }
        return currentScore;
    }
    
    [Benchmark]
    public int LinqGame()
    {
        return File.ReadLines(MyFile)
            .Sum(x => (int)GetElvesPlay(x[2]) + 1 + Rules[(int)GetElvesPlay(x[0]), (int)GetElvesPlay(x[2])]);
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
}