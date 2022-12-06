namespace AdventOfCode2022.Extensions;

public static class StackExtensions
{
    public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int count)
    {
        var result = new List<T>();
        for (int i = count - 1; i >= 0; i--)
        {
            result[i] = stack.Pop();
        }

        return result;
    }
    
    public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> range)
    {
        foreach (var r in range)
        {
            stack.Push(r);
        }
    }
}