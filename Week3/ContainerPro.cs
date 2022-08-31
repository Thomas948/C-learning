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

            Dictionary<string, List<int>> dictC =
                dictA.ToDictionary(pair => pair.Key, pair => new List<int> { pair.Value, dictB[pair.Key] });
            Console.WriteLine("--------dictC--------");
            foreach (var pair in dictC)
            {
                Console.WriteLine($"{pair.Key}:{PrintList(pair.Value)}");
            }
        }

        static string PrintList(List<int> list)
        {
            string output = "";
            foreach (var e in list)
            {
                output = output + e + " ";
            }

            return output;
        }
    }
}