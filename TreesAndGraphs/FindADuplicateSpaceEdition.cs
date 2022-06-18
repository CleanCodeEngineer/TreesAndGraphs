using System.Linq;

namespace TreesAndGraphs
{
    // Question #400: Write a method which finds an integer that appears more than once in our array. Don't modify the input! (We need to optimize for space!)
    // (If there are multiple duplicates, you only need to find one of them.) 
    // We have an array of integers, where:
    // 1. The integers are in the range 1..n
    // 2. The array has a length of n + 1
    // It follows that our array has at least one integer which appears at least twice. But it may have several duplicates, and each duplicate may appear more than twice.
    // If we're going to do O(nlgn) time, we'll probably be iteratively doubling something or iteratively cutting something in half. So what if we could cut the problem in half somehow?
    // Well, binary search works by cutting the problem in half after figuring out which half of our input array holds the answer. But in a binary search, the reason we can confidently say
    // which half has the answer is because the array is sorted.
    // What if we could cut the problem in half a different way, other than cutting the array in half? 
    // We cut the set of possibilities for the duplicate number in half by looking at the ranges 1..n/2 and n/2 + 1..n
    // Pigeonhole principle: We have more items (n + 1) than we have possibilities (n), so we must have at least one repeat.
    // Which half of our range contains a repeat?
    // Careful-if we do this recursively, we'll incur a space cost in the call stack! Do it iteratively instead.
    public class FindADuplicateSpaceEdition
    {
        public static int FindRepeat(int[] numbers)
        {
            int floor = 1;
            int ceiling = numbers.Length - 1;

            while (floor < ceiling)
            {
                // Divide our range 1..n into upper range and lower range
                // (such that they don't overlap)
                // Lower range is floor..midpoint
                // Upper range is midpoint+1..ceiling
                int midpoint = floor + (ceiling - floor) / 2;
                int lowerRangeFloor = floor;
                int lowerRangeCeiling = midpoint;
                int upperRangeFloor = midpoint + 1;
                int upperRangeCeiling = ceiling;

                // Count number of items in lower range
                int itemsInLowerRange = numbers.Count(item => item >= lowerRangeFloor && item <= lowerRangeCeiling);

                int distinctPossibleIntegersInLowerRange = lowerRangeCeiling - lowerRangeFloor + 1;

                if (itemsInLowerRange > distinctPossibleIntegersInLowerRange)
                {
                    // There must be a duplicate in the lower range
                    // so use the same approach iteratively on that range
                    floor = lowerRangeFloor;
                    ceiling = lowerRangeCeiling;
                }
                else
                {
                    // There must be a duplicate in the upper range
                    // so use the same approach iteratively on that range
                    floor = upperRangeFloor;
                    ceiling = upperRangeCeiling;
                }
            }

            // Floor and ceiling have coverged
            // We found a number that repeats!
            return floor;
        }
    }
}
