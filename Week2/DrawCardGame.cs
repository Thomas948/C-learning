using System;
using System.Collections.Generic;
using System.Linq;

namespace Week2
{
    class DrawCardGame
    {
        private static Dictionary<string, int> _levelToWeight;

        private static List<string> _cardPool = new List<string>();

        private static Random _random = new Random();

        static void Init()
        {
            _levelToWeight = new Dictionary<string, int> { { "S", 1 }, { "A", 2 }, { "B", 2 }, { "C", 4 } };
            foreach (var pair in _levelToWeight)
            {
                for (int i = 1; i <= pair.Value; i++)
                {
                    _cardPool.Add(pair.Key);
                }
            }
        }

        static void AddCard(string cardLevel, int cardWeight)
        {
            _levelToWeight.Add(cardLevel, cardWeight);
            for (int i = 1; i <= cardWeight; i++)
            {
                _cardPool.Add(cardLevel);
            }
        }

        static string DrawCard()
        {
            return _cardPool[_random.Next(0, _cardPool.Count)];
        }

        static void Main(string[] args)
        {
            Init();
            Dictionary<string, int> counts = _levelToWeight.Keys.ToDictionary(key => key, key => 0);
            for (int i = 0; i < 20; i++)
            {
                string card = DrawCard();
                Console.WriteLine($"第{i + 1}次抽卡，抽到{card}");
                counts[card]++;
            }

            Console.WriteLine($"一共抽到{counts["S"]}次S，{counts["A"]}次A，{counts["B"]}次B，{counts["C"]}次C");

            AddCard("D", 5);
            counts = _levelToWeight.Keys.ToDictionary(key => key, key => 0);
            for (int i = 0; i < 20; i++)
            {
                string card = DrawCard();
                Console.WriteLine($"第{i + 1}次抽卡，抽到{card}");
                counts[card]++;
            }

            Console.WriteLine($"一共抽到{counts["S"]}次S，{counts["A"]}次A，{counts["B"]}次B，{counts["C"]}次C，{counts["D"]}次D");
        }
    }
}