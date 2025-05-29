// Google Second Interview // Topological Sort // DFS using Stack // Graph // Build Order
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreesAndGraphs
{
    // Given a list of reports about the Runners in a game
    // Build list of the order of the runners
    // Reports = New List<Report> { new Report { Faster = "A", Slower = "D" },
    //                              new Report { Faster = "F", Slower = "B" },
    //                              new Report { Faster = "B", Slower = "D" },
    //                              new Report { Faster = "F", Slower = "A" },
    //                              new Report { Faster = "D", Slower = "C" },
    //           }
    // { { "a", "d" }, { "f", "b" }, { "b", "d" }, { "f", "a" }, { "d", "c" } };
    // Runners = { A, B, C, D, F}
    ////      ---> A ---> 
    ////    /             \
    ////   /          edge \
    //// F                   ---> D ---> C   
    ////   \               /       
    ////    \             /      
    ////      ---> B --->   
    // Result = { F, A, B, D, C}
    public class FasterSlowerRunner_Google
    {
        public class Report
        {
            public string Faster { get; set; }
            public string Slower { get; set; }
        }

        public class Graph
        {
            public List<Node> Nodes { get; set; }
            public Dictionary<string, Node> map { get; set; }

            public Graph()
            {
                Nodes = new List<Node>();
                map = new Dictionary<string, Node>();
            }

            public void AddNode(string name)
            {
                if (!map.ContainsKey(name))
                {
                    Node newNode = new Node(name);
                    Nodes.Add(newNode);
                    map.Add(name, newNode);
                }
            }

            public void AddEdge(string startNodeName, string endNodeName)
            {
                Node startNode = map[startNodeName];
                Node endNode = map[endNodeName];

                startNode.AddNeighbor(endNode);
            }
        }

        public class Node
        {
            public string Name { get; set; }
            public List<Node> Children { get; set; }
            public Status Status { get; set; }

            public Node(string name)
            {
                Name = name;
                Children = new List<Node>();
                Status = Status.NotVisited;
            }

            public void AddNeighbor(Node node)
            {
                if (!Children.Contains(node))
                {
                    Children.Add(node);
                }
            }
        }

        public enum Status
        {
            NotVisited,
            Visiting, // Cycle Detection
            Visited
        }

        // O(n + m)  where n is Count of nodes and m is Count of edges 
        // O(n) space
        public static string[] RunnersInOrder(List<Report> reports)
        {
            // 1. Finding the runners list from the input reports list
            HashSet<string> runners = new HashSet<string>();

            foreach (Report report in reports)
            {
                if (!runners.Contains(report.Faster))
                    runners.Add(report.Faster);

                if (!runners.Contains(report.Slower))
                    runners.Add(report.Slower);
            }

            // Finding the result length from the runners list Count
            string[] result = new string[runners.Count];

            // 2. Build a directed Graph containing Nodes and Edges
            Graph graph = new Graph();

            // Add Nodes // O(n)
            foreach (string runner in runners)
            {
                graph.AddNode(runner);
            }

            // Add Edges // O(m)
            foreach (Report report in reports)
            {
                graph.AddEdge(report.Faster, report.Slower);
            }

            // 3. Build Order
            Stack<Node> stack = new Stack<Node>();

            foreach (Node node in graph.Nodes)
            {
                if (node.Status == Status.NotVisited)
                {
                    DFS(node, stack);
                }
            }

            int i = 0;

            // At last when we pop from the stack, it's in order
            // LIFO
            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                result[i] = node.Name;
                i++;
            }

            return result;
        }

        // For each node we do DFS and we go all the way down to the end of the path that there's no node after that
        // and we push that to the stack and backwards we continue pushing to stack
        public static bool DFS(Node node, Stack<Node> stack)
        {
            if (node.Status == Status.Visiting)
                throw new InvalidOperationException("Cycle detected!");

            if (node.Status == Status.Visited)
                return true;

            node.Status = Status.Visiting;

            foreach (Node child in node.Children)
            {
                DFS(child, stack);
            }

            node.Status = Status.Visited;
            stack.Push(node);

            return true;
        }

        // Chatgpt version
        static List<string> GetRunnerOrderDFS(List<Report> reports, List<String> runners)
        {
            // Step 1: Build graph
            var graph = new Dictionary<string, List<string>>();

            foreach (var runner in runners)
            {
                graph[runner] = new List<string>();
            }

            foreach (var report in reports)
            {
                graph[report.Faster].Add(report.Slower);
            }

            // Step 2: DFS with vidited tracking
            var visited = new HashSet<string>();
            var visiting = new HashSet<string>(); // for cycle detection
            var result = new Stack<string>();

            foreach (var runner in runners)
            {
                if (!visited.Contains(runner))
                {
                    if (!DFS(runner, graph, visited, visiting, result))
                    {
                        throw new InvalidOperationException("Cycle detected! Invalid runner reports.");
                    }
                }
            }

            return result.ToList(); // Toplogical order
        }

        static bool DFS(string node, Dictionary<string, List<string>> graph, HashSet<string> visited, HashSet<string> visiting, Stack<string> result)
        {
            if (visiting.Contains(node))
            {
                return false; // Cycle detected
            }

            if (visited.Contains(node))
            {
                return true; // Already processed
            }

            visiting.Add(node);

            foreach (var neighbor in graph[node])
            {
                if (!DFS(neighbor, graph, visited, visiting, result))
                {
                    return false;
                }
            }

            visiting.Remove(node);
            visited.Add(node);
            result.Push(node);

            return true;
        }

        public List<string> GetRunnerOrderBFS(List<Report> reports, List<string> runners)
        {
            // Step 1: Build Graph
            var graph = new Dictionary<string, List<string>>();
            var inDegree = new Dictionary<string, int>();

            // Initialize graph and inDegree for all runners
            foreach (var runner in runners)
            {
                graph[runner] = new List<string>();
                inDegree[runner] = 0;
            }

            // Add edges and update in-degrees
            foreach (var report in reports)
            {
                graph[report.Faster].Add(report.Slower);
                inDegree[report.Slower]++;
            }

            // Step 2: Initialize queue with nodes having in-degree 0
            var queue = new Queue<string>(inDegree.Where(kv => kv.Value == 0).Select(kv => kv.Key));
            var result = new List<string>();

            // Step 3: Process the queue
            while (queue.Count > 0)
            {
                var runner = queue.Dequeue();
                result.Add(runner);

                foreach (var neighbor in graph[runner])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                        queue.Enqueue(neighbor);
                }
            }

            // Step 4: Validate result (cycle check)
            if (result.Count != runners.Count)
            {
                throw new InvalidOperationException("Cycle detected! Invalid runner reports.");
            }

            return result;
        }
    }
}
