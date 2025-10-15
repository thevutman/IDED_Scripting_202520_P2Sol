using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class Raichu : Pokemon
    {
        public Raichu() : base("Raichu", PokemonType.Electric)
        {
            Level = 1;
            Attack = 49;
            Defense = 49;
            SpAttack = 65;
            SpDefense = 65;
        }
    }
}
