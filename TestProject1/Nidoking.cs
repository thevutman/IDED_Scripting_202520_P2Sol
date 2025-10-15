using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class Nidoking : Pokemon
    {
        public Nidoking() : base("Nidoking", PokemonType.Poison, PokemonType.Ground)
        {
            Level = 1;
            Attack = 49;
            Defense = 49;
            SpAttack = 65;
            SpDefense = 65;
        }
    }
}
