using System.ComponentModel.Design;

namespace AdventOfCode2022.Days;

public class Day10
{
    private const string MyFile = "./TextFiles/Day10/Input.txt";
    public int Solution()
    {
        using var reader = new StreamReader(MyFile);
        int tick = 0, x = 1, result = 0;
        var process = new Queue<string>();
        var interestingCycles = new[] { 20, 60, 100, 140, 180, 220 };
        var screen = new char[6, 40];
        int i = 0;
        while (true)
        {
            var screenPosition = tick - (40 * (i));
            screen[i, screenPosition] = screenPosition >= x - 1 && screenPosition <= x + 1 ? '#' : '.';

            if (interestingCycles.Contains(tick))
                result += (tick * x);
            
            var line = reader.ReadLine();
            QueCommand(line, process);
            
            x += HandleProcess(process.Dequeue());
            tick++;
            
            if (screenPosition == 39) i++;
            if (!process.Any() && line is null) break;
        }

        PrintScreen(screen);
        return result;
    }

    private static void PrintScreen(char[,] screen)
    {
        for (int j = 0; j < screen.GetLength(0); j++)
        {
            for (int k = 0; k < screen.GetLength(1); k++)
            {
                Console.Write(screen[j, k]);
            }

            Console.WriteLine();
        }
    }

    private static void QueCommand(string? line, Queue<string> process)
    {
        if (line is null) return;
        var enumerable = line.Split(" ");
        foreach (var s in enumerable)
            process.Enqueue(s);
    }

    private static int HandleProcess(string dequeue)
    {
        return dequeue switch
        {
            "noop" => 0,
            "addx" => 0,
            _ => int.Parse(dequeue)
        };
    }
}