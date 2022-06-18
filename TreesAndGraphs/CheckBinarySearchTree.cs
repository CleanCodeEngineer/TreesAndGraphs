namespace TreesAndGraphs
{
    // 4.5 Validate BST: Implement a function to check if a binary tree is a binary search tree.
    // If every node on the left must be less than or equal to the current node, then this is really
    // the same thing as saying that the biggest node on the left must be less than or equal to
    // the current node.
    // Think about the checkBST function as a recursive function that ensures each node is
    // within an allowable (min, max) range. At first, this range is infinite. When we traverse
    // to the left, the min is negative infinity and the max is root.value. 
    //              50
    //            /    \
    //          17      72
    //          / \     / \
    //        12   23  54  76
    //       / \   /    \
    //      9  14 19     67
    // IsBinarySeachTree(50) : IsBinarySeachTree(50, int.MinValue, int.MaxValue)= true
    // 1- IsBinarySeachTree(50, int.MinValue, int.MaxValue): 50 >= int.MaxValue || 50 <= int.MinValue : false, IsBinarySeachTree(17, int.MinValue, 50)=true && IsBinarySeachTree(72, 50, int.MaxValue)=true
    // 2- IsBinarySeachTree(17, int.MinValue, 50): IsBinarySeachTree(12, int.MinValue, 17)=true && IsBinarySeachTree(23, 17, 50)=true
    // 15- IsBinarySeachTree(72, 50, int.MaxValue): IsBinarySeachTree(54, 50, 72)=true && IsBinarySeachTree(76, 72, int.MaxValue)=true
    // 3- IsBinarySeachTree(12, int.MinValue, 17): IsBinarySeachTree(9, int.MinValue, 12)=true && IsBinarySeachTree(14, 12, 17)=true
    // 10- IsBinarySeachTree(23, 17, 50): IsBinarySeachTree(19, 17, 23)=true && IsBinarySeachTree(null, 23, 50)=true
    // 16- IsBinarySeachTree(54, 50, 72): IsBinarySeachTree(null, 50, 54)=true && IsBinarySeachTree(67, 54, 72)=true
    // 21- IsBinarySeachTree(76, 72, int.MaxValue): IsBinarySeachTree(null, 72, 76)=true && IsBinarySeachTree(null, 76, int.MaxValue)=true
    // 4- IsBinarySeachTree(9, int.MinValue, 12): IsBinarySeachTree(null, int.MinValue, 9)=true && IsBinarySeachTree(null, 9, 12)=true
    // 7- IsBinarySeachTree(14, 12, 17): IsBinarySeachTree(null, 12, 14)=true && IsBinarySeachTree(null, 14, 17)=true
    // 5- IsBinarySeachTree(null, int.MinValue, 9): root == null return true
    // 6- IsBinarySeachTree(null, 9, 12): root == null return true
    // 8- IsBinarySeachTree(null, 12, 14): root == null return true
    // 9- IsBinarySeachTree(null, 14, 17): root == null return true
    // 11- IsBinarySeachTree(19, 17, 23): IsBinarySeachTree(null, 17, 19)=true && IsBinarySeachTree(null, 19, 23)=true
    // 12- IsBinarySeachTree(null, 17, 19): root == null return true
    // 13- IsBinarySeachTree(null, 19, 23): root == null return true 
    // 14- IsBinarySeachTree(null, 23, 50): root == null return true
    // 17- IsBinarySeachTree(null, 50, 54): root == null return true
    // 18- IsBinarySeachTree(67, 54, 72): IsBinarySeachTree(null, 54, 67)=true && IsBinarySeachTree(null, 67, 72)=true
    // 19- IsBinarySeachTree(null, 54, 67): root == null return true
    // 20- IsBinarySeachTree(null, 67, 72): root == null return true
    // 22- IsBinarySeachTree(null, 72, 76): root == null return true
    // 23- IsBinarySeachTree(null, 76, int.MaxValue): root == null return true
    public class CheckBinarySearchTree
    {
        public static bool IsBinarySearchTree(BinaryTreeNode root)
        {
            return IsBinarySearchTree(root, int.MinValue, int.MaxValue);
        }

        public static bool IsBinarySearchTree(BinaryTreeNode root, int lowerBound, int upperBound)
        {
            if (root == null)
                return true;

            if (root.Value >= upperBound || root.Value <= lowerBound)
                return false;

            return IsBinarySearchTree(root.Left, lowerBound, root.Value)
                    && IsBinarySearchTree(root.Right, root.Value, upperBound);
        }
    }
}
