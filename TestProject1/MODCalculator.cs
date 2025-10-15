using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1;

namespace Parcial2SCriptingPokemones.Source
{
    internal static class MODCalculator
    {
        private static readonly Dictionary<PokemonType, Dictionary<PokemonType, double>> TypeChart =
            new Dictionary<PokemonType, Dictionary<PokemonType, double>>
        {
        // Atacante: Roca
        { PokemonType.Rock, new Dictionary<PokemonType, double>
            {
                { PokemonType.Ground, 0.5 }, { PokemonType.Water, 1.0 }, { PokemonType.Electric, 1.0 },
                { PokemonType.Fire, 2.0 }, { PokemonType.Grass, 0.5 }, { PokemonType.Ghost, 1.0 },
                { PokemonType.Poison, 1.0 }, { PokemonType.Psychic, 1.0 }, { PokemonType.Bug, 2.0 }
            }
        },
        // Atacante: Tierra
        { PokemonType.Ground, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 2.0 }, { PokemonType.Ground, 1.0 }, { PokemonType.Water, 1.0 },
                { PokemonType.Electric, 2.0 }, { PokemonType.Fire, 1.0 }, { PokemonType.Grass, 0.5 },
                { PokemonType.Ghost, 1.0 }, { PokemonType.Poison, 2.0 }, { PokemonType.Psychic, 1.0 },
                { PokemonType.Bug, 0.5 }
            }
        },
        // Atacante: Agua
        { PokemonType.Water, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 2.0 }, { PokemonType.Ground, 2.0 }, { PokemonType.Water, 0.5 },
                { PokemonType.Electric, 1.0 }, { PokemonType.Fire, 2.0 }, { PokemonType.Grass, 0.5 },
                { PokemonType.Ghost, 1.0 }, { PokemonType.Poison, 1.0 }, { PokemonType.Psychic, 1.0 },
                { PokemonType.Bug, 1.0 }
            }
        },
        // Atacante: Eléctrico
        { PokemonType.Electric, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 1.0 }, { PokemonType.Ground, 0.0 }, { PokemonType.Water, 2.0 },
                { PokemonType.Electric, 0.5 }, { PokemonType.Fire, 1.0 }, { PokemonType.Grass, 0.5 },
                { PokemonType.Ghost, 1.0 }, { PokemonType.Poison, 1.0 }, { PokemonType.Psychic, 1.0 },
                { PokemonType.Bug, 1.0 }
            }
        },
        // Atacante: Fuego
        { PokemonType.Fire, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 0.5 }, { PokemonType.Ground, 1.0 }, { PokemonType.Water, 0.5 },
                { PokemonType.Electric, 1.0 }, { PokemonType.Fire, 0.5 }, { PokemonType.Grass, 2.0 },
                { PokemonType.Ghost, 1.0 }, { PokemonType.Poison, 1.0 }, { PokemonType.Psychic, 1.0 },
                { PokemonType.Bug, 2.0 }
            }
        },
        // Atacante: Planta
        { PokemonType.Grass, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 2.0 }, { PokemonType.Ground, 2.0 }, { PokemonType.Water, 2.0 },
                { PokemonType.Electric, 0.5 }, { PokemonType.Fire, 0.5 }, { PokemonType.Grass, 0.5 },
                { PokemonType.Ghost, 1.0 }, { PokemonType.Poison, 0.5 }, { PokemonType.Psychic, 1.0 },
                { PokemonType.Bug, 0.5 }
            }
        },
        // Atacante: Fantasma
        { PokemonType.Ghost, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 1.0 }, { PokemonType.Ground, 1.0 }, { PokemonType.Water, 1.0 },
                { PokemonType.Electric, 1.0 }, { PokemonType.Fire, 1.0 }, { PokemonType.Grass, 1.0 },
                { PokemonType.Ghost, 2.0 }, { PokemonType.Poison, 0.5 }, { PokemonType.Psychic, 2.0 },
                { PokemonType.Bug, 1.0 }
            }
        },
        // Atacante: Veneno
        { PokemonType.Poison, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 0.5 }, { PokemonType.Ground, 0.5 }, { PokemonType.Water, 1.0 },
                { PokemonType.Electric, 1.0 }, { PokemonType.Fire, 1.0 }, { PokemonType.Grass, 2.0 },
                { PokemonType.Ghost, 0.5 }, { PokemonType.Poison, 0.5 }, { PokemonType.Psychic, 1.0 },
                { PokemonType.Bug, 1.0 }
            }
        },
        // Atacante: Psíquico
        { PokemonType.Psychic, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 1.0 }, { PokemonType.Ground, 1.0 }, { PokemonType.Water, 1.0 },
                { PokemonType.Electric, 1.0 }, { PokemonType.Fire, 1.0 }, { PokemonType.Grass, 1.0 },
                { PokemonType.Ghost, 1.0 }, { PokemonType.Poison, 2.0 }, { PokemonType.Psychic, 0.5 },
                { PokemonType.Bug, 1.0 }
            }
        },
        // Atacante: Bicho
        { PokemonType.Bug, new Dictionary<PokemonType, double>
            {
                { PokemonType.Rock, 1.0 }, { PokemonType.Ground, 1.0 }, { PokemonType.Water, 1.0 },
                { PokemonType.Electric, 1.0 }, { PokemonType.Fire, 0.5 }, { PokemonType.Grass, 2.0 },
                { PokemonType.Ghost, 0.5 }, { PokemonType.Poison, 0.5 }, { PokemonType.Psychic, 2.0 },
                { PokemonType.Bug, 0.5 }
            }
        },
        };

        public static double CalculateMod(List<PokemonType> attackingType, List<PokemonType> defendingTypes)
        {
            double mod = 1.0;
            foreach (var  attackType in attackingType)
            {
                foreach (var defendingType in defendingTypes)
                {
                    if (TypeChart.ContainsKey(attackType) && TypeChart[attackType].ContainsKey(defendingType))
                    {
                        mod *= TypeChart[attackType][defendingType];
                    }
                }
            }
            return mod;
        }
    }
}