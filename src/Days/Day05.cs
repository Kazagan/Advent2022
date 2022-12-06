using System.Text.RegularExpressions;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022.Days;

public class Day05
{
    private const string MyFile = "./TextFiles/Day05/Input.txt";

    public string Solution()
    {
        var start = new List<string>();
        var lines = File.ReadAllLines(MyFile);
        int i = 0;
        while (!lines[i].Contains("move"))
        {
            start.Add(lines[i++]);
        }

        var stacks = BuildStack(start);
        var moves = lines
            .Skip(i)
            .Select(x => Regex.Matches(x, "[0-9]+")
                .Select(y => int.Parse(y.Value)).ToArray());

        foreach (var move in moves)
        {
            Console.WriteLine($"move {move[0]} from {move[1]} to {move[2]}");
            var items = stacks[move[1] - 1].PopRange(move[0]);
            stacks[move[2] - 1].PushRange(items);
        }

        return new string(stacks.Select(x => x.Pop()).ToArray());
    }

    private List<Stack<char>> BuildStack(List<string> start)
    {
        var output = new List<Stack<char>>();
        var bottom = start.Count - 2; // 2 to account for empty line between initial setup and moves.
        var stackCount =
            start[bottom--]
                .Max(x => x == ' ' ? 0 : x - '0'); // Get number of stacks, and set bottom to bottom of stacks.
        for (int i = 0; i < stackCount; i++)
            output.Add(new Stack<char>());

        for (int i = bottom; i >= 0; i--)
        {
            for (int j = 0; j < stackCount; j++)
            {
                var x = start[i][j * 4 + 1];
                if (x != ' ')
                    output[j].Push(x);
            }
        }

        return output;
    }
}