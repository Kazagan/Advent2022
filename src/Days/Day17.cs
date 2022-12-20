using AdventOfCode2022.Extensions;

namespace AdventOfCode2022.Days;

public class Day17
{
    private const string MyFile = "./TextFiles/Day17/Input.Txt";

    public int Solution()
    {
        var wind = ParseFile();
        var chamber = new Chamber(0, 7);
        return RockFall(wind, chamber, 2022);
    }

    private static int RockFall(IEnumerable<char> wind, Chamber chamber, int rocks)
    {
        var gusts = new WrappedList<char>(wind);
        for (var i = 0; i < rocks; i++)
        {
            Console.WriteLine(i);
            if(i % 100 == 0)
                Console.Clear();
            DropRock(gusts, chamber, i);
        }

        return chamber.Height;
    }

    private static void DropRock(WrappedList<char> wind, Chamber chamber, int shape)
    {
        var rock = Rock.Spawn((2, chamber.Height+4), shape % 5);
        var falling = true;
        while (falling)
        {
            // PrintCurrent(chamber, rock);
            Shift(wind, chamber, ref rock);
            falling = Drop(chamber, ref rock);
        }
        chamber.Land(rock);
    }

    private static bool Drop(Chamber chamber, ref Rock rock)
    {
        var offset = rock.MoveDown();
        var min = offset.Shape.MinBy(x => x.Y).Y;
        if (min > 0 && !offset.Shape.Any(p => chamber.Pile.Contains(p)))
        {
            rock = offset;
            return true;
        }
        return false;
    }

    private static void Shift(WrappedList<char> wind, Chamber chamber, ref Rock rock)
    {
        var direction = wind.Next();
        var offset = rock.MoveHorizontally(direction);
        var min = offset.Shape.MinBy(x => x.X).X;
        var max = offset.Shape.MaxBy(x => x.X).X;
        if (min >= 0 && max < chamber.Width && !offset.Shape.Any(p => chamber.Pile.Contains(p)))
            rock = offset;
    }

    private static void PrintCurrent(Chamber chamber, Rock rock)
    {
        Thread.Sleep(250);
        var height = chamber.Height + 10;
        Console.Clear();
        for (var y = height; y >= 0; y--)
        {
            for (var x = -1; x <= chamber.Width; x++)
            {
                if (x == -1 || x == chamber.Width)
                {
                    Console.Write('|');
                }
                else if (rock.Shape.Contains((x, y)))
                {
                    Console.Write('@');
                }
                else
                {
                    Console.Write(chamber.Pile.Contains((x, y)) ? '#' : '.');
                }
            }
            Console.WriteLine($"{y} - {chamber.Height}");
        }
        Console.WriteLine("101234567");
    }

    private IEnumerable<char> ParseFile() => File.ReadAllText(MyFile).Select(x => x);
}

internal class Chamber
{
    public Chamber(int height, int width)
    {
        Height = height;
        Width = width;
        Pile = new HashSet<(int X, int Y)>();
        InitializePile();
    }

    public int Height { get; private set; }
    public int Width { get; }

    public HashSet<(int X, int Y)> Pile { get; }


    public void Land(Rock rock)
    {
        foreach (var s in rock.Shape)
        {
            Pile.Add(s);
        }

        Height = Pile.MaxBy(x => x.Y).Y;
    }

    private void InitializePile()
    {
        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j <= Height; j++)
            {
                Pile.Add((i, j));
            }
        }
    }
}

internal record Rock((int X, int Y)[] Shape)
{
    public Rock(Rock rock)
    {
        Shape = new (int, int)[rock.Shape.Length];
        rock.Shape.CopyTo(Shape, 0);
    }
    
    public Rock MoveDown()
    {
        return new Rock(Shape: Shape.Select(x => (x.X, x.Y - 1)).ToArray());
    }

    public Rock MoveHorizontally(char direction)
    {
        return direction switch
        {
            '<' => new Rock(Shape: Shape.Select(x => (x.X - 1, x.Y)).ToArray()),
            '>' => new Rock(Shape: Shape.Select(x => (x.X + 1, x.Y)).ToArray()),
            _ => throw new ArgumentException($"Invalid Direction {direction}")
        };
    }

    public static Rock Spawn((int X, int Y) location, int shape)
    {
        var rock = new Rock(Shapes[shape]);
        for (var i = 0; i < rock.Shape.Length; i++)
        {
            rock.Shape[i].X += location.X;
            rock.Shape[i].Y += location.Y;
        }

        return rock;
    }

    public static readonly Rock[] Shapes =
    {
        new (new [] { (0, 0), (1, 0), (2, 0), (3, 0) }), // Horizontal Line
        new (new [] { (1, 0), (0, 1), (1, 1), (2, 1), (1, 2) }), // Cross
        new (new [] { (0, 0), (1, 0), (2, 0), (2, 1), (2, 2) }), // ReverseL
        new (new [] { (0, 0), (0, 1), (0, 2), (0, 3) }), // Vertical Line
        new (new [] { (0, 0), (1, 0), (0, 1), (1, 1) }), // Block
    };
}