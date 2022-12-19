namespace AdventOfCode2022.Helpers;

internal class Coordinates
{
    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { get; set; }
    public int Y { get; set;  }

    public static Coordinates operator +(Coordinates a) => a;
    public static Coordinates operator -(Coordinates a) => new (-a.X, -a.Y);
    public static Coordinates operator +(Coordinates a, Coordinates b) => new (a.X + b.X, a.Y + b.Y);
    public static Coordinates operator +(Coordinates a, (int X, int Y) t) => new(a.X + t.X, a.Y + t.Y);
    public static Coordinates operator -(Coordinates a, Coordinates b) => a + (-b);
    public static bool operator ==(Coordinates a, Coordinates b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Coordinates a, Coordinates b) => !(a==b);
    public bool Equals(Coordinates other)
    {
        return X == other.X && Y == other.Y;
    }
    public bool Equals((int X, int Y) t)
    {
        return X == t.X && Y == t.Y;
    }
    public override bool Equals(object? obj)
    {
        return obj is Coordinates other && Equals(other);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}