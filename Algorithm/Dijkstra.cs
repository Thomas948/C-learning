using System;
using System.Collections.Generic;

namespace Algorithm
{
    public class Dijkstra
    {
        private static Dictionary<string, Dictionary<string, int>> graph =
            new Dictionary<string, Dictionary<string, int>>
            {
                { "start", new Dictionary<string, int> { { "a", 6 }, { "b", 2 } } },
                { "a", new Dictionary<string, int> { { "fin", 1 } } },
                { "b", new Dictionary<string, int> { { "a", 3 }, { "fin", 5 } } },
                { "fin", new Dictionary<string, int>() }
            };

        public static Dictionary<string, int> costs = new Dictionary<string, int>
        {
            { "a", 6 }, { "b", 2 }, { "fin", int.MaxValue }
        };

        private static Dictionary<string, string> parents = new Dictionary<string, string>
        {
            { "a", "start" }, { "b", "start" }, { "fin", null }
        };

        private static List<string> processed = new List<string>();

        public static void CalculateLowestCost()
        {
            var node = FindLowestCostNode(costs);
            while (node != null)
            {
                var cost = costs[node];
                var neighbors = graph[node];
                foreach (var neighbor in neighbors.Keys)
                {
                    var newCost = cost + neighbors[neighbor];
                    if (newCost >= costs[neighbor]) continue;
                    costs[neighbor] = newCost;
                    parents[neighbor] = node;
                }
                processed.Add(node);
                node = FindLowestCostNode(costs);
            }
        }

        private static string FindLowestCostNode(Dictionary<string, int> costNodes)
        {
            var lowestCost = int.MaxValue;
            string lowestCostNode = null;
            foreach (var (node, cost) in costNodes)
            {
                if (cost >= lowestCost || processed.Contains(node)) continue;
                lowestCost = cost;
                lowestCostNode = node;
            }

            return lowestCostNode;
        }
    }
}