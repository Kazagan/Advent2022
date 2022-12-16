namespace AdventOfCode2022.Days;

public class Day14
{
    private const string MyFile = "./TextFiles/Day14/Input.txt";

    public int Solution()
    {
        using var reader = new StreamReader(MyFile);
        var rocks = new List<List<Coordinates>>();
        while (reader.ReadLine() is { } line)
            rocks.Add(GetSlices(line));
        
        var right = rocks.SelectMany(x => x.Select(y => y.X)).Max() + 5;
        var bottom = rocks.SelectMany(x => x.Select(y => y.Y)).Max() + 2;
        var left = rocks.SelectMany(x => x.Select(y => y.X)).Min() - 2;
        var grid = InitializeGrid(bottom, right);
        grid[0, 500] = '+';
        PlaceObstructions(rocks, grid);
        return GenerateSand(grid, left);
    }

    private int GenerateSand(char[,] grid, int left)
    {
        int count = 0;
        while(SandFlow(grid, left))
        {
            count++;
            PrintGrid(grid, left);
        }
        return count;
    }

    private bool SandFlow(char[,] grid, int left)
    {
        var sandLocation = new Coordinates(500, 0);
        sandLocation = Drop(grid, sandLocation, left);
        if (sandLocation.Equals((0, 0)))
            return false;
        grid[sandLocation.Y, sandLocation.X] = '0';
        return true;
    }

    private static Coordinates Drop(char[,] grid, Coordinates current, int left)
    {
        while (true)
        {
            grid[current.Y, current.X] = '.';
            if (current.Y+1 == grid.GetLength(0))
                return new Coordinates(0,0);
            
            if (grid[current.Y + 1, current.X] == '.')
                current += (0, 1);
            else if (grid[current.Y + 1, current.X - 1] == '.')
                current += (-1, 1);
            else if (grid[current.Y + 1, current.X + 1] == '.')
                current += (1, 1);
            else
                break;
            
            grid[current.Y, current.X] = '0';
            PrintGrid(grid, left);
        }

        return current;
    }

    private static void PlaceObstructions(List<List<Coordinates>> rocks, char[,] grid)
    {
        foreach (var rock in rocks)
        {
            PlaceRocks(rock, grid);
        }
    }

    private static void PlaceRocks(IReadOnlyList<Coordinates> rock, char[,] grid)
    {
        for (var i = 0; i < rock.Count - 1; i++)
        {
            var start = rock[i];
            var end = rock[i + 1];
            Place(grid, start, end);
        }
    }

    private static void Place(char[,] grid, Coordinates start, Coordinates end)
    {
        grid[start.Y, start.X] = '#';
        while(start != end)
        {
            var move = GetMovement(end, start);
            start += move;
            grid[start.Y, start.X] = '#';
        }
    }

    private static Coordinates GetMovement(Coordinates start, Coordinates end)
    {
        var x = start.X - end.X;
        var y = start.Y - end.Y;
        if (x != 0)
            x = x < 0 ? -1 : 1;
        if (y != 0)
            y = y < 0 ? -1 : 1;

        return new Coordinates(x, y);
    }

    private static char[,] InitializeGrid(int yMax, int xMax)
    {
        var x =  new char[yMax,xMax];
        for(var i = 0; i < yMax; i++)
        {
            for (var j = 0; j < xMax; j++)
            {
                x[i, j] = '.';
            }
        }

        return x;
    }

    private static void PrintGrid(char[,] grid, int min)
    {
        Thread.Sleep(50);
        Console.Clear();
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = min; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i,j]);
            }
            Console.WriteLine();
        }
    }

    private List<Coordinates> GetSlices(string line)
    {
        return line
            .Split("->")
            .Select(x => x.Split(","))
            .Select(c => new Coordinates(int.Parse(c[0]), int.Parse(c[1])))
            .ToList();
    }
}

internal struct Coordinates
{
    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { get; set; }
    public int Y { get; set;  }

    public static Coordinates operator +(Coordinates a) => a;
    public static Coordinates operator -(Coordinates a) => new (-a.X, -a.Y);
    public static Coordinates operator +(Coordinates a, Coordinates b) => new (a.X + b.X, a.Y + b.Y);
    public static Coordinates operator +(Coordinates a, (int X, int Y) t) => new(a.X + t.X, a.Y + t.Y);
    public static Coordinates operator -(Coordinates a, Coordinates b) => a + (-b);
    public static bool operator ==(Coordinates a, Coordinates b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Coordinates a, Coordinates b) => !(a==b);
    public bool Equals(Coordinates other)
    {
        return X == other.X && Y == other.Y;
    }
    public bool Equals((int X, int Y) t)
    {
        return X == t.X && Y == t.Y;
    }
    public override bool Equals(object? obj)
    {
        return obj is Coordinates other && Equals(other);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}