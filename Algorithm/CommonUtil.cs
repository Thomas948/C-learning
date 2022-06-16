using System;

namespace Algorithm
{
    public class CommonUtil
    {
        public static int GetInputNum()
        {
            Console.WriteLine("请输入一个数字");
            var isCorrect = int.TryParse(Console.ReadLine(), out var input);
            while (!isCorrect)
            {
                Console.WriteLine("输入有误，请再次输入");
                isCorrect = int.TryParse(Console.ReadLine(), out input);
            }
            return input;
        }

        public static int GetRandomNum(int minRange, int maxRange)
        {
            var random = new Random();
            var r = random.Next(minRange, maxRange + 1);
            return r;
        }
    }
}