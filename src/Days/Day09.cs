using System.Runtime.Intrinsics.X86;

namespace AdventOfCode2022.Days;

public class Day09
{
    private const string MyFile = "./TextFiles/Day09/Input.txt";

    public int Solution1()
    {
        var rope = new Rope(2);
        var visited = new HashSet<(int, int)>();
        using var reader = new StreamReader(MyFile);
        while (reader.ReadLine() is { } line)
        {
            var lineSplit = line.Split(" ");
            var distance = int.Parse(lineSplit[1]);
            var direction = GetDirection(lineSplit[0]);
            for (int i = 0; i < distance; i++)
            {
                rope.MoveHead(direction);
                visited.Add(rope.Knots[1]);
            }
        }
        
        return visited.Count;
    }

    private (int, int) GetDirection(string direction)
    {
        return direction switch
        {
            "R" => (1, 0),
            "L" => (-1, 0),
            "U" => (0, 1),
            "D" => (0, -1),
            _ => throw new Exception($"Invalid direction {direction}")
        };
    }
}

public struct Rope
{
    public Rope(int knots)
    {
        Knots = new (int, int)[knots];
    }

    public (int X, int Y)[] Knots { get; private set; }

    public (int, int) Distance(int i, int j)
    {
        var horizontalDistance = Knots[i].X - Knots[j].X;
        var verticalDistance = Knots[i].Y - Knots[j].Y;
        return (horizontalDistance, verticalDistance);
    }

    public void MoveHead((int X, int Y) direction)
    {
        Knots[0] = (Knots[0].X + direction.X, Knots[0].Y + direction.Y);
        for (var i = 1; i < Knots.Length; i++)
        {
            MoveKnot(i);
        }
    }

    private void MoveKnot(int i)
    {
        var movement = GetTailMovement(i);
        Knots[i] = (Knots[i].X + movement.Item1, Knots[i].Y + movement.Item2);
    }

    private (int, int) GetTailMovement(int i)
    {
        var distance = Distance(i-1, i);
        var a = distance.Item1 * distance.Item1;
        var b = distance.Item2 * distance.Item2;
        var dist = Math.Sqrt(a + b);
        var movement = dist switch
        {
            > 2 => (1, 1),
            2 => distance.Item1 is 2 or -2 ? (1, 0) : (0, 1),
            _ => (0, 0)
        };
        if (distance.Item1 < 0) movement.Item1 *= -1;
        if (distance.Item2 < 0) movement.Item2 *= -1;
        return movement;
    }
}