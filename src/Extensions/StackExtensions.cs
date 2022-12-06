namespace AdventOfCode2022.Extensions;

public static class StackExtensions
{
    /// <summary>
    /// Pop off n number of elements as if lifting from bottom of n element
    /// </summary>
    /// <returns>IEnumerable</returns>
    public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int count)
    {
        var result = new T[count];
        for (int i = count - 1; i >= 0; i--)
        {
            result[i] = stack.Pop();
        }

        return result;
    }
    
    /// <summary>
    /// Push n elements as if off a forklift.
    /// </summary>
    public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> range)
    {
        foreach (var r in range)
        {
            stack.Push(r);
        }
    }
}