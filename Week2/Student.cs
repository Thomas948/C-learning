using System;

namespace Week2
{
    class Student
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public override string ToString()
        {
            return $"名字：{Name}，分数：{Score}";
        }

        public Student Copy()
        {
            var copy = new Student();
            copy.Name = Name;
            copy.Score = Score;
            return copy;
        }
    }
    
}