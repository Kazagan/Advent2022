using System.Collections.Immutable;

namespace AdventOfCode2022.Days;

public class Day16
{
    private const string MyFile = "./TextFiles/Day16/Input.Txt";

    public int Solution()
    {
        var valves = File.ReadLines(MyFile)
            .Select(Valve.Parse).ToList();
        const string start = "AA";
        // Get Dikstra for each valve to other valves
        var simple = SimplifyGraph(valves, start).ToDictionary(x => x.Name);
        // Get all valves other than start, sorted
        var open = simple.Keys.Where(x => x != start).ToImmutableSortedSet(); 
            
        var result = Search(start, simple, open, 30);

        return result;
    }
    
    public int Solution2()
    {
        var valves = File.ReadLines(MyFile)
            .Select(Valve.Parse).ToList();
        const string start = "AA";
        // Get Dikstra for each valve to other valves
        var simple = SimplifyGraph(valves, start).ToDictionary(x => x.Name);
        // Get all valves other than start, sorted
        var open = simple.Keys.Where(x => x != start).ToImmutableSortedSet(); 
            
        var result = Search(start, simple, open, 26, 2);

        return result;
    }

    private int Search(string start, Dictionary<string, Valve> simple, ImmutableSortedSet<string> valves, int time, int valveOpeners = 1)
    {
        var cache = new Dictionary<(string current, string openValve, int timeLeft, int openers), int>();
        return Traverse(start, valves, time, valveOpeners - 1);
        
        int Traverse(string current, ImmutableSortedSet<string> open, int remaining, int openersLeft)
        {
            // If all valves open, return 0;
            if (!open.Any())
                return 0;

            var cacheKey = (current, string.Concat(open), remaining, openersLeft);
            if(cache.TryGetValue(cacheKey, out var value))
                return value;
    
            var best = 0;
            foreach (var next in open)
            {
                //cost of opening, distance + 1 for minute to open
                var openingCost = simple[current].Distances[next] + 1;
                var afterOpen = remaining - openingCost;
    
                if (afterOpen <= 0) continue;
                // Total pressure released from now to minute 30.
                var total = afterOpen * simple[next].Flow;
                // recursively check all routes.
                total += Traverse(next, open.Remove(next), afterOpen, openersLeft);
                // if current route better than last
                if (total > best)
                    best = total;
            }
    
            if (openersLeft > 0)
            {
                var total = Traverse(start, open, time, openersLeft - 1);
                if (total > best)
                    best = total;
            }
    
            return cache[cacheKey] = best;
        }
    }

    private IEnumerable<Valve> SimplifyGraph(IList<Valve> valves, string start)
    {
        // Lookup for valve, and tunnels
        var tunnels = valves.SelectMany(x => x.Tunnels, (valve, s) => (valve.Name, s)).ToLookup(x => x.Name, x => x.s);
        // dictionary of all valves that are either at the start, or have a flow greater than 0
        var hasFlow = valves.Where(x => x.Name == start || x.Flow > 0).ToDictionary(x => x.Name);

        foreach (var valve in hasFlow.Values)
        {
            yield return valve with
            {
                // Distance to each valve from current valve
                Distances = Dijkstra(valve.Name, x => tunnels[x])
                    .Where(x => hasFlow.Keys.Contains(x.Item1))
                    .ToDictionary(x => x.Item1, x => x.Item2)
            };
        }
    }

    private IEnumerable<(string, int)> Dijkstra(string valve, Func<string, IEnumerable<string>> lookup)
    {
        var distances = new Dictionary<string, int> { { valve, 0 } };

        var toVisit = new PriorityQueue<string, int>();
        toVisit.Enqueue(valve, 0);
        while (toVisit.Count != 0)
        {
            var current = toVisit.Dequeue();
            foreach (var tunnel in lookup(current))
            {
                var currentDistance = distances[current] + 1;
                if (currentDistance >= distances.GetValueOrDefault(tunnel, int.MaxValue)) continue;
                distances[tunnel] = currentDistance;
                toVisit.Enqueue(tunnel, currentDistance);
            }
        }

        return distances.Where(x => x.Key != valve).Select(x => (x.Key, x.Value));
    }
}

internal record Valve(string Name, int Flow, ImmutableArray<string> Tunnels)
{
    public Dictionary<string, int> Distances = new Dictionary<string, int>();

    public static Valve Parse(string line)
    {
        var words = line.Split(" ");
        var name = words[1];
        var flow = int.Parse(words[4].Split("=")[1].TrimEnd(';'));
        var tunnels = words[9..].Select(ParseTunnels).ToImmutableArray();
        return new Valve(name, flow, tunnels);
    }

    private static string ParseTunnels(string tunnel)
    {
        return tunnel.TrimEnd(',');
    }
}