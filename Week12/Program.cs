using System;
using System.Collections.Generic;

namespace Week12
{
    internal class Program
    {
        private static PokemonManager manager = new PokemonManager();

        public static void Main(string[] args)
        {
            Pokemon p1 = new Pokemon(4, "小火龙", 10, 22);
            p1.extras = new List<int> { 33, 22 };
            manager.AddPokemon(p1);
            Pokemon p2 = new Pokemon(7, "杰尼龟");
            manager.AddPokemon(p2);
            Pokemon p3 = new Pokemon(1, "妙娃种子");
            p3.extras = new List<int>();
            manager.AddPokemon(p3);
            Pokemon p4 = new Pokemon(9, "水箭龟", 36, 88);
            p4.extras = new List<int> { 233, 666, 88 };
            manager.AddPokemon(p4);
            manager.SerializeJson();
            manager.Clear();
            manager.DeserializeJson();
            manager.Print();
        }
    }
}