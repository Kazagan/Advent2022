using Microsoft.CodeAnalysis.FlowAnalysis;

namespace AdventOfCode2022.Days;

public class Day12
{
    private const string MyFile = "./TextFiles/Day12/Example.txt";

    public int Solution()
    {
        var nodes = ParseFile().ToArray();
        var start = nodes.Single(x => x.Height == 'S');
        Dijkstra(nodes, start);
        return nodes.Single(x => x.Height == 'E').Distance;
    }

    public int Solution2()
    {
        var nodes = ParseFile().ToArray();
        nodes.Single(x => x.Height == 'S').Height = 'a';
        var start = nodes.Single(x => x.Height == 'E');
        Dijkstra(nodes, start);
        var allA = nodes.Where(x => x.Height == 'a');
        return allA.MinBy(x => x.Distance).Distance;
    }

    private void Dijkstra(IEnumerable<Node> nodes, Node start)
    {
        start.Distance = 0;
        var toVisit = new List<Node> { start };
        while(toVisit.Any())
        {
            var smallest = toVisit.MinBy(x => x.Distance)!;
            toVisit.Remove(smallest);
            foreach (var neighbor in smallest.Neighbors)
            {
                if(neighbor.Visited) continue;
                var currentDistance = smallest.Distance + 1; // Every neighbor is 1 away
                if (currentDistance >= neighbor.Distance) continue;
                neighbor.Distance = currentDistance;
                toVisit.Add(neighbor);
            }
        }
    }

    private static void PrintGraph(List<List<Node>> graph)
    {
        for (int j = 0; j < graph.Count; j++)
        {
            for (int k = 0; k < graph[j].Count; k++)
            {
                Console.Write(graph[j][k].Height);
            }

            Console.WriteLine();
        }
    }
    
    private IEnumerable<Node> ParseFile()
    {
        var nodes = File.ReadAllLines(MyFile)
            .Select(line =>
                line.Select(height => new Node(height)).ToArray()).ToArray();
        SetNeighbors(nodes);
        return nodes.SelectMany(x => x);
    }

    private void SetNeighbors(IReadOnlyList<IReadOnlyList<Node>> nodes)
    {
        for (var i = 0; i < nodes.Count; i++)
        {
            for (var j = 0; j < nodes[i].Count; j++)
            {
                nodes[i][j].Neighbors = GetValidNeighbors(nodes, i, j);
            }
        }
    }

    private IEnumerable<Node> GetValidNeighbors(IReadOnlyList<IReadOnlyList<Node>> nodes, int i, int j)
    {
        var neighbors = new List<Node>();
        //up
        if (i - 1 >= 0 && ValidHeight(nodes[i][j], nodes[i - 1][j]))
            neighbors.Add(nodes[i - 1][j]);
        //down
        if (i + 1 < nodes.Count && ValidHeight(nodes[i][j], nodes[i + 1][j]))
            neighbors.Add(nodes[i + 1][j]);
        //left/
        if (j - 1 >= 0 && ValidHeight(nodes[i][j], nodes[i][j-1]))
            neighbors.Add(nodes[i][j-1]);
        //right
        if (j + 1 < nodes[i].Count && ValidHeight(nodes[i][j], nodes[i][j+1]))
            neighbors.Add(nodes[i][j+1]);
        return neighbors;
    }

    private static bool ValidHeight(Node first, Node second)
    {
        var firstHeight = first.Height;
        var secondHeight = second.Height;
        if (first.Height is 'S' or 'E')
            firstHeight = first.Height == 'S' ? 'a' : 'z';
        if (second.Height is 'S' or 'E')
            secondHeight = first.Height == 'S' ? 'a' : 'z';
        return firstHeight + 1 >= secondHeight;
    }
}

internal class Node
{
    public Node(char height) => Height = height;

    private int _distance = int.MaxValue;
    public char Height { get; set; }
    public bool Visited { get; set; } = false;
    public IEnumerable<Node> Neighbors { get; set; }

    public int Distance
    {
        get => _distance;
        set => _distance = value < _distance ? value : _distance;
    }
}