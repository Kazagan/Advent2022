using System.IO;

namespace AdventOfCode2022.Days;

public static class Day02
{
    private const string File = "./src/TextFiles/Day02/Input.txt";
    private static readonly int[,] Rules =
    {
        //r  p  s   
        { 3, 6, 0 }, //r
        { 0, 3, 6 }, //p
        { 6, 0, 3 } // s
    };

    private enum Play { Rock = 0, Paper = 1, Scissors = 2 }

    public static int Game()
    {
        using var reader = new StreamReader(File);
        var currentScore = 0;
        while (reader.ReadLine() is { } line)
        {
            var plays = line.Split(" ").Select(GetElvesPlay).ToArray();
            currentScore = (int)plays[1] + 1 + WinOrLose(plays); 
        }
        return currentScore;
    }

    private static int WinOrLose(IReadOnlyList<Play> game)
    {
        return Rules[(int)game[0], (int)game[1]];
    }

    private static Play GetElvesPlay(string play)
    {
        return play switch
        {
            "A" or "X" => Play.Rock,
            "B" or "Y" => Play.Paper,
            "C" or "Z" => Play.Scissors,
            _ => throw new Exception("Invalid Play")
        };
    }
}