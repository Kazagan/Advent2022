namespace AdventOfCode2022.Helpers;

public static class Print
{
    public static void Grid<T>(T[,] grid)
    {
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j]);
            }
            Console.WriteLine();
        }
    }
    public static void Grid<T>(T[][] grid)
    {
        foreach (var t in grid)
        {
            foreach (var t1 in t)
            {
                Console.Write(t1);
            }

            Console.WriteLine();
        }
    }
}