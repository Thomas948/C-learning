using System;
using System.Collections.Generic;

namespace 回合制对战
{
    internal class Program
    {
        static Skill ChooseSkill(Character c)
        {
            List<Skill> skills = c.Skills;
            for (int i = 0; i < skills.Count; i++)
            {
                Skill skill = skills[i];
                Console.WriteLine($"{i + 1}-->{skill.Name}");
            }

            int index = -1;
            while (index < 0 || index >= skills.Count)
            {
                index = Utils.GetInputNum() - 1;
            }

            return skills[index];
        }

        static void PrintStates(Character c)
        {
            for (int i = 0; i < c.States.Count; i++)
            {
                State state = c.States[i];
                Console.Write(state + "  ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Character sophie = new Character("索菲", 1526, 432, 226, 0);
            sophie.AddSkill(new Skill("撕裂冲击", SkillType.NormalAttack, 100));
            sophie.AddSkill(new Skill(name: "水海豚", SkillType.SuckBlood, 80, 30));
            sophie.AddSkill(new Skill("白袍庇佑", SkillType.HealOverTime, 20, 5, true, 3));

            Character plitvice = new Character("普利特维采", 1825, 400, 195, 3);
            plitvice.AddSkill(new Skill("永恒火焰", SkillType.NormalAttack, 100));
            plitvice.AddSkill(new Skill("复苏", SkillType.Heal, 35, 0, true));

            int round = 1;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"--------第{round}回合--------");

                 //显示状态
                 PrintStates(sophie);
                
                //攻击之前，状态生效
                sophie.StatesEffect();
                
                //1攻击2
                Console.ForegroundColor = ConsoleColor.Green;
                //选择技能
                Skill sophieSkill = ChooseSkill(sophie);
                sophie.Attack(sophieSkill, sophieSkill.Self ? sophie : plitvice);

                if (plitvice.isDead())
                {
                    Console.WriteLine($"{plitvice.Name}倒下了");
                    break;
                }
                
                Console.ForegroundColor = ConsoleColor.Gray;
                PrintStates(plitvice);
                
                //攻击之前，状态生效
                plitvice.StatesEffect();

                //2攻击1
                Skill plitviceSkill = Utils.RandomSkill(plitvice);
                Console.ForegroundColor = ConsoleColor.Red;
                plitvice.Attack(plitviceSkill, plitviceSkill.Self ? plitvice : sophie);
                if (sophie.isDead())
                {
                    Console.WriteLine($"{sophie.Name}倒下了");
                    break;
                }

                round++;
            }

            Console.WriteLine("游戏结束");
        }
    }
}