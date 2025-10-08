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
        public int BasePower { get; set; }

        public PokemonType type { get; set; }
        public MoveType moveType { get; set; }        

        public Move(string name, PokemonType type, MoveType moveType, int basePower = 100)
        {
            Name = name;            
            this.type = type;
            this.moveType = moveType;

            //BasePower = Math.Clamp(basePower, 1, 255);

            //BasePower = basePower < 1 ? 1 
            //    : basePower > 255 ? 255 
            //    : basePower;

            if (basePower < 1)
                BasePower = 1;
            else if (basePower > 255)
                BasePower = 255;
            else
                BasePower = basePower;
        }        
    }
}