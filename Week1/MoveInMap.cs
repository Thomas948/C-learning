using System;

namespace Week1
{
    public class MoveInMap
    {
        static string mapStr =
            "#################\n" +
            "#               #\n" +
            "#    ~~~     H  #\n" +
            "#      ~~~      #\n" +
            "#        ~~~    #\n" +
            "#    C    ~~~   #\n" +
            "#     D  C  ~~~ #\n" +
            "#               #\n" +
            "#               #\n" +
            "#################";

        static char[,] map = new char[10, 17];

        private static bool needQuit;

        private static int vertical = 1;

        private static int horizontal = 5;

        public static void EnterMap()
        {
            FillMap(vertical, horizontal);
            while (!needQuit)
            {
                Console.Clear();
                PrintMap2d();
                ProcessInput();
                FillMap(vertical, horizontal);
            }
        }

        private static void ProcessInput()
        {
            var keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    vertical -= 1;
                    break;
                case ConsoleKey.W:
                    vertical -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    vertical += 1;
                    break;
                case ConsoleKey.S:
                    vertical += 1;
                    break;
                case ConsoleKey.LeftArrow:
                    horizontal -= 1;
                    break;
                case ConsoleKey.A:
                    horizontal -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    horizontal += 1;
                    break;
                case ConsoleKey.D:
                    horizontal += 1;
                    break;
                case ConsoleKey.Q:
                    needQuit = true;
                    break;
            }

            if (vertical < 1)
            {
                vertical = 1;
            }

            if (vertical > 8)
            {
                vertical = 8;
            }

            if (horizontal < 1)
            {
                horizontal = 1;
            }

            if (horizontal > 15)
            {
                horizontal = 15;
            }
        }

        private static void PrintMap2d()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        private static void FillMap(int y, int x)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    map[i, j] = mapStr[i * 18 + j];
                }
            }

            map[y, x] = 'o';
        }
    }
}