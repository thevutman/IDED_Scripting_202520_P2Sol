using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class Ponyta : Pokemon
    {
        public Ponyta() : base("Ponyta", PokemonType.Fire)
        {
            Level = 1;
            Attack = 49;
            Defense = 49;
            SpAttack = 65;
            SpDefense = 65;
        }
    }
}
