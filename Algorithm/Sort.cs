using System.Collections.Generic;
using System.Linq;

namespace Algorithm
{
    public class Sort
    {
        
        public static List<int> QuickSort(List<int> list)
        {
            if (list.Count < 2)
            {
                return list;
            }

            var pivot = list[0];
            var less = list.Where(item => item < pivot).ToList();
            var equals = list.Where(item => item == pivot).ToList();
            var greater = list.Where(item => item > pivot).ToList();
            return QuickSort(less).Concat(equals).Concat(QuickSort(greater)).ToList();
        }

        public static void BubbleSort(List<int> list)
        {
            for (var i = 0; i < list.Count - 1; i++)
            {
                for (var j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        (list[j], list[j + 1]) = (list[j + 1], list[j]);
                    }
                }
            }
        }
        
        public static List<int> SelectionSort(List<int> list)
        {
            var newList = new List<int>();
            for (var i = 0; i < list.Count;)
            {
                var smallest = FindSmallest(list.ToArray());
                newList.Add(list[smallest]);
                list.RemoveAt(smallest);
            }

            return newList;
        }

        private static int FindSmallest(int[] arr)
        {
            var smallest = arr[0];
            var smallestIndex = 0;
            for (var i = 1; i < arr.Length; i++)
            {
                if (arr[i] >= smallest) continue;
                smallest = arr[i];
                smallestIndex = i;
            }

            return smallestIndex;
        }
    }
}