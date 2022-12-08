using AdventOfCode2022.Extensions;
using AdventOfCode2022.Helpers;

namespace AdventOfCode2022.Days;

public class Day07
{
    private const string ReadFile = "./TextFiles/Day07/Input.Txt";
    // private static readonly MyFile Root = new MyFile("/", true);
    private MyFile _context = new MyFile("/", true);

    public int Solution1()
    {
        MyFile root = new MyFile("/", true);
        _context = root;
        using var reader = new StreamReader(ReadFile);
        ParseFile(reader);
        MyFile.RecursiveList(root);
        return MyFile.SearchBelowFolderSize(root, 100000);
    }

    public int Solution2()
    {
        MyFile root = new MyFile("/", true);
        _context = root;
        using var reader = new StreamReader(ReadFile);
        ParseFile(reader);
        var currentSize = 70000000 - root.Size;
        var toDelete = 30000000 - currentSize;
        return SmallestToDelete(root, toDelete);
    }

    private int SmallestToDelete(MyFile file, int toDelete)
    {
        var x = file.GetFiles().Flatten(x => x.GetFiles());
        return x.Where(myFile => myFile.Size > toDelete).Min(myFile => myFile.Size);
    }

    private void HandleCommand(string command, TextReader reader)
    {
        var line = command.Split(" ");
        switch (line[0])
        {
            case "cd":
                _context = ChangeDirectories(line.Length == 1 ? "/" : line[1]);
                break;
            case "ls":
                ParseFile(reader);
                break;
        }
    }

    private void ParseFile(TextReader reader)
    {
        while (reader.ReadLine() is { } line)
        {
            if (line[0] == '$')
                HandleCommand(line[2..], reader);
            var x = line.Split(" ");
            if (x[0] == "dir")
                _context.AddFile(new MyFile(x[1], isFolder: true));
            if (!int.TryParse(x[0], out var size)) continue;
            var myFile = new MyFile(x[1], isFolder: false) { Size = size };
            _context.AddFile(myFile);
        }
    }

    private MyFile ChangeDirectories(string directory)
    {
        return directory switch
        {
            ".." => _context.Container ?? _context,
            "." => _context,
            "/" => _context.Name != "/" ? ChangeDirectories("..") : _context,
            _ => _context.GetFiles().Single(x => x.Name == directory)
        };
    }
}

