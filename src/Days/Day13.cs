namespace AdventOfCode2022.Days;

public class Day13
{
    private const string MyFile = "./TextFiles/Day13/Input.txt";

    public int Solution()
    {
        using var reader = new StreamReader(MyFile);
        var lines = GetLines(reader);
        var result = 0;
        var pair = 1;
        for(var i = 0; i < lines.Count; i += 2)
        {
            if (ValidateOrder(lines[i], lines[i + 1]) > 0)
                result += pair;
            pair++;
        }
        return result;
    }
    
    public int Solution2()
    {
        using var reader = new StreamReader(MyFile);
        var lines = GetLines(reader);

        using var x = new StringReader("[2]]");
        using var y = new StringReader("[6]]");
        var first = GetList(x);
        var second = GetList(y);
        lines.Add(first);
        lines.Add(second);
        lines.Sort(ValidateOrder);
        lines.Reverse();
        return (lines.IndexOf(first) + 1) * (lines.IndexOf(second) + 1);
    }

    private static List<object> GetLines(StreamReader reader)
    {
        int character;
        var output = new List<object>();
        while ((character = reader.Read()) != -1)
        {
            // x += $"{character}: {((char)character == '\n' ? 'n' : (char)character)}\n";
            if (character != '[')
                continue;
            output.Add(GetList(reader));
            reader.Read(); // Move to next line
            reader.Read(); // move past [
            output.Add(GetList(reader));
        }

        return output;
    }

    private static int ValidateOrder(object left, object right)
    {
        return (left, right) switch
        {
            (int, int) => (int)right - (int)left,
            (List<object>, int) => Compare((List<object>)left, new List<object>() { right }),
            (int, List<Object>) => Compare(new List<object>() { left }, (List<object>)right),
            _ => Compare((List<object>)left, (List<object>)right)
        };
    }

    private static int Compare(IReadOnlyList<object> left, IReadOnlyList<object> right)
    {
        int result;
        using var l = left.GetEnumerator();
        using var r = right.GetEnumerator();
        while(l.MoveNext() && r.MoveNext())
            if ((result = ValidateOrder(l.Current, r.Current)) != 0)
                return result;
        return right.Count - left.Count;
    }
    
    private static List<object> GetList(TextReader line)
    {
        var output = new List<object>();
        
        char character;
        while ((character = (char)line.Read()) != ']')
        {
            switch (character)
            {
                case '[':
                    output.Add(GetList(line));
                    break;
                case ',':
                    break;
                default:
                    output.Add(GetNumber(line, character));
                    break;
            }
        }

        return output;
    }

    private static int GetNumber(TextReader line, char character)
    {
        string number = character.ToString();
        while (IsNumber((char)line.Peek()))
        {
            character = (char)line.Read();
            number += character;
        }
        return int.Parse(number);
    }

    private static bool IsNumber(char next)
    {
        return next is >= '0' and <= '9';
    }
}