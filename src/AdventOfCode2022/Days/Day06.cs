namespace AdventOfCode2022.Days;

public class Day06
{
    private const string MyFile = "./src/TextFiles/Day06/Input.Txt";
    public int Solution1()
    {
        var reader = new StreamReader(MyFile);
        return Solve(reader.ReadLine()!, 4);
    }

    public int Solution2()
    {
        var reader = new StreamReader(MyFile);
        return Solve(reader.ReadLine()!, 14);
    }

    private static int Solve(string input, int marker)
    {
        for (var i = 0; i < input.Length - (marker + 1); i++)
        {
            // var values = input.Take(new Range(i, i + marker)).Distinct();
            var range = new System.Range(i, i + marker);
            var values = input.Take(range).Distinct();
            if (values.Count() != marker) continue;
            return i + marker;
        }

        return 0;
    }
    
}