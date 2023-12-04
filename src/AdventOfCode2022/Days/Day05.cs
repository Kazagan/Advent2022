using AdventOfCode2022.Extensions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
namespace AdventOfCode2022.Days;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Day05
{
    private const string MyFile = "./TextFiles/Day05/input.txt";
    private delegate void Model(IReadOnlyList<Stack<char>> stacks, Command command);
    
    [Benchmark]
    public string Solution1()
    {
        using var reader = new StreamReader(MyFile); 
        var stacks = BuildInitialStack(reader);

        CrateMover(reader, stacks, Model9000);
        return new string(stacks.Select(x => x.Pop()).ToArray());
    }

    [Benchmark]
    public string Solution2()
    {
        using var reader = new StreamReader(MyFile); 
        var stacks = BuildInitialStack(reader);
        
        CrateMover(reader, stacks, Model9001);
        return new string(stacks.Select(x => x.Pop()).ToArray());
    }
    
    private List<Stack<char>> BuildInitialStack(TextReader reader)
    {
        var start = ReadInitialState(reader);
        var stacks = BuildStack(start);
        return stacks;
    }

    private static List<string> ReadInitialState(TextReader reader)
    {
        var start = new List<string>();
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrEmpty(line))
                break;
            start.Add(line);
        }

        return start;
    }

    private static void CrateMover(TextReader reader, IReadOnlyList<Stack<char>> stacks, Model model)
    {
        var lines = reader
            .ReadToEnd()
            .Split("\n")
            .Select(x =>
            {
                var y = x.Split(" ");
                return new Command(int.Parse(y[1]), int.Parse(y[3]) - 1, int.Parse(y[5]) - 1  );
            }).ToList();
        for (int i = 0; i < lines.Count; i++)
        {
            model(stacks, lines[i]);
        }
    }

    private static void Model9000(IReadOnlyList<Stack<char>> stacks, Command command)
    {
        for (var i = 0; i < command.Move; i++)
        {
            var item = stacks[command.From].Pop();
            stacks[command.To].Push(item);
        }
    }

    private static void Model9001(IReadOnlyList<Stack<char>> stacks, Command command)
    {
        var items = stacks[command.From].PopRange(command.Move);
        stacks[command.To].PushRange(items);
    }

    private static List<Stack<char>> BuildStack(IReadOnlyList<string> start)
    {
        var output = new List<Stack<char>>();
        var bottom = start.Count - 1;
        var stackCount = start[bottom--].Max(x => x == ' ' ? 0 : x - '0'); 
        for (var i = 0; i < stackCount; i++)
            output.Add(new Stack<char>());

        for (var i = bottom; i >= 0; i--)
        {
            for (var j = 0; j < stackCount; j++)
            {
                var x = start[i][j * 4 + 1];
                if (x != ' ')
                    output[j].Push(x);
            }
        }

        return output;
    }
}

internal struct Command
{
    public readonly int Move;
    public readonly int From;
    public readonly int To;

    public Command(int move, int from, int to) => (Move, From, To) = (move, from, to);
}
