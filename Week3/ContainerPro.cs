using System;
using System.Collections.Generic;
using System.Linq;

namespace Week3
{
    internal class ContainerPro
    {
        static Dictionary<string, int> dictA = new Dictionary<string, int>();
        static Dictionary<string, int> dictB = new Dictionary<string, int>();
        static Random random = new Random();

        public static void ContainerTest()
        {
            for (int i = 'a'; i <= 'z'; i++)
            {
                dictA.Add(Convert.ToChar(i).ToString(), random.Next(0, 11));
                dictB.Add(Convert.ToChar(i).ToString(), random.Next(0, 11));
            }

            List<string> evenKeysA = dictA.Where(pair => pair.Value % 2 == 0).Select(pair => pair.Key)
                .ToList();
            List<string> evenKeysB = dictB.Where(pair => pair.Value % 2 == 0).Select(pair => pair.Key)
                .ToList();
            foreach (string key in evenKeysA)
            {
                dictA.Remove(key);
            }

            foreach (string key in evenKeysB)
            {
                dictB.Remove(key);
            }

            Console.WriteLine("--------dictA--------");
            foreach (var pair in dictA)
            {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }

            Console.WriteLine("--------dictB--------");
            foreach (var pair in dictB)
            {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }

            Dictionary<string, int> dictC = dictA.Union(dictB.Where(pair => !dictA.ContainsKey(pair.Key)))
                .OrderBy(pair => pair.Key)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            Console.WriteLine("--------dictC--------");
            foreach (var pair in dictC)
            {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }
        }
    }
}