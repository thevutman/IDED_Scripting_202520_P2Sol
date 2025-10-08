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

        public Pokemon(string name, PokemonType type, int level, int attack, int defense, int SpAtk, int SpDef)
        {
            Name = name;
            Types.Add(type);
            Level = level;
            Attack = attack;
            Defense = defense;
            SpAttack = SpAtk;
            SpDefense = SpDef;
        }

        public Pokemon(string name, PokemonType type) : this(name, type, 1, 10, 10, 10, 10)
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