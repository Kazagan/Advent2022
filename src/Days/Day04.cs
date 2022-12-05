
namespace AdventOfCode2022.Days;

public class Day04
{
    private const string MyFile = "./TextFiles/Day04/Input.Txt";

    public int Solution()
    {
        var reader = new StreamReader(MyFile);
        var count = 0;
        while (reader.ReadLine() is { } line)
        {
            var blocks = line.Split(",");
            var times = blocks
                .Select(x => x.Split("-")
                    .Select(int.Parse));
            if (ContainsAtAll(times))
                count++;
        }

        return count;
    }

    private bool FullyContains(IEnumerable<IEnumerable<int>> blocks)
    {
        var x = blocks.Select(x => x.ToArray()).ToArray();
        if (x[0][0] >= x[1][0] && x[0][1] <= x[1][1])
            return true;
        if (x[1][0] >= x[0][0] && x[1][1] <= x[0][1])
            return true;
        return false;
    }
    
    private bool ContainsAtAll(IEnumerable<IEnumerable<int>> blocks)
    {
        var x = blocks.Select(x => x.ToArray()).ToArray();
        if ( (x[0][0] >= x[1][0] && x[0][0] <= x[1][1]) || (x[0][1] >= x[1][0] && x[0][1] <= x[1][1]))
            return true;
        if ( (x[1][0] >= x[0][0] && x[1][0] <= x[0][1]) || (x[1][1] >= x[0][0] && x[1][1] <= x[0][1]))
            return true;
        return false;
    }
}