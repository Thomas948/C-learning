using System;

namespace Algorithm
{
    public class BinarySearch
    {
        
        public static int Find(int[] nums, int target)
        {
            Array.Sort(nums);
            var low = 0;
            var high = nums.Length - 1;
            while (low <= high)
            {
                var mid = (low + high) / 2;
                var guess = nums[mid];
                if (guess == target) return mid;

                if (guess < target)
                    low = mid + 1;
                else
                    high = mid - 1;
            }

            return -1;
        }

        public static int FindFactorial(int[] nums, int target, int low, int high)
        {
            if (nums.Length == 0 || low > high)
            {
                return -1;
            }

            var mid = (low + high) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[mid] < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }

            return FindFactorial(nums, target, low, high);
        }
    }
}