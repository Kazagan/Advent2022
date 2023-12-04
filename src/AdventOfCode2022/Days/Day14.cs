using AdventOfCode2022.Helpers;

namespace AdventOfCode2022.Days;

public class Day14
{
    private const string MyFile = "./TextFiles/Day14/Example.txt";

    public int Solution()
    {
        using var reader = new StreamReader(MyFile);
        var rocks = new List<List<Coordinates>>();
        while (reader.ReadLine() is { } line)
            rocks.Add(GetSlices(line));
        
        var right = rocks.SelectMany(x => x.Select(y => y.X)).Max() * 2;
        var bottom = rocks.SelectMany(x => x.Select(y => y.Y)).Max() + 3;
        var grid = InitializeGrid(bottom, right);
        grid[0, 500] = '+';
        PlaceObstructions(rocks, grid);
        return GenerateSand(grid, 0);
    }

    private int GenerateSand(char[,] grid, int left)
    {
        int count = 0;
        PrintGrid(grid, left, (0, grid.GetLength(0)));
        while(SandFlow(grid, left))
        {
            count++;
        }
        return count;
    }

    private bool SandFlow(char[,] grid, int left)
    {
        var sandLocation = new Coordinates(500, 0);
        sandLocation = Drop(grid, sandLocation, left);
        if (sandLocation.Equals((0, 0)))
            return false;
        if (sandLocation.Equals((500, 0)))
            return false;
        grid[sandLocation.Y, sandLocation.X] = '0';
        return true;
    }

    private static Coordinates Drop(char[,] grid, Coordinates current, int left)
    {
        while (true)
        {
            var prior = current;
            grid[current.Y, current.X] = ' ';
            if (current.Y+1 == grid.GetLength(0))
                return new Coordinates(0,0);
            
            
            if (grid[current.Y + 1, current.X] == ' ')
                current += (0, 1);
            else if (grid[current.Y + 1, current.X - 1] == ' ')
                current += (-1, 1);
            else if (grid[current.Y + 1, current.X + 1] == ' ')
                current += (1, 1);
            else
                break;
            
            grid[current.Y, current.X] = '0';
            UpdateConsole(grid, current, prior, left);
        }

        return current;
    }

    private static void PlaceObstructions(List<List<Coordinates>> rocks, char[,] grid)
    {
        foreach (var rock in rocks)
        {
            PlaceRocks(rock, grid);
        }

        for (int i = 0; i < grid.GetLength(1); i++)
            grid[grid.GetLength(0) - 1, i] = '#';
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
                x[i, j] = ' ';
            }
        }

        return x;
    }

    private static void PrintGrid(char[,] grid, int left, (int x, int y) x)
    {
        var top = x.x < 0 ? 0 : x.x;
        var bottom = x.y > grid.GetLength(0) ? grid.GetLength(0) : x.y;
        var length = grid.GetLength(1);
        grid[0, 500] = '+';
        Console.SetCursorPosition(0,3);
        for (var i = top; i < bottom; i++)
        {
            for (var j = left; j < length; j++)
            {
                Console.Write(grid[i,j]);
            }
        
            Console.WriteLine();
        }
    }
    
    private static void UpdateConsole(char[,] grid,Coordinates current, Coordinates prior, int left)
    {
        if (prior.X == 500 && prior.Y == 0) return;
        Thread.Sleep(100);
        if (prior.X < left || prior.X > grid.GetLength(1))
            return;
        Console.SetCursorPosition(( prior.X - left), (prior.Y) + 3);
        Console.Write(grid[prior.Y, prior.X]);

        if (current.X < left || current.X > grid.GetLength(1))
            return;
        Console.SetCursorPosition(( current.X - left), (current.Y) + 3);
        Console.Write(grid[current.Y, current.X]);
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

