using System;
using System.Collections.Generic;
using System.Linq;

namespace Week3
{
    public class ContainerConvert
    {
        public static void Main(string[] args)
        {
            List<string> list1 = new List<string> { "abandon", "black", "come", "druid", "evil", "flash", "gun" };
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < list1.Count; i += 2)
            {
                dict.Add(list1[i], i == list1.Count - 1 ? null : list1[i + 1]);
            }

            foreach (var pair in dict)
            {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }

            List<string> list2 = dict.Keys.Concat(dict.Values)
                .Where(e => e != null)
                .OrderBy(e => e)
                .ToList();
            foreach (string s in list2)
            {
                Console.Write($"{s}\t");
            }
        }
    }
}