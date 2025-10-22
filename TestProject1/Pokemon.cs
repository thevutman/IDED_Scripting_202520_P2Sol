using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class Pokemon
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }

        public int SpAttack { get; set; }
        public int SpDefense { get; set; }

        public List<PokemonType> Types { get; } = new List<PokemonType>();

        public Move[] Moves { get; set; }

        public Pokemon(string name, int level, int attack, int defense, int SpAtk, int SpDef, params PokemonType[] types)
        {
            Name = name;
            Types = types.ToList();
            Level = level;
            Attack = attack;
            Defense = defense;
            SpAttack = SpAtk;
            SpDefense = SpDef;
        }

        public Pokemon(string name, params PokemonType[] types) : this(name, 1, 10, 10, 10, 10, types)
        {
            //Name = name;
            //Types.Add(type);
            //Level = 1;
            //Attack = 10;
            //Defense = 10;
            //SpAttack = 10;
            //SpDefense = 10;
        }
    }
}
