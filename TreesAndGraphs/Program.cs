using System;
using System.Collections.Generic;
using static TreesAndGraphs.FasterSlowerRunner;

namespace TreesAndGraphs
{
    class Program
    {
        static void Main(string[] args)
        {
            //              50
            //            /    \
            //          17      72
            //          / \     / \
            //        12   23  54  76
            //       / \   /    \
            //      9  14 19     67
            //BinaryTreeNode root = new BinaryTreeNode(50);
            //var node2 = root.InsertLeft(17);
            //var node3 = root.InsertRight(72);

            //var node4 = node2.InsertLeft(12);
            //var node5 = node2.InsertRight(23);

            //var node6 = node3.InsertLeft(54);
            //var node7 = node3.InsertRight(76);

            //var node8 = node4.InsertLeft(9);
            //var node9 = node4.InsertRight(14);

            //var node10 = node5.InsertLeft(19);

            //var node11 = node6.InsertRight(67);

            //BFS bFS = new BFS();
            //Console.WriteLine("Breadth First Search: ");
            //bFS.LevelOrderQueue(root);

            //Console.WriteLine("Is tree Balanced?(Depth First Search using Stack)");
            //bool isBalanced = NodeDepthPair.IsBalanced(root);
            //Console.WriteLine(isBalanced);

            //SecondLargestElementInaBST secondLargestElementInaBST = new SecondLargestElementInaBST();
            //Console.WriteLine("Reverse In Order");
            //SecondLargestElementInaBST.SecondLargest(root);

            // Graph
            // 1 -------------> 2
            // ^\               |
            // | \              |
            // |   --> 3        |
            // | /              |
            // |/               |
            // 4 <------------- 5

            //ShortestPath.Node node1 = new ShortestPath.Node();
            //ShortestPath.Node node2 = new ShortestPath.Node();
            //ShortestPath.Node node3 = new ShortestPath.Node();
            //ShortestPath.Node node4 = new ShortestPath.Node();
            //ShortestPath.Node node5 = new ShortestPath.Node();

            //node1.value = 1;
            //node1.next = new List<ShortestPath.Node> { node2, node3 };

            //node2.value = 2;
            //node2.next = new List<ShortestPath.Node> { node5 };

            //node3.value = 3;

            //node5.value = 5;
            //node5.next = new List<ShortestPath.Node> { node4 };

            //node4.value = 4;
            //node4.next = new List<ShortestPath.Node> { node3, node1 };

            //ShortestPath sp = new ShortestPath();
            //List<ShortestPath.Node> nodes = sp.shortestPath(node5, node3);

            //Console.WriteLine("Result:");

            //for (int i = nodes.Count - 1; i >= 0; i--)
            //{
            //    Console.WriteLine(nodes[i].value.ToString());
            //}

            //Console.ReadLine();

            // Find a duplicate, Space Edition
            //int[] numbers1 = { 4, 1, 7, 3, 5, 2, 4, 6 };
            //int[] numbers2 = { 4, 1, 7, 3, 5, 2, 4, 7 };
            //int[] numbers3 = { 4, 1, 7, 3, 7, 2, 5, 7 };
            //int[] numbers4 = { 6, 1, 5, 3, 7, 2, 4, 7 };

            //int result1 = FindADuplicateSpaceEdition.FindRepeat(numbers1);
            //int result2 = FindADuplicateSpaceEdition.FindRepeat(numbers2);
            //int result3 = FindADuplicateSpaceEdition.FindRepeat(numbers3);
            //int result4 = FindADuplicateSpaceEdition.FindRepeat(numbers4);

            // "def", "a", "aba"
            // s = "def"
            // i = 2
            //          "", {'d'}, false
            //                  \d
            //              "d", {'e'}, false
            //                     \e
            //               "de", {'f'}, false
            //                       \f
            //                "def", {}, true
            //Console.WriteLine("Auto Complete:");

            ////string[] dic = new string[] { "def", "a", "aba" };
            //string[] dic = new string[] { "ab", "a" };

            //AutoComplete autoComplete = new AutoComplete(dic);

            ////List<string> results = autoComplete.GetWordsForPrefix("def");
            //List<string> results = autoComplete.GetWordsForPrefix("a");

            //foreach (string word in results)
            //    Console.WriteLine(word);

            //Console.ReadLine();

            Console.WriteLine("Build Order:");
            // projects: a, b, c, d, e, f
            // dependencies: (a, d), (f, b), (b, d), (f, a), (d, c)
            // Output: f, e, a, b, d, c
            //      ---> a ---> 
            //    /             \
            //   /          edge \
            // f                   ---> d ---> c   
            //   \               /       
            //    \             /      
            //      ---> b --->   
            //string[] projects = { "a", "b", "c", "d", "e", "f" };
            //string[,] dependencies = new string[5, 2]
            //    { { "a", "d" }, { "f", "b" }, { "b", "d" }, { "f", "a" }, { "d", "c" } };

            //BuildOrder buildOrder = new BuildOrder();

            //BuildOrder.Project[] projects1 = buildOrder.FindBuildOrder(projects, dependencies);
            ////Stack<BuildOrder.Project> projects1 = buildOrder.FindBuildOrder2(projects, dependencies);

            //foreach (BuildOrder.Project project in projects1)
            //{ 
            //    Console.WriteLine(project.Name);
            //}

            ////while (projects1.Count > 0)
            ////{
            ////    BuildOrder.Project project = projects1.Pop();
            ////    Console.WriteLine(project.Name);
            ////}

            // {{},{0},{0},{1,2},{3}}
            // 0, 1, 2, 3, 4
            //      - 1 <-
            //     /       \
            // 0 <-         <- 3 <- 4
            //     \       /
            //      - 2 <-

            int[,] processes = new int[5, 2] { { -1, -1 }, { 0, -1 }, { 0, -1 }, { 1, 2 }, { 3, -1 } };
            BuildOrderByteByByte buildOrderByteByByte = new BuildOrderByteByByte();
            List<int> result = buildOrderByteByByte.BuildOrder(processes);

            foreach(int i in result)
                Console.WriteLine(i);

            Console.ReadLine();

            // Runners = { A, B, C, D, F}
            ////      ---> A ---> 
            ////    /             \
            ////   /          edge \
            //// F                   ---> D ---> C   
            ////   \               /       
            ////    \             /      
            ////      ---> B --->   
            // Result = { F, B, A, D, C}
            //Console.WriteLine("Runners Order:");
            //List<Report> reports = new List<Report> {
            //                                           new Report { Faster = "A", Slower = "D" },
            //                                           new Report { Faster = "F", Slower = "B" },
            //                                           new Report { Faster = "B", Slower = "D" },
            //                                           new Report { Faster = "F", Slower = "A" },
            //                                           new Report { Faster = "D", Slower = "C" },
            //                                        };

            //// Output = {}
            //string[] result = RunnersInOrder(reports);

            //foreach (string runner in result)
            //    Console.WriteLine(runner);

            //Console.ReadLine();

            //Console.WriteLine("Mesh Message");

            //var network = new Dictionary<string, string[]>();
            //network.Add("Min", new[] { "William", "Jayden", "Omar" });
            //network.Add("William", new[] { "Min", "Noam" });
            //network.Add("Jayden", new[] { "Min", "Amelia", "Ren", "Noam" });
            //network.Add("Ren", new[] { "Jayden", "Omar" });
            //network.Add("Amelia", new[] { "Jayden", "Adam", "Miguel" });
            //network.Add("Adam", new[] { "Amelia", "Miguel", "Sofia", "Lucas" });
            //network.Add("Miguel", new[] { "Amelia", "Adam", "Liam", "Nathan" });
            //network.Add("Noam", new[] { "Nathan", "Jayden", "William" });
            //network.Add("Omar", new[] { "Ren", "Min", "Scott" });

            //string[] route = MeshMessage.BfsGetPath(network, "Jayden", "Adam");

            //Console.WriteLine(String.Join(", ", route));
            //Console.ReadLine();
        }
    }
}
