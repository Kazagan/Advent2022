namespace AdventOfCode2022.Days;

public class Day12
{
    private const string MyFile = "./TextFiles/Day12/Input.txt";

    public int Solution()
    {
        var nodes = ParseFile(ValidHeight).ToArray();
        var start = nodes.Single(x => x.Height == 'S');
        Dijkstra(start);
        return nodes.Single(x => x.Height == 'E').Distance;
    }

    public int Solution2()
    {
        var nodes = ParseFile(ValidHeightDown).ToArray();
        nodes.Single(x => x.Height == 'S').Height = 'a';
        var start = nodes.Single(x => x.Height == 'E');
        Dijkstra(start);
        var allA = nodes.Where(x => x.Height == 'a');
        return allA.MinBy(x => x.Distance)!.Distance;
    }

    private static void Dijkstra(Node start)
    {
        start.Distance = 0;
        var toVisit = new List<Node> { start };
        while(toVisit.Any())
        {
            var smallest = toVisit.MinBy(x => x.Distance)!;
            toVisit.Remove(smallest);
            foreach (var neighbor in smallest.Neighbors)
            {
                var currentDistance = smallest.Distance + 1; // Every neighbor is 1 away
                if (currentDistance >= neighbor.Distance) continue;
                neighbor.Distance = currentDistance;
                toVisit.Add(neighbor);
            }
        }
    }
    
    private IEnumerable<Node> ParseFile(Func<Node, Node, bool> heightRule)
    {
        var nodes = File.ReadAllLines(MyFile)
            .Select(line =>
                line.Select(height => new Node(height)).ToArray()).ToArray();
        SetNeighbors(nodes, heightRule);
        return nodes.SelectMany(x => x);
    }

    private static void SetNeighbors(IReadOnlyList<IReadOnlyList<Node>> nodes, Func<Node, Node,bool> heightRule)
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            for (var j = 0; j < nodes[i].Count; j++)
            {
                nodes[i][j].Neighbors = GetValidNeighbors(nodes, i, j, heightRule);
            }
        }
    }

    private static IEnumerable<Node> GetValidNeighbors(IReadOnlyList<IReadOnlyList<Node>> nodes, int i, int j, Func<Node, Node,bool> heightRule)
    {
        var neighbors = new List<Node>();
        //up
        if (i > 0 && heightRule(nodes[i][j], nodes[i - 1][j]))
            neighbors.Add(nodes[i - 1][j]);
        //down
        if (i + 1 < nodes.Count && heightRule(nodes[i][j], nodes[i + 1][j]))
            neighbors.Add(nodes[i + 1][j]);
        //left/
        if (j > 0 && heightRule(nodes[i][j], nodes[i][j-1]))
            neighbors.Add(nodes[i][j-1]);
        //right
        if (j + 1 < nodes[i].Count && heightRule(nodes[i][j], nodes[i][j+1]))
            neighbors.Add(nodes[i][j+1]);
        return neighbors;
    }

    private static bool ValidHeight(Node first, Node second)
    {
        return GetHeight(first) + 1 >= GetHeight(second);
    }

    private static bool ValidHeightDown(Node first, Node second)
    {
        return GetHeight(second) + 1 >= GetHeight(first);
    }

    private static char GetHeight(Node node)
    {
        var firstHeight = node.Height;
        if (firstHeight < 'a')
            firstHeight = node.Height == 'S' ? 'a' : 'z';
        return firstHeight;
    }
}

internal class Node
{
    private int _distance = int.MaxValue;
    public char Height { get; set; }
    public IEnumerable<Node> Neighbors { get; set; }
    public Node(char height) => Height = height;

    public int Distance
    {
        get => _distance;
        set => _distance = value < _distance ? value : _distance;
    }
}