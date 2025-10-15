using NUnit.Framework.Legacy;
using Parcial2SCriptingPokemones.Source;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(40)]
        [TestCase(0)]
        [TestCase(-90)]
        [TestCase(256)]
        public void TestMoveCreation(int poder)
        {
            var move = new Move("Water Gun", PokemonType.Water, Move.MoveType.Special, poder);

            //ClassicAssert.True(move.BasePower > 0 && move.BasePower < 256);
            //Assert.That(move.BasePower, Is.InRange(1, 255));
            //Assert.That(move.BasePower, Is.GreaterThan(0).And.LessThan(256));
            //Assert.That(move.BasePower, Is.AtLeast(1).And.AtMost(255));

            ClassicAssert.LessOrEqual(move.BasePower, 255);
            ClassicAssert.GreaterOrEqual(move.BasePower, 1);
        }

        [Test]
        public void TestDefaultMovePower()
        {
            Move move1 = new Move("Water Gun", PokemonType.Water, Move.MoveType.Special);
            Assert.That(move1.BasePower, Is.EqualTo(100));
        }

        [Test]
        public void TestPokemonCreation()
        {
            Pokemon squirtle = new Pokemon("Squirtle", PokemonType.Water);

            Assert.That(squirtle.Level, Is.EqualTo(1));
            Assert.That(squirtle.Attack, Is.EqualTo(10));
            Assert.That(squirtle.Defense, Is.EqualTo(10));
            Assert.That(squirtle.SpAttack, Is.EqualTo(10));
            Assert.That(squirtle.SpDefense, Is.EqualTo(10));
        }

        [Test]
        public void AttackMODCalculator()
        {
            Chorrotuga squirtle = new Chorrotuga();
            Raichu raichu = new Raichu();
            Ponyta ponyta = new Ponyta();
            List<PokemonType> moves = new List<PokemonType>();
            moves.Add(PokemonType.Fire);
            moves.Add(PokemonType.Ground);
            double mod;

            mod = MODCalculator.CalculateMod(raichu.Types, moves);
            Assert.That(mod, Is.EqualTo(0));
        }

        [TestCase(PokemonType.Electric, PokemonType.Ground, 0.0,
        "Ataque Electrico contra Tierra, (0x)")]
        [TestCase(PokemonType.Grass, PokemonType.Fire, 0.5,
        "Ataque Hierva contra Fuego, (0.5x) ")]
        [TestCase(PokemonType.Psychic, PokemonType.Ghost, 1.0,
        "Ataque Fisico contra Fantasma, (1x) ")]
        [TestCase(PokemonType.Rock, PokemonType.Bug, 2.0,
        "Ataque Roca contra Bicho, (2x) ")]
        public void CalculateMod_SingleTypeCombinations_ShouldReturnCorrectMod(PokemonType attackType, PokemonType defenderType, double expectedMod, string description)
        {
            // Act
            double actualMod = MODCalculator.CalculateMod(new List<PokemonType> { attackType }, new List<PokemonType> { defenderType });

            // Assert
            Assert.That(actualMod, Is.EqualTo(expectedMod), description);
        }

        [TestCase(PokemonType.Ground, PokemonType.Electric, PokemonType.Poison, 4.0,
        "Ataque Tierra (2x) contra Eléctrico, (2x) contra Veneno. 2x2=4x")]
        [TestCase(PokemonType.Electric, PokemonType.Water, PokemonType.Ground, 0.0,
        "Ataque Eléctrico (2x) contra Agua, (0x) contra Tierra. 2x0=0x")]
        [TestCase(PokemonType.Fire, PokemonType.Water, PokemonType.Bug, 1.0,
        "Ataque Fuego (0.5x) contra Agua, (2x) contra Bicho. 0.5x2=1x")]
        [TestCase(PokemonType.Bug, PokemonType.Grass, PokemonType.Electric, 2.0,
        "Ataque Bicho (2x) contra Planta, (0.5x) contra Electrico. 2x1=2x")]
        [TestCase(PokemonType.Poison, PokemonType.Water, PokemonType.Poison, 0.5,
        "Ataque Veneno (1x) contra Agua, (0.5x) contra Veneno. 2x0.5=1x")]
        public void CalculateMod_IntTypeCombinations_ShouldMultiplyMods(PokemonType attackType, PokemonType defType1, PokemonType defType2, double expectedMod, string description)
        {
            // Arrange
            List<PokemonType> defenderTypes = new List<PokemonType> { defType1, defType2 };

            // Act
            double actualMod = MODCalculator.CalculateMod(new List<PokemonType> { attackType }, defenderTypes);

            // Assert
            Assert.That(actualMod, Is.EqualTo(expectedMod), description);
        }

        [Test]
        public void CalculateMod_UsingPokemonInstances_ShouldReturnCorrectMod()
        {
            // Arrange - Instancias de Pokémon
            var Metapod = new Metapod();      // Tipo: Bug
            var Chorrotuga = new Chorrotuga();    // Tipo: Water
            var Ponyta = new Ponyta();        // Tipo: Fire
            var Raichu = new Raichu();        // Tipo: Electric
            var Nidoking = new Nidoking();      // Tipos: Poison, Ground

            double mod1 = MODCalculator.CalculateMod(Metapod.Types, Chorrotuga.Types);
            Assert.That(mod1, Is.EqualTo(1.0));

            double mod2 = MODCalculator.CalculateMod(Ponyta.Types, Raichu.Types);
            Assert.That(mod2, Is.EqualTo(1.0));

            double mod3 = MODCalculator.CalculateMod(Raichu.Types, Nidoking.Types);
            Assert.That(mod3, Is.EqualTo(0.0));

            double mod4 = MODCalculator.CalculateMod(Nidoking.Types, Metapod.Types);
            Assert.That(mod4, Is.EqualTo(0.5));

            double mod5 = MODCalculator.CalculateMod(Nidoking.Types, Ponyta.Types);
            Assert.That(mod5, Is.EqualTo(1.0));
        }
    }
}