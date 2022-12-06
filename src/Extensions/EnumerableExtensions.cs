using BenchmarkDotNet.Attributes;

namespace AdventOfCode2022.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, Func<T, bool> shouldChunk)
    {
        var chunk = new List<T>();
        foreach (var line in source)
        {
            if (shouldChunk(line))
            {
                yield return new List<T>(chunk);
                chunk.Clear();
                continue;
            }

            chunk.Add(line);
        }
    }
}