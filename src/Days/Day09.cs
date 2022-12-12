namespace AdventOfCode2022.Days;

public class Day09
{
    private const string MyFile = "./TextFiles/Day09/Input.txt";

    public int Solution1()
    {
        var rope = new Rope();
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
                visited.Add(rope.Tail);
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
    public Rope()
    {
    }

    public (int X, int Y) Head { get; private set; } = (0, 0);
    public (int X, int Y) Tail { get; private set; } = (0, 0);

    public (int, int) Distance()
    {
        var horizontalDistance = Head.X - Tail.X;
        var verticalDistance = Head.Y - Tail.Y;
        return (horizontalDistance, verticalDistance);
    }

    public void MoveHead((int X, int Y) direction)
    {
        Head = (Head.X + direction.X, Head.Y + direction.Y);
        MoveTail();
    }

    private void MoveTail()
    {
        var movement = GetTailMovement();
        Tail = (Tail.X + movement.Item1, Tail.Y + movement.Item2);
    }

    private (int, int) GetTailMovement()
    {
        var distance = Distance();
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