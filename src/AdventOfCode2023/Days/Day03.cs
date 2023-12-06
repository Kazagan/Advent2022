using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;

namespace AdventOfCode2023.Days;

public static class Day03
{
    public static int Part1(string file)
    {
        var lines = File.ReadAllLines(file);
        Dictionary<(int X, int Y), int> numbers = new();
        
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var value = lines[i][j];
                if (value == '.' || char.IsDigit(value)) continue;
                AddNumbersByCoordinates(lines, i, j, numbers);
            }
        }
        
        return numbers.Sum(x => x.Value);
    }

    public static int Part2(string file)
    {
        var lines = File.ReadAllLines(file);
        List<int> numbers = new();
        
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var value = lines[i][j];
                if (value != '*') continue;
                var adjacent = Adjacent(lines, i, j);
                
                if (adjacent.Count() != 2) continue;
                
                numbers.Add(adjacent.Aggregate((a, b) => a * b));
            }
        }

        return numbers.Sum();
    }

    private static void AddNumbersByCoordinates(string[] lines, int i, int j,
        Dictionary<(int X, int Y), int> dictionary)
    {
        var bottom = i > 0 ? i - 1 : i;
        var top = i < lines.Length - 1 ? i + 1 : i;
        var left = j > 0 ? j - 1 : j;
        var right = j < lines[i].Length - 1 ? j + 1 : j;
            
        for(int x = bottom; x <= top; x++)
        {
            for (int y = left; y <= right; y++)
            {
                if (!char.IsDigit(lines[x][y])) continue;
                
                var begining = y;
                var end = y;
                while (begining >= 0 && char.IsDigit(lines[x][begining])) 
                    begining--;
                while (end < lines[x].Length && char.IsDigit(lines[x][end]))
                    end++;

                var number = lines[x][(begining + 1)..(end)];
                
                if(dictionary.ContainsKey((x, begining+1)))
                    continue;
                
                dictionary.Add((x, begining+1), int.Parse(number));
            }
        }
    }
    
    private static IEnumerable<int> Adjacent(string[] lines, int i, int j)
    {
        var result = new List<int>();
        var startCoordinates = new List<(int, int)>();
        
        var bottom = i > 0 ? i - 1 : i;
        var top = i < lines.Length - 1 ? i + 1 : i;
        var left = j > 0 ? j - 1 : j;
        var right = j < lines[i].Length - 1 ? j + 1 : j;
            
        for(int x = bottom; x <= top; x++)
        {
            for (int y = left; y <= right; y++)
            {
                if (!char.IsDigit(lines[x][y])) continue;
                
                var begining = y;
                var end = y;
                while (begining >= 0 && char.IsDigit(lines[x][begining])) 
                    begining--;
                begining++;
                while (end < lines[x].Length && char.IsDigit(lines[x][end]))
                    end++;

                if (startCoordinates.Contains((x, begining))) continue;
                
                startCoordinates.Add((x, begining));
                var number = lines[x][(begining)..(end)];
                result.Add(int.Parse(number));
            }
        }
        
        return result;
    }
}