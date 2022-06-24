using System;
using System.Threading;

namespace Week1
{
    public enum 信号灯
    {
        无信号,
        红,
        绿,
        黄
    }

    public class SignalLamp
    {
        static bool isPowerOn = true;

        static 信号灯 state = 信号灯.无信号;

        static int time;
        
        public static void EnterSignalLamp()
        {
            while (isPowerOn)
            {
                switch (state)
                {
                    case 信号灯.绿:
                    {
                        Console.Clear();
                        Console.WriteLine($"-->绿灯 {time}\n   黄灯\n   红灯");
                        time--;
                        if (time == 0)
                        {
                            state = 信号灯.黄;
                            time = 4;
                        }
                    }
                        break;
                    case 信号灯.黄:
                    {
                        Console.Clear();
                        Console.WriteLine($"   绿灯\n-->黄灯 {time}\n   红灯");
                        time--;
                        if (time == 0)
                        {
                            state = 信号灯.红;
                            time = 30;
                        }
                    }
                        break;
                    case 信号灯.红:
                    {
                        Console.Clear();
                        Console.WriteLine($"   绿灯\n   黄灯\n-->红灯 {time}");
                        time--;
                        if (time == 0)
                        {
                            state = 信号灯.绿;
                            time = 30;
                        }
                    }
                        break;
                    default:
                        state = 信号灯.绿;
                        time = 30;
                        break;
                }

                Thread.Sleep(100);
            }
        }
    }
}