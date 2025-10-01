namespace TestProject1
{
    public enum PokemonType
    {
        Rock,
        Ground,
        Water,
        Electric,
        Fire,
        Grass,
        Ghost,
        Poison,
        Psychic,
        Bug,
    }

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

        public Pokemon(string name, PokemonType type)
        {
            Name = name;
            Types.Add(type);
        }
    }
}