using System;
using System.Collections.Generic;

namespace TreesAndGraphs
{
    // Question 4.7 from Cracking the Coding Interview and similar to bytebybyte packages question 
    // You are given a list of projects and a list of dependencies (which is a list of pairs of projects, where the second project is dependent on the first project).
    // All of a project's dependencies must be built before the project is. Find a build order that will allow the projects to be built.
    // If there is no valid order, return an error.
    // EXAMPLE 
    // Input:
    // projects: a, b, c, d, e, f
    // dependencies: (a, d), (f, b), (b, d), (f, a), (d, c)
    // Output: f, e, a, b, d, c
    // Hints: #26, #47, #60, #85, #125, #133
    // Build a directed graph representing the dependencies. Each node is a project and an 
    // edge exists from A to B if B depends on A(A must be built before B). You can also build
    // it the other way if it's easier for you.
    //      ---> a ---> 
    //    /             \
    //   /          edge \
    // f                   ---> d ---> c     e
    //   \               /       
    //    \             /      
    //      ---> b --->               

    // Topological sort algorithm
    // O(P + D) time, where P is the number of projects and D is the number of dependency pairs.
    // A topological sort of a directed graph is a way of ordering the list of nodes such that if (a, b) is an edge 
    // in the graph then a will appear before b in the list. If a graph has cycles or is not directed, then there is no
    // topological sort.
    // There are a number of applications for this. For example, suppose the graph represents parts on an assembly
    // line.The edge (Handle, Door) indicates that you need to assemble the handle before the door. The topological sort would offer a valid ordering for the assembly line.
    public class BuildOrder
    {
        // SOLUTION #1
        /* Find a correct build order. */
        //  1.We first added the nodes with no incoming edges. If the set of projects can be built, there must be some 
        //  "first" project, and that project can't have any dependencies. If a project has no dependencies (incoming 
        //  edges), then we certainly can't break anything by building it first. 
        //  2. We removed all outgoing edges from these roots.This is reasonable.Once those root projects were built,
        //  it doesn't matter if another project depends on them. 
        //  3. After that, we found the nodes that now have no incoming edges. Using the same logic from steps 1 and
        //  2, it's okay if we build these. Now we just repeat the same steps: find the nodes with no dependencies, 
        //  add them to the build order, remove their outgoing edges, and repeat. 
        //  4. What if there are nodes remaining, but all have dependencies (incoming edges)? This means there's no 
        //  way to build the system.We should return an error. 
        //  The implementation follows this approach very closely.
        //  Initialization and setup: 
        //  1. Build a graph where each project is a node and its outgoing edges represent the projects that depend
        //  on it.That is, if A has an edge to B (A -> B), it means B has a dependency on A and therefore A must be
        //  built before B. Each node also tracks the number of incoming edges. 
        //  2. Initialize a buildOrder array. Once we determine a project's build order, we add it to the array. We also 
        //  continue to iterate through the array, using a toBeProcessed pointer to point to the next node to be
        //  fully processed.
        //  3. Find all the nodes with zero incoming edges and add those to a buildOrder array. Set a
        //  toBeProcessed pointer to the beginning of the array.
        //  Repeat until toBeProcessed is at the end of the buildOrder: 
        //  1. Read node at toBeProcessed. 
        //  » If node is null, then all remaining nodes have a dependency and we have detected a cycle.
        //  2. For each child of node: 
        //  » Decrement child.dependencies (the number of incoming edges). 
        //  » If child.dependencies is zero, add child to end of buildOrder. 
        //  3. Increment toBeProcessed.
        public Project[] FindBuildOrder(string[] projects, string[,] dependencies)
        {
            Graph graph = BuildGraph(projects, dependencies);

            return OrderProjects(graph.Nodes);
        }

        /* Build the graph, adding the edge (a, b) if b is dependent on a.
         * Assumes a pair is listed in "build order". The pair (a, b) in dependencies indicates that b 
         * depends on a and a must be built before b. */
        Graph BuildGraph(string[] projects, string[,] dependencies)
        {
            Graph graph = new Graph();

            foreach (string project in projects)
                graph.GetOrCreateNode(project);

            for (int i = 0; i < dependencies.GetLength(0); i++)
            {
                string first = dependencies[i, 0];
                string second = dependencies[i, 1];

                graph.AddEdge(first, second);
            }

            return graph;
        }

