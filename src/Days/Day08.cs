namespace AdventOfCode2022.Days;

public class Day08
{
    private const string MyFile = "./TextFiles/Day08/Input.txt";

    public int Solution1()
    {
        var grid = new Grid(File.ReadAllLines(MyFile));
        var visible = (grid.Columns * 2) + (grid.Rows - 2) * 2;
        for (var i = 1; i < grid.Columns - 1; i++)
        {
            for (var j = 1; j < grid.Rows - 1; j++)
            {
                if (IsVisibleVertical(grid, (i, j)) || IsVisibleHorizontal(grid, (i, j)))
                    visible++;
            }
        }
        
        return visible;
    }

    public int Solution2()
    {
        var grid = new Grid(File.ReadAllLines(MyFile));
        var max = 0;
        for (var i = 1; i < grid.Columns - 1; i++)
        {
            for (var j = 1; j < grid.Rows - 1; j++)
            {
                var visible = VisibleVertical(grid, (i, j)) * VisibleHorizontal(grid, (i, j));
                max = max < visible ? visible : max;
            }
        }
        
        return max;
    }

    private static bool IsVisibleHorizontal(Grid grid, (int x, int y) cord)
    {
        var tree = grid.Forest[cord.x][cord.y];
        var isVisible = true;
        for(var i = 0; i < cord.y; i++)
        {
            if (tree > grid.Forest[cord.x][i]) continue;
            isVisible = false;
            break;
        }
        if (isVisible)
            return isVisible;
            
        isVisible = true;
        for(var i = cord.y+1; i < grid.Columns; i++)
        {
            if (tree > grid.Forest[cord.x][i]) continue;
            isVisible = false;
            break;
        }

        return isVisible;
    }

    private static bool IsVisibleVertical(Grid grid, (int x, int y) cord)
    {
        var tree = grid.Forest[cord.x][cord.y];
        var isVisible = true;
        for(var i = 0; i < cord.x; i++)
        {
            if (tree > grid.Forest[i][cord.y]) continue;
            isVisible = false;
            break;
        }
        if (isVisible)
            return isVisible;
            
        isVisible = true;
        for(var i = cord.x+1; i < grid.Rows; i++)
        {
            if (tree > grid.Forest[i][cord.y]) continue;
            isVisible = false;
            break;
        }

        return isVisible;
    }

    private static int VisibleHorizontal(Grid grid, (int x, int y) cord)
    {
        var tree = grid.Forest[cord.x][cord.y];
        var visibleLeft = 0;
        for(var i = cord.y - 1; i >= 0; i--)
        {
            visibleLeft++;
            if (grid.Forest[cord.x][i] >= tree)
                break;
        }
        var visibleRight = 0;
        for(var i = cord.y+1; i < grid.Columns; i++)
        {
            visibleRight++;
            if (grid.Forest[cord.x][i] >= tree)
                break;
        }

        return visibleLeft * visibleRight;
    }
    
    private static int VisibleVertical(Grid grid, (int x, int y) cord)
    {
        var tree = grid.Forest[cord.x][cord.y];
        var visibleUp = 0;
        for(var i = cord.x - 1; i >= 0; i--)
        {
            visibleUp++;
            if (grid.Forest[i][cord.y] >= tree)
                break;
        }
        var visibleDown = 0;
        for(var i = cord.x+1; i < grid.Rows; i++)
        {
            visibleDown++;
            if (grid.Forest[i][cord.y] >= tree)
                break;
        }
        
        
        return visibleUp * visibleDown;
    }
}

internal struct Grid
{
    public string[] Forest { get; }
    public int Columns { get; }
    public int Rows { get; }
        
    public Grid(string[] forest) 
    {
        Forest = forest;
        Columns = forest.Length;
        Rows = forest[0].Length;
    } 

}