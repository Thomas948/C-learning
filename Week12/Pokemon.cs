using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;

namespace Week12
{
    public class Pokemon
    {
        public int Id { set; get; }
        public int GameId { set; get; }
        public string Name { set; get; }
        public int Level { get; set; }
        public int Attack { get; set; }

        public List<int> extras;

        public Pokemon(int gameId, string name, int level = 1, int attack = 6)
        {
            GameId = gameId;
            Name = name;
            Level = level;
            Attack = attack;
        }

        public Pokemon()
        {
        }

        public override string ToString()
        {
            return $"{GameId} {Name} {Level} {Attack}";
        }
    }

    class PokemonManager
    {
        private Dictionary<int, Pokemon> idToPokemon = new Dictionary<int, Pokemon>();
        private int counter = 1;

        private Dictionary<string, List<Pokemon>> nameToPokemons = new Dictionary<string, List<Pokemon>>();

        public bool AddPokemon(Pokemon pokemon)
        {
            if (pokemon.Id != 0)
            {
                Console.WriteLine("添加失败");
                return false;
            }

            pokemon.Id = counter++;
            idToPokemon.Add(pokemon.Id, pokemon);
            if (nameToPokemons.ContainsKey(pokemon.Name))
            {
                nameToPokemons[pokemon.Name].Add(pokemon);
            }
            else
            {
                nameToPokemons.Add(pokemon.Name, new List<Pokemon> { pokemon });
            }

            return true;
        }

        public void Clear()
        {
            idToPokemon.Clear();
            nameToPokemons.Clear();
        }

        public void Print()
        {
            Console.WriteLine("---------idToPokemon---------");
            foreach (var pair in idToPokemon)
            {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }

            Console.WriteLine("---------nameToPokemons---------");
            foreach (var pair in nameToPokemons)
            {
                string output = $"{pair.Key}:";
                foreach (var pokemon in pair.Value)
                {
                    output += "{" + pokemon + "}";
                }

                Console.WriteLine(output);
            }
        }

        public void SerializeJson()
        {
            using (FileStream file = new FileStream("save", FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(file);
                bw.Write(idToPokemon.Count);
                foreach (var pokemon in idToPokemon.Select(pair => pair.Value))
                {
                    string json =
                        JsonMapper.ToJson(new Pokemon(pokemon.GameId, pokemon.Name, pokemon.Level, pokemon.Attack));
                    bw.Write(json);
                }
            }
        }

        public void DeserializeJson()
        {
            using (FileStream file = new FileStream("save", FileMode.Open))
            {
                BinaryReader br = new BinaryReader(file);
                int num = br.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    string json = br.ReadString();
                    Pokemon pokemon = JsonMapper.ToObject<Pokemon>(json);
                    AddPokemon(pokemon);
                }
            }
        }
    }
}