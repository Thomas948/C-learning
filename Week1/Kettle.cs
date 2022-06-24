using System;
using System.Threading;

namespace Week1
{
    public class Kettle
    {
        private string name;
        
        private int capacity;
        
        private double temperature;

        private double volume;

        public Kettle(string name,int capacity)
        {
            this.name = name;
            this.capacity = capacity;
        }

        public void AddWater(int increase)
        {
            if (volume + increase > capacity)
            {
                Console.WriteLine($"只能加{capacity - volume}ml水");
                temperature = volume / capacity * temperature;
                volume = capacity;
            }
            else
            {
                temperature = volume / (volume + increase) * temperature;
                volume += increase;
            }
            ShowInfo();
        }

        public void PourOut(int decrease)
        {
            if (volume==0)
            {
                Console.WriteLine("没有水了");
                temperature = 0;
            }else if (volume < decrease)
            {
                Console.WriteLine($"只能倒出{volume}ml水");
                volume = 0;
                temperature = 0;
            }
            else
            {
                volume -= decrease;
            }
            ShowInfo();
        }

        public void Heating()
        {
            if (volume == 0)
            {
                Console.WriteLine("没有水会把壶烧坏");
            }
            else
            {
                Console.WriteLine("开始加热");
                while (temperature < 100)
                {
                    temperature += 10;
                    if (temperature > 100)
                    {
                        temperature = 100;
                    }
                    Thread.Sleep(100);
                    Console.WriteLine($"-->水温：{temperature} 度");
                }
                Console.WriteLine("水烧开了");
            }
            ShowInfo();
        }

        private void ShowInfo()
        {
            Console.WriteLine($"{name}还有{volume}ml水，水温：{temperature}度");
        }
    }
}