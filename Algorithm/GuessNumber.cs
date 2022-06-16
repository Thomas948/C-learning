using System;

namespace Algorithm
{
    public class GuessNumber
    {
        public static void GuessNum(int left, int right)
        {
            var random = CommonUtil.GetRandomNum(left, right);
            Console.WriteLine($"数字在{left}到{right}之间，猜猜看!");
            var count = Guess(random);
            Console.WriteLine($"一共猜了{count}次");
        }


        private static int Guess(int r)
        {
            var count = 0;
            var input = 0;
            while (input != r)
            {
                var inputStr = Console.ReadLine();
                var isCorrectInput = int.TryParse(inputStr, out input);
                if (isCorrectInput)
                {
                    if (input<r)
                    {
                        Console.WriteLine("小了，请再猜");
                    }else if (input>r)
                    {
                        Console.WriteLine("大了，请再猜");
                    }
                    else
                    {
                        Console.WriteLine("猜对了");
                    }
                    count++;
                }
                else
                {
                    Console.WriteLine("不合理的猜想！请再猜！");
                }
            }

            return count;
        }
    }
}