using System.Collections.Generic;

namespace TreesAndGraphs
{
    // Given a directed graph, find the shortest path between two nodes if one exists.
    // eg.
    // 1 -------------> 2
    // ^\               |
    // | \              |
    // |   --> 3        |
    // | /              |
    // |/               |
    // 4 <------------- 5
    //
    // shortestPath(2, 3) = 2 -> 5 -> 4 -> 3
    // Hash Map
    // "acyclic graph": is a graph without cycles
    // shortestPath(5, 3) = {5, 4 , 3}
    public class ShortestPath
    {
        // a Node in a Graph
        public class Node
        {
            public int value;
            public List<Node> next;
        }

        public List<Node> shortestPath(Node a, Node b)
        {
            if (a == null || b == null) return null;

            // FIFO // BFS // Traversing the Graph using a queue: Breadth First Search
            Queue<Node> toVisit = new Queue<Node>();

            // Hash Map<node, parent>
            Dictionary<Node, Node> parents = new Dictionary<Node, Node>();

            toVisit.Enqueue(a);
            parents.Add(a, null);

            while (toVisit.Count > 0)
            {
                Node curr = toVisit.Dequeue();

                if (curr == b) break;

                foreach (Node n in curr.next)
                {
                    // Here we check if there is a cycle in our Graph and we avoid duplicates
                    if (!parents.ContainsKey(n))
                    {
                        toVisit.Enqueue(n);
                        parents.Add(n, curr);
                    }
                }
            }

            // We created our Hash Map
            // parents = {5:null, 4:5, 1:4, 3:4, 2:1}
            if (!parents.ContainsKey(b)) return null;

            List<Node> result = new List<Node>();

            Node current = b;

            while (current != null)
            {
                result.Add(current);

                Node value;

                if (parents.TryGetValue(current, out value))
                {
                    current = value;
                }
            }

            // result = {5, 4 , 3}
            return result;
        }
    }
}
