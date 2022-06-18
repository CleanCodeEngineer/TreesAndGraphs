using System;
using System.Collections.Generic;

namespace TreesAndGraphs
{
    // Tree traversal with BFS using Queue
    // Breadth First Search
    //              50
    //            /    \
    //          17      72
    //          / \     / \
    //        12   23  54  76
    //       / \   /    \
    //      9  14 19     67
    //
    // {50, 17, 72, 12, 23, 54, 76, 9, 14, 19, 67}
    public class BFS
    {
        public void LevelOrderQueue(BinaryTreeNode root)
        {
            if (root == null)
                return;

            var nodes = new Queue<BinaryTreeNode>();
            nodes.Enqueue(root);

            while (nodes.Count > 0)
            {
                // Remove the node from the front of our Queue
                var node = nodes.Dequeue();

                // Print the node
                Console.WriteLine(" " + node.Value);

                if (node.Left != null)
                    nodes.Enqueue(node.Left);

                if (node.Right != null)
                    nodes.Enqueue(node.Right);
            }
        }
    }
}
