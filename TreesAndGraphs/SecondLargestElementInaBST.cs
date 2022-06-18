using System;

namespace TreesAndGraphs
{
    // BST (Balanced- Space: O(n), Insert: O(lg(n)), Lookup: O(lg(n)), Delete: O(lg(n)))
    // Write a method to find the 2nd largest element in a binary search tree.
    public class SecondLargestElementInaBST
    {
        public int Count { get; set; }

        public SecondLargestElementInaBST()
        {
            Count = 0;
        }
        //              50
        //            /    \
        //          17      72
        //          / \     / \
        //        12   23  54  76
        //       / \   /    \   \
        //      9  14 19     67  80

        // This method prints out the elements of a BST in Descending order 
        // We're using the ReverseInOrder concept to write this method
        // Without 80:
        // SecondLargest(50): SecondLargest(72), Count = 1, SecondLargest(17)
        // SecondLargest(72): SecondLargest(76), Conut = 2, 72

        // With 80:
        // SecondLargest(50): SecondLargest(72),
        // SecondLargest(72): SecondLargest(76)
        // SecondLargest(76): SecondLargest(80), Count = 2, 76
        // SecondLargest(80): SecondLargest(null), Count = 1, SecondLargest(null)
        // SecondLargest(null): root == null return
        // SecondLargest(null): root == null return
        public void ReverseInOrder(BinaryTreeNode root)
        {
            if (root == null)
                return;

            ReverseInOrder(root.Right);

            Console.WriteLine(root.Value);

            ReverseInOrder(root.Left);
        }

        // This prints out the second element of a BST in Descending order : 2nd largest element
        public void SecondLargest(BinaryTreeNode root)
        {
            if (root == null)
                return;

            SecondLargest(root.Right);

            Count++;

            if (Count == 2)
            {
                Console.WriteLine(root.Value);
                return;
            }

            SecondLargest(root.Left);
        }
    }
}
