using System;

namespace Week1
{
    public enum Hand
    {
        剪刀 = 1,
        石头 = 2,
        布 = 3
    }

    public enum GuessResult
    {
        Draw = 0,
        Win = 1,
        Lose = 2
    }

    public class FingerGuess
    {
        private static int gameCount = 10;
        static int win;
        static int lose;
        static int draw;
        
        public static void EnterFingerGuess()
        {
            var random = new Random();
            Console.WriteLine("-----游戏开始-----");
            for (var i = 0; i < gameCount; i++)
            {
                Console.WriteLine($"----第{i + 1}局----");
                Console.WriteLine("你想出什么？ 1：剪刀 2：石头 3：布");
                var input = GetPlayerHand();

                Enum.TryParse(random.Next(1, 4).ToString(), out Hand system);
                Console.WriteLine($"电脑出了{system}");

                var guessResult = GetGuessResult(input, system);
                switch (guessResult)
                {
                    case GuessResult.Draw:
                        draw += 1;
                        Console.WriteLine("这一局平局");
                        break;
                    case GuessResult.Lose:
                        lose += 1;
                        Console.WriteLine("这一局输了");
                        break;
                    case GuessResult.Win:
                        win += 1;
                        Console.WriteLine("这一局赢了");
                        break;
                }
            }

            Console.WriteLine($"您共获胜{win}局，失败{lose}局，打平{draw}局");
        }

        private static Hand GetPlayerHand()
        {
            var inputStr = Console.ReadLine();
            Enum.TryParse(inputStr, out Hand input);
            while (input != Hand.剪刀 && input != Hand.石头 && input != Hand.布)
            {
                Console.WriteLine("这不是正确的手势，请重新输入");
                Enum.TryParse(Console.ReadLine(), out input);
            }

            return input;
        }

        private static GuessResult GetGuessResult(Hand input, Hand system)
        {
            if (input.Equals(system))
            {
                return GuessResult.Draw;
            }

            switch (input)
            {
                case Hand.剪刀:
                    return system.Equals(Hand.石头) ? GuessResult.Lose : GuessResult.Win;
                case Hand.石头:
                    return system.Equals(Hand.剪刀) ? GuessResult.Win : GuessResult.Lose;
                case Hand.布:
                    return system.Equals(Hand.剪刀) ? GuessResult.Lose : GuessResult.Win;
                default:
                    return GuessResult.Draw;
            }
        }
    }
}