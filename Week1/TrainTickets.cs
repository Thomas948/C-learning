using System;

namespace Week1
{
    class TrainTickets
    {
        static bool[,] tickets = new bool[3, 4];

        public static void EnterTicketSystem()
        {
            InitTickets();
            while (HasTicket())
            {
                Console.WriteLine("-----火车票管理系统-----");
                ShowTickets();
                Console.WriteLine("您想要哪一行？");
                var row = GetInputLocation(3);

                Console.WriteLine("您想要哪一列？");
                var col = GetInputLocation(4);
                Console.WriteLine(BuyTickets(row, col) ? "购票成功\n" : "购票失败\n");
            }

            Console.WriteLine("没有票了");
        }

        private static bool HasTicket()
        {
            for (int i = 0; i < tickets.GetLength(0); i++)
            {
                for (int j = 0; j < tickets.GetLength(1); j++)
                {
                    if (tickets[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool BuyTickets(int row, int col)
        {
            if (!tickets[row - 1, col - 1]) return false;
            tickets[row - 1, col - 1] = false;
            return true;
        }

        private static int GetInputLocation(int limit)
        {
            var isCorrectRow = int.TryParse(Console.ReadLine(), out var locationNum);
            while (!isCorrectRow || locationNum < 1 || locationNum > limit)
            {
                Console.WriteLine("没有这样的位置,请重新选择");
                isCorrectRow = int.TryParse(Console.ReadLine(), out locationNum);
            }

            return locationNum;
        }

        private static void InitTickets()
        {
            for (int i = 0; i < tickets.GetLength(0); i++)
            {
                for (int j = 0; j < tickets.GetLength(1); j++)
                {
                    tickets[i, j] = true;
                }
            }
        }

        private static void ShowTickets()
        {
            for (int i = 0; i < tickets.GetLength(0); i++)
            {
                for (int j = 0; j < tickets.GetLength(1); j++)
                {
                    Console.Write(tickets[i, j] ? $"{i + 1}-{j + 1}有票" : $"{i + 1}-{j + 1}无票");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}