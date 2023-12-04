namespace AdventOfCode2022.Helpers;

internal record Coordinates(int X, int Y)
{

    public static Coordinates operator +(Coordinates a) => a;
    public static Coordinates operator -(Coordinates a) => new (-a.X, -a.Y);
    public static Coordinates operator +(Coordinates a, Coordinates b) => new (a.X + b.X, a.Y + b.Y);
    public static Coordinates operator +(Coordinates a, (int X, int Y) t) => new(a.X + t.X, a.Y + t.Y);
    public static Coordinates operator -(Coordinates a, Coordinates b) => a + (-b);
}