using Advent.Days;

namespace Days;

public interface AdventDay
{
    public int Day { get; }

    public int Solution(string filePath, int step) => step switch
    {
        1 => First(filePath),
        2 => Second(filePath),
        _ => throw new ArgumentException($"Only 2 steps per day")
    };

    protected int First(string filePath);
    protected int Second(string filePath);
}

public static class AdventDays
{
    public static List<AdventDay> Days() => new()
    {
        new Day01(),
        new Day02(),
    };
}