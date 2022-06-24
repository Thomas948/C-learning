using System;
using System.Threading;

namespace Week1
{
    class Boxer
    {
        public static readonly Random Random = new Random();

        private bool _isTrigger;

        private string Name { get; }

        private int MaxHp { get; }

        private int CurrentHp { get; set; }

        private int Attack { get; set; }

        public Boxer(string name, int maxHp, int attack)
        {
            Name = name;
            MaxHp = maxHp;
            CurrentHp = maxHp;
            Attack = attack;
        }

        public void BlastFist(Boxer opponent)
        {
            Console.WriteLine($"{Name}使用技能『爆裂拳』攻击{opponent.Name}");
            opponent.Attacked(Attack * 3);
        }

        public void NormalAtk(Boxer defender)
        {
            Console.WriteLine($"-->{Name}攻击{defender.Name}");
            defender.Attacked(Attack);
        }

        private void Attacked(int damage)
        {
            switch (Name)
            {
                case "warrior":
                    Injured(damage);
                    WarriorPassive();
                    break;
                case "ranger" when Random.Next(1, 5) < 2:
                    Console.WriteLine($"{Name}闪开了");
                    return;
                case "ranger":
                    Injured(damage);
                    RangerPassive();
                    break;
            }
        }

        private void RangerPassive()
        {
            if (_isTrigger || CurrentHp > 0) return;
            Console.WriteLine($"{Name}发动了技能复活，生命值回满！");
            _isTrigger = true;
            CurrentHp = MaxHp;
        }

        private void WarriorPassive()
        {
            if (_isTrigger || !(CurrentHp < MaxHp * 0.4)) return;
            Console.WriteLine($"{Name}发动了技能愤怒，攻击力翻倍！");
            _isTrigger = true;
            Attack *= 2;
        }

        private void Injured(int damage)
        {
            CurrentHp -= damage;
            if (CurrentHp < 0)
            {
                CurrentHp = 0;
            }

            Console.WriteLine($"{Name}受到{damage}点伤害，生命值：{CurrentHp}");
        }

        public bool IsAlive()
        {
            return CurrentHp > 0;
        }
    }

    public class Duel
    {
        public static void StartDuel()
        {
            var warrior = new Boxer("warrior", 250, 15);
            var ranger = new Boxer("ranger", 200, 10);
            while (warrior.IsAlive() && ranger.IsAlive())
            {
                if (Boxer.Random.Next(0, 3) == 1)
                {
                    warrior.BlastFist(ranger);
                }
                else
                {
                    warrior.NormalAtk(ranger);
                }

                if (!ranger.IsAlive())
                {
                    break;
                }

                ranger.NormalAtk(warrior);
                Console.WriteLine();
            }

            Console.WriteLine(warrior.IsAlive() ? "warrior获胜" : "ranger获胜");
        }
    }
}