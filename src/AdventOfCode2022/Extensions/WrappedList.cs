namespace AdventOfCode2022.Extensions;

public class WrappedList<T>
{
    private T[] _array;
    private int _next;
    private int _size;

    public int Count => _size;
    

    public WrappedList()
    {
        _array = Array.Empty<T>();
    }

    public WrappedList(IEnumerable<T> collection)
    {
        if (collection is null)
            throw new ArgumentException(nameof(collection));

        _array = collection.ToArray();
        _size = collection.Count();
    }

    public T Next()
    {
        if (_next == _size)
            _next = 0;

        return _array[_next++];
    }
}