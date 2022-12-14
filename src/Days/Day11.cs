using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days;

public class Day11
{
    private const string MyFile = "./TextFiles/Day11/Input.txt";

    public long Solution1()
    {
        using var reader = new StreamReader(MyFile);
        var monkeys = InitialMonkeyBusiness(reader);

        PreformRounds(monkeys, 20, 3);

        var x = monkeys
            .Select(x => x.InspectionCount)
            .OrderByDescending(x => x)
            .Take(2);
        return x.Aggregate((a, b) => a * b);
    }

    public long Solution2()
    {
        using var reader = new StreamReader(MyFile);
        var monkeys = InitialMonkeyBusiness(reader);

        PreformRounds(monkeys, 10000, 0);

        var x = monkeys
            .Select(x => x.InspectionCount)
            .OrderByDescending(x => x)
            .Take(2);
        return x.Aggregate((a, b) => a * b);
    }

    private void PreformRounds(List<Monkey> monkeys, int rounds, int worry)
    {
        if (worry == 0)
            worry = monkeys.Select(x => x.Divisor).Aggregate(LeastCommonMultiple);
        for (var i = 0; i < rounds; i++)
            PreformRound(monkeys, worry);
    }

    private int GreatestCommonDenominator(int x, int y)
    {
        while (true)
        {
            if (y == 0) return x;
            var x1 = x;
            x = y;
            y = x1 % y;
        }
    }

    private int LeastCommonMultiple(int x, int y)
    {
        return x / GreatestCommonDenominator(x, y) * y;
    }

    private void PreformRound(List<Monkey> monkeys, int worry)
    {
        foreach (var monkey in monkeys)
            monkey.ThrowItem(monkeys, worry);
    }

    private List<Monkey> InitialMonkeyBusiness(TextReader reader)
    {
        var monkeys = new List<Monkey>();
        while (reader.ReadLine() is { } line)
            if (line.Contains("Monkey"))
                monkeys.Add(ImportMonkey(reader));
        return monkeys;
    }

    private Monkey ImportMonkey(TextReader reader)
    {
        var newMonkey = new Monkey
        {
            Items = GetItems(reader.ReadLine()!),
            Operation = GetOperation(reader.ReadLine()!),
            Divisor = GetDivisor(reader.ReadLine()!),
            Targets = GetTargets(reader.ReadLine()!, reader.ReadLine()!)
        };
        return newMonkey;
    }

    private Queue<long> GetItems(string line)
    {
        var pattern = new Regex("[0-9]+");
        var que = new Queue<long>();
        var matches = pattern.Matches(line);

        foreach (Match match in matches)
            que.Enqueue(int.Parse(match.Value));

        return que;
    }

    private Func<long, long> GetOperation(string line)
    {
        var x = line.Split(" ").TakeLast(2).ToArray();
        return x[0] switch
        {
            "+" => x[1] == "old" ? old => old + old : old => old + int.Parse(x[1]),
            "*" => x[1] == "old" ? old => old * old : old => old * int.Parse(x[1]),
            "-" => x[1] == "old" ? old => old - old : old => old - int.Parse(x[1]),
            "/" => x[1] == "old" ? old => old / old : old => old / int.Parse(x[1]),
            _ => throw new Exception($"Invalid operator {x[0]}")
        };
    }

    private int GetDivisor(string line)
    {
        var x = line.Split(" ").Last();
        return int.Parse(x);
    }

    private (int, int) GetTargets(string line1, string line2)
    {
        var t = line1.Split(" ").Last();
        var f = line2.Split(" ").Last();
        return (int.Parse(t), int.Parse(f));
    }
}

public class Monkey
{
    public Queue<long> Items { get; set; }
    public Func<long, long> Operation { get; set; }
    public int Divisor { get; set; }
    public (int, int) Targets { get; set; }
    public long InspectionCount { get; set; }

    public void ThrowItem(List<Monkey> monkeys, int worry)
    {
        var items = Items.Count;
        for (var i = 0; i < items; i++)
        {
            var thrown = PreformInspection();
            if (worry == 3) thrown /= 3;
            else thrown %= worry; // Reduce number by LCM
            var throwTo = thrown % Divisor == 0 ? Targets.Item1 : Targets.Item2;
            monkeys[throwTo].Items.Enqueue(thrown);
        }
    }

    private long PreformInspection()
    {
        var item = Items.Dequeue();
        item = Operation(item);
        InspectionCount++;
        return item;
    }
}

