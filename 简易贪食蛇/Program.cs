using System;
using System.Collections.Generic;
using System.Threading;

namespace 简易贪食蛇
{
    enum Dir
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    struct Pos
    {
        public int x;
        public int y;

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Snake
    {
        public List<Pos> bodies;

        public Dir dir;

        public Snake()
        {
            bodies = new List<Pos> { new Pos(0, 0) };
        }

        public void ChangeDir(Dir inputDir)
        {
            if (inputDir == Dir.None || inputDir == dir)
            {
                return;
            }

            switch (dir)
            {
                case Dir.Right when inputDir == Dir.Left:
                case Dir.Left when inputDir == Dir.Right:
                case Dir.Up when inputDir == Dir.Down:
                case Dir.Down when inputDir == Dir.Up:
                    return;
            }

            dir = inputDir;
        }

        public void Move()
        {
            Pos head = bodies[0];

            //前进
            switch (dir)
            {
                case Dir.Up:
                {
                    head.y--;
                }
                    break;
                case Dir.Down:
                {
                    head.y++;
                }
                    break;
                case Dir.Left:
                {
                    head.x--;
                }
                    break;
                case Dir.Right:
                {
                    head.x++;
                }
                    break;
                case Dir.None:
                {
                }
                    break;
            }

            // 后一节跟上前一节
            for (int i = bodies.Count - 1; i > 0; i--)
            {
                bodies[i] = bodies[i - 1];
            }

            bodies[0] = head;
        }

        public void Grow()
        {
            bodies.Add(bodies[bodies.Count - 1]);
        }
    }

    internal class Program
    {
        private static Random random;
        private static Snake snake;
        private static char[,] buffer;
        private static ConsoleColor[,] colorBuffer;

        private static int width = 10;
        private static int height = 10;
        private static int ox = 4;
        private static int oy = 3;

        private static Pos food;
        private static int score;

        static void RandFood()
        {
            food = new Pos(random.Next(0, width), random.Next(0, height));

            foreach (Pos body in snake.bodies)
            {
                if (food.Equals(body))
                {
                    RandFood();
                }
            }
        }

        public static void Main(string[] args)
        {
            Init();
            Dir inputDir;
            while (true)
            {
                //输入处理
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
                while (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey();
                }

                switch (keyInfo.Key)
                {
                    case ConsoleKey.W:
                    {
                        inputDir = Dir.Up;
                    }
                        break;
                    case ConsoleKey.S:
                    {
                        inputDir = Dir.Down;
                    }
                        break;
                    case ConsoleKey.A:
                    {
                        inputDir = Dir.Left;
                    }
                        break;
                    case ConsoleKey.D:
                    {
                        inputDir = Dir.Right;
                    }
                        break;
                    default:
                    {
                        inputDir = Dir.None;
                    }
                        break;
                }

                //转向
                snake.ChangeDir(inputDir);


                snake.Move();
                Pos head = snake.bodies[0];

                bool isOver = false;
                for (int i = 0; i < snake.bodies.Count; i++)
                {
                    if (head.Equals(snake.bodies[i]) && i != 0)
                    {
                        isOver = true;
                    }
                }

                //蛇撞墙游戏结束
                if (head.y < 0 || head.y >= height || head.x < 0 || head.x >= width)
                {
                    isOver = true;
                }

                if (isOver)
                {
                    break;
                }

                // 吃掉食物
                if (snake.bodies[0].Equals(food))
                {
                    snake.Grow();
                    score++;
                    RandFood();
                }

                // 渲染
                Refresh();
                Thread.Sleep(250);
            }

            Console.SetCursorPosition((ox - 1) * 2, height + oy + 1);
            Console.Write("游戏结束");
        }

        private static void Init()
        {
            random = new Random();
            snake = new Snake();
            buffer = new char[height + oy + 1, width + ox + 1];
            colorBuffer = new ConsoleColor[buffer.GetLength(0), buffer.GetLength(1)];
            RandFood();
            Refresh();
        }

        private static void Refresh()
        {
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[i, j] = ' ';
                    colorBuffer[i, j] = ConsoleColor.Gray;
                }
            }

            // 画边界
            for (int i = oy - 1; i < height + oy + 1; i++)
            {
                for (int j = ox - 1; j < width + ox + 1; j++)
                {
                    if (i == oy - 1)
                    {
                        buffer[i, j] = '#';
                    }
                    else if (i == height + oy)
                    {
                        buffer[i, j] = '#';
                    }
                    else if (j == ox - 1)
                    {
                        buffer[i, j] = '#';
                    }
                    else if (j == width + ox)
                    {
                        buffer[i, j] = '#';
                    }
                }
            }

            // 画蛇
            for (int i = 0; i < snake.bodies.Count; i++)
            {
                int ty = snake.bodies[i].y + oy;
                int tx = snake.bodies[i].x + ox;
                buffer[ty, tx] = 'o';
                colorBuffer[ty, tx] = ConsoleColor.Green;
            }

            buffer[food.y + oy, food.x + ox] = 'x';
            colorBuffer[food.y + oy, food.x + ox] = ConsoleColor.Red;

            // Console打印Buffer
            Console.Clear();
            for (int i = 0; i < buffer.GetLength(0); i++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    Console.ForegroundColor = colorBuffer[i, j];
                    Console.Write(buffer[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition((ox - 1) * 2, oy - 2);
            Console.Write("得分：" + score);
        }
    }
}