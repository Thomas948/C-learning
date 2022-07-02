using System;
using System.Collections.Generic;

namespace 回合制对战
{
    public class Utils
    {
        private static Random r = new Random();

        private Utils()
        {
        }

        public static int GetInputNum()
        {
            Console.WriteLine("选择使用哪个技能：");
            var isCorrect = int.TryParse(Console.ReadLine(), out var input);
            while (!isCorrect)
            {
                Console.WriteLine("输入有误，请再次输入");
                isCorrect = int.TryParse(Console.ReadLine(), out input);
            }

            return input;
        }

        public static Skill RandomSkill(Character c)
        {
            return c.Skills[r.Next(0, c.Skills.Count)];
        }
    }

    public class Character
    {
        public string Name { get; }
        public int Hp { get; private set; }
        public int MaxHp { get; }
        public int Atk { get; }
        public int Def { get; } //伤害公式 对方攻击*（1-def/100+def）

        public int critRate;

        public int critBonus = 200; //暴击伤害倍数默认2

        public List<Skill> Skills { get; private set; }

        public List<State> States { get; }

        public Character(string name, int hp, int atk, int def, int critRate)
        {
            Name = name;
            Hp = hp;
            MaxHp = hp;
            Atk = atk;
            Def = def;
            this.critRate = critRate;
            Skills = new List<Skill>();
            States = new List<State>();
        }

        public void AddSkill(Skill skill)
        {
            Skills.Add(skill);
        }

        public void ReduceHp(int reduce)
        {
            Hp = Hp - reduce < 0 ? 0 : Hp - reduce;
        }

        public void ResumeHp(int resume)
        {
            Hp = Hp + resume > MaxHp ? MaxHp : Hp + resume;
        }

        public int Calcdamage(int atk, int percent, int def = 0)
        {
            float a = atk * (percent / 100f);
            int damage = (int)(a * (1 - (float)def / (100 + def)));
            return damage;
        }


        public bool isDead()
        {
            return Hp == 0;
        }

        public void StatesEffect()
        {
            foreach (var state in States)
            {
                switch (state.Type)
                {
                    case StateType.Damage:
                    {
                        int damage = Calcdamage(state.Source.Atk, state.Data1, Def);

                        ReduceHp(damage);
                        Console.WriteLine($"{state.Name}生效，受到{damage}点伤害。{Hp}/{MaxHp}");
                    }
                        break;
                    case StateType.Heal:
                    {
                        int reply = Calcdamage(state.Source.Atk, state.Data1);
                        ResumeHp(reply);
                        Console.WriteLine($"{state.Name}生效，回复{reply}点生命。{Hp}/{MaxHp}");
                    }
                        break;
                }
            }

            //状态时间减少
            for (int i = States.Count - 1; i >= 0; i--)
            {
                State state = States[i];
                state.Time--;
                if (state.Time <= 0)
                {
                    States.RemoveAt(i);
                }
            }
        }

        public void Attack(Skill skill, Character target)
        {
            switch (skill.Type)
            {
                case SkillType.NormalAttack:
                {
                    int damage = Calcdamage(Atk, skill.Data1, target.Def);

                    target.ReduceHp(damage);
                    Console.WriteLine($"{Name}使用{skill.Name}攻击{target.Name},造成{damage}点伤害。");
                    Console.WriteLine($"{target.Name}:{target.Hp}/{target.MaxHp}");
                }
                    break;
                case SkillType.SuckBlood:
                {
                    float realAtk = Atk * (float)skill.Data1 / 100f;
                    int damage = (int)(realAtk * (1 - (float)target.Def / (100 + target.Def)));

                    int suck = (int)(damage * skill.Data2 / 100f);
                    target.ReduceHp(damage);
                    ResumeHp(suck);
                    Console.WriteLine($"{Name}使用{skill.Name}攻击{target.Name},造成{damage}点伤害。");
                    Console.WriteLine($"{target.Name}:{target.Hp}/{target.MaxHp}");
                    Console.WriteLine($"{Name}回复{suck}点生命。{Name}:{Hp}/{MaxHp}");
                }
                    break;
                case SkillType.Heal:
                {
                    int reply = Calcdamage(Atk, skill.Data1);
                    target.ResumeHp(reply);

                    Console.WriteLine($"{Name}使用治疗技能，回复{reply}点生命");
                    Console.WriteLine($"{target.Name}：{target.Hp}/{target.MaxHp}");
                }
                    break;
                case SkillType.HealOverTime:
                {
                    int reply = Calcdamage(Atk, skill.Data1);
                    target.ResumeHp(reply);
                    State state = new State(StateType.Heal, this, skill.Time, skill.Data2);
                    state.Name = skill.Name;
                    States.Add(state);
                    Console.WriteLine($"{Name}使用治疗技能，回复{reply}点生命，进入{state.Name}状态");
                    Console.WriteLine($"{target.Name}：{target.Hp}/{target.MaxHp}");
                }
                    break;
            }
        }
    }

    public enum SkillType
    {
        NormalAttack, //普通攻击
        SuckBlood, //攻击回血
        Heal, //回复
        HealOverTime,
        DamageOverTime,
    }

    public enum StateType
    {
        Heal,
        Damage,
    }

    public class Skill
    {
        public string Name { get; }
        public SkillType Type { get; }

        public bool Self { get; }
        public int Data1 { get; } //普攻比例  治疗比例  HOT直接治疗百分比
        public int Data2 { get; } //伤害转化回复生命比例 持续治疗百分比
        public int Time { get; } // 技能持续时间

        public Skill(string name, SkillType type, int data1, int data2 = 0, bool self = false, int time = 0)
        {
            Name = name;
            Type = type;
            Data1 = data1;
            Data2 = data2;
            Self = self;
            Time = time;
        }
    }

    public class State
    {
        public string Name { get; set; }
        public StateType Type { get; }
        public int Time { get; set; }
        public int Data1 { get; }

        public Character Source { get; }

        public State(StateType type, Character src, int time, int data1)
        {
            Type = type;
            Time = time;
            Data1 = data1;
            Source = src;
        }

        public override string ToString()
        {
            return $"{Name}({Time})";
        }
    }
}