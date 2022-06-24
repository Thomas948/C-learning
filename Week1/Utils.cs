using System;

namespace Week1
{
    class Utils
    {
        private static Utils instance;
        
        public static Utils Instance => instance ?? (instance = new Utils());

        private Utils()
        {
            
        }

        private static Random _random = new Random();

        public int GetRandomNum(int a, int b)
        {
            return _random.Next(a, b);
        }

        public void PrintArray(int[] array)
        {
            foreach (var e in array)
            {
                Console.Write($"{e} ");
            }
        }
    }

    public class UtilsTest
    {
       public static void TestUtil()
        {
            Console.WriteLine($"生成一个1~10的随机数：{Utils.Instance.GetRandomNum(1, 10)}");
            var array = new int[] { 1, 7, 2, 4, 8 };
            Utils.Instance.PrintArray(array);
        }
    }
}