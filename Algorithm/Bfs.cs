using System;
using System.Collections.Generic;

namespace Algorithm
{
    public class Bfs
    {
        private static Dictionary<string, List<string>> map = new Dictionary<string, List<string>>
        {
            { "you", new List<string> { "alice", "bob", "claire" } },
            { "bob", new List<string> { "anuj", "peggy" } },
            { "alice", new List<string> { "peggy" } },
            { "claire", new List<string> { "thom", "jonny" } },
            { "anuj", new List<string>() },
            { "peggy", new List<string>() },
            { "thom", new List<string>() },
            { "jonny", new List<string>() }
        };


        public static bool BreadthFirstSearch(string name)
        {
            var searchQueue = new Queue<string>(map[name]);
            var searched = new List<string>();
            while (searchQueue.Count != 0)
            {
                var person = searchQueue.Dequeue();
                if (searched.Contains(person))
                {
                    continue;
                }

                if (CheckPerson(person))
                {
                    Console.WriteLine($"{person} is a mango seller!");
                    return true;
                }

                foreach (var friend in map[person])
                {
                    searchQueue.Enqueue(friend);
                }

                searched.Add(person);
            }

            Console.WriteLine("no one is mango seller.");
            return false;
        }

        private static bool CheckPerson(string name)
        {
            return name.EndsWith('m');
        }
    }
}