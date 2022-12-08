namespace AdventOfCode2022.Helpers;

public class MyFile
{
    public MyFile(string name, bool isFolder)
    {
        Name = name;
        IsFolder = isFolder;
        _files = new HashSet<MyFile>();
    }

    private readonly int _size;
    private readonly HashSet<MyFile> _files;
    public string Name { get; }

    public bool IsFolder { get; }
    public MyFile? Container { get; private set; }

    public int Size
    {
        get => IsFolder ? _files.Sum(x => x.Size) : _size;
        init => _size = value;
    }

    public void AddFile(MyFile file)
    {
        if (!IsFolder)
            throw new Exception($"Can't add files to {Name}");
        file.Container = this;
        _files.Add(file);
    }

    public IEnumerable<MyFile> GetFiles()
    {
        return _files;
    }

    public override string ToString()
    {
        if (IsFolder)
            return $"dir {Name}";
        return $"{_size} {Name}";
    }
    
    public static void RecursiveList(MyFile file, int level = 0)
    {
        var line = "";
        for (var i = 0; i < level; i++) line += "  ";
        line += $"- {file.Name}";
        line += file.IsFolder ? $" (dir {file.Size})" : $" (file {file.Size})";
        Console.WriteLine(line);
        if (!file.IsFolder) return;
        foreach (var f in file._files)
        {
            RecursiveList(f, level + 1);
        }
    }

    public static int SearchBelowFolderSize(MyFile file, int size)
    {
        var output = 0;
        if (!file.IsFolder)
            return 0;
        if (file.Size < size)
            output += file.Size;
        output += file._files.Sum(f => SearchBelowFolderSize(f, size));
        return output;
    }
}