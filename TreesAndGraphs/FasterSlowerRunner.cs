// Google Second Interview // Topological Sort // DFS using Stack // Graph // Build Order
using System.Collections.Generic;

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
    public class FasterSlowerRunner
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
            public Dictionary<string, Node> map { get; set; }
            public Status Status { get; set; }

            public Node(string name)
            {
                Name = name;
                Children = new List<Node>();
                map = new Dictionary<string, Node>();
                Status = Status.NotInStack;
            }

            public void AddNeighbor(Node node)
            {
                if (!map.ContainsKey(node.Name))
                {
                    Children.Add(node);
                    map.Add(node.Name, node);
                }
            }
        }

        public enum Status
        {
            NotInStack,
            InStack
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
                if (node.Status == Status.NotInStack)
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
        public static void DFS(Node node, Stack<Node> stack)
        {
                foreach (Node child in node.Children)
                {
                    if (child.Status == Status.NotInStack)
                    {
                        DFS(child, stack);
                    }
                }

            node.Status = Status.InStack;
            stack.Push(node);
        }
    }
}
