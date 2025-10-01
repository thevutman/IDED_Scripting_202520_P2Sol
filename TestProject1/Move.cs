namespace TestProject1
{
    public class Move
    {
        public enum MoveType
        {
            Physical,
            Special,
        }

        public string Name { get; set; }
        public string BasePower { get; set; }

        public PokemonType type { get; set; }
        public MoveType moveType { get; set; }
    }
}