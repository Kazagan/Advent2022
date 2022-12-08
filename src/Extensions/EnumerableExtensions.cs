using System.Collections;
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

    public static IEnumerable<T> Flatten<T>(this IEnumerable<T> collection)
    {
        foreach (var obj in collection)
        {
            if (obj is IEnumerable and not T)
                foreach (var t in Flatten((IEnumerable<T>)obj))
                    yield return t;
            
            yield return (T)obj;
        }
    }
    
    public static IEnumerable<T> Flatten<T>(this IEnumerable<T> collection, Func<T, IEnumerable<T>> func)
    {
        foreach (var obj in collection)
        {
            IEnumerable<T> x;
            if ((x = func(obj)) is not null  and not T)
                foreach (var t in x.Flatten(func))
                    yield return t;
            
            yield return obj;
        }
    }
}