        /* Return a list of the projects with a correct build order. */
        Project[] OrderProjects(List<Project> projects)
        {
            Project[] order = new Project[projects.Count];

            /* Add "roots" to the build order first. */
            int endOfList = AddNonDependent(order, projects, 0);

            int toBeProcessed = 0;

            while (toBeProcessed < order.Length)
            {
                Project current = order[toBeProcessed];

                /* We have a circular dependency since there are no remaining projects with zero dependencies. */
                if (current == null)
                    return null;


                /* Remove myself as a dependency. */
                List<Project> children = current.Children;

                foreach (Project child in children)
                    child.Dependencies--;

                /* Add children that have no one depending on them. */
                if (children.Count != 0)
                    endOfList = AddNonDependent(order, children, endOfList);

                toBeProcessed++;
            }

            return order;
        }

        /* A helper function to insert projects with zero dependencies into the order
         * array, starting at index offset. */
        int AddNonDependent(Project[] order, List<Project> projects, int offset)
        {
            foreach (Project project in projects)
            {
                if (project.Dependencies == 0)
                {
                    order[offset] = project;
                    offset++;
                }
            }

            return offset;
        }

        public class Graph
        {
            public List<Project> Nodes { get; set; }

            // HashMap
            public Dictionary<string, Project> Map { get; set; }

            public Graph()
            {
                Nodes = new List<Project>();
                Map = new Dictionary<string, Project>();
            }

            public void AddEdge(string startName, string endName)
            {
                Project start = GetOrCreateNode(startName);
                Project end = GetOrCreateNode(endName);

                start.AddNeighbor(end);
            }

            public Project GetOrCreateNode(string name)
            {
                // Here we check if the node already exists
                if (!Map.ContainsKey(name))
                {
                    Project node = new Project(name);
                    Nodes.Add(node);
                    Map[name] = node;
                }

                return Map[name];
            }
        }

        public class Project
        {
            public string Name { get; set; }
            public int Dependencies { get; set; }

            // HashMap
            public Dictionary<string, Project> Map { get; set; }

            public List<Project> Children { get; set; }

            public Project(string name)
            {
                Name = name;

                // For Solution #1
                Dependencies = 0;

                Children = new List<Project>();
                Map = new Dictionary<string, Project>();

                // For Solution #2
                state = State.BLANK;
            }

            public void AddNeighbor(Project node)
            {
                // Here we check if there is a cycle in our Graph and we avoid duplicates
                if (!Map.ContainsKey(node.Name))
                {
                    Children.Add(node);
                    Map.Add(node.Name, node);
                    node.Dependencies++;
                }
            }

            // For Solution #2
            public enum State { BLANK, PARTIAL, COMPLETE }

            public State state { get; set; }
        }

        // SOLUTION #2
        // DFS (depth-first search)
        // O(P+D) time, where P is the number of projects and D is the number of dependency pairs.
        //      ---> a ---> 
        //    /             \
        //   /          edge \
        // f                   ---> d ---> c     e
        //   \               /       
        //    \             /      
        //      ---> b --->
        //
        // projects: a, b, c, d, e, f
        // dependencies: (a, d), (f, b), (b, d), (f, a), (d, c)
        // Output: f, e, a, b, d, c
        public Stack<Project> FindBuildOrder2(string[] projects, string[,] dependencies)
        {
            Graph graph = BuildGraph(projects, dependencies);

            return OrderProjects2(graph.Nodes);
        }

        Stack<Project> OrderProjects2(List<Project> projects)
        {
            Stack<Project> stack = new Stack<Project>();

            foreach (Project project in projects)
            {
                if (project.state == Project.State.BLANK)
                {
                    if (!doDFS(project, stack))
                    {
                        return null;
                    }
                }
            }

            return stack;
        }

        public Boolean doDFS(Project project, Stack<Project> stack)
        {
            if (project.state == Project.State.PARTIAL)
            {
                return false; // Cycle
            }

            if (project.state == Project.State.BLANK)
            {
                project.state = Project.State.PARTIAL;

                List<Project> children = project.Children;

                foreach (Project child in children)
                {
                    if (!doDFS(child, stack))
                    {
                        return false;
                    }
                }

                project.state = Project.State.COMPLETE;
                stack.Push(project);
            }

            return true;
        }
    }
}
