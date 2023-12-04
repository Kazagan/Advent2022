namespace AdventOfCode2023.Days;

public static class Day01
{

    private static Dictionary<string, string> Numbers => new()
    {
        {"one", "1"}, {"two", "2"}, {"three", "3"}, {"four", "4"}, {"five", "5"}, {"six", "6"}, {"seven", "7"}, {"eight", "8"}, {"nine", "9"},
    };
    
    public static int Solution(string file)
    {
        var asDigits = File.ReadLines(file)
            .Select(line => GetFirstNumber(line) + GetLastNumber(line)).ToList();
        return asDigits
            .Sum(int.Parse);
    }

    private static string GetFirstNumber(string input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
                return input[i].ToString();

            var current = input[i..];
            var keys = Numbers.Keys.Where(x => x.Length <= current.Length).ToList();
            
            var number = keys.SingleOrDefault(n => n.Equals(current[..n.Length]));
            
            if (number is null)
                continue;

            return Numbers[number];
        }

        throw new Exception($"string {input} contained no numbers represented as text or digit");
    }
    
    private static string GetLastNumber(string input)
    {
        for (var i = input.Length - 1; i >= 0; i--)
        {
            var current = input[i..];
            var keys = Numbers.Keys.Where(x => x.Length <= current.Length).ToList();

            if (char.IsDigit(current.First()))
                return current.First().ToString();
            
            var number = keys.SingleOrDefault(n => n.Equals(current[..n.Length]));
            
            if (number is null)
                continue;

            return Numbers[number];
        }

        throw new Exception($"string {input} contained no numbers represented as text or digit");
    }
}