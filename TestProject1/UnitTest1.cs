using NUnit.Framework.Legacy;
using Parcial2SCriptingPokemones.Source;
using static TestProject1.Move;

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
            Pokemon ekans = new Pokemon("ekans", PokemonType.Poison);
            Pokemon vulpix = new Pokemon("vulpix", PokemonType.Fire);

            Move move1 = new Move("mordisco", ekans.Types[0], Move.MoveType.Physical);
            double mod;

            mod = MODCalculator.CalculateMod(move1, vulpix.Types);
            Assert.That(mod, Is.EqualTo(1.0));
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
            // Move
            Move move1 = new Move("move 1", attackType, Move.MoveType.Physical);

            // Act
            double actualMod = MODCalculator.CalculateMod(move1, new List<PokemonType> { defenderType });

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
            // Move
            Move move = new Move("move", attackType, Move.MoveType.Physical);

            // Arrange
            List<PokemonType> defenderTypes = new List<PokemonType> { defType1, defType2 };

            // Act
            double actualMod = MODCalculator.CalculateMod(move, defenderTypes);

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

        /*        [TestCase(1, 1, 1, 1, 1, 1, ExpectedResult = 0)]
                [TestCase(2, 1, 1, 1, 1, 1, ExpectedResult = 1)]
                [TestCase(3, 5, 50, 100, 50, 2, ExpectedResult = 16)]
                [TestCase(4, 5, 50, 100, 50, 1, ExpectedResult = 5)]
                [TestCase(5, 10, 20, 30, 15, 1, ExpectedResult = 5)]
                [TestCase(6, 12, 40, 60, 80, 2, ExpectedResult = 9)]
                [TestCase(7, 25, 80, 120, 60, 1, ExpectedResult = 40)]
                [TestCase(8, 30, 100, 50, 100, 4, ExpectedResult = 58)]
                [TestCase(9, 40, 150, 200, 150, 1, ExpectedResult = 37)]
                [TestCase(10, 50, 128, 200, 100, 1, ExpectedResult = 58)]
                [TestCase(11, 50, 128, 200, 100, 4, ExpectedResult = 455)]
                [TestCase(12, 60, 200, 250, 200, 1, ExpectedResult = 132)]
                [TestCase(13, 70, 180, 200, 100, 2, ExpectedResult = 435)]
                [TestCase(14, 80, 90, 45, 90, 1, ExpectedResult = 33)]
                [TestCase(15, 90, 255, 200, 50, 2, ExpectedResult = 1554)]
                [TestCase(16, 99, 255, 255, 1, 2, ExpectedResult = 108206)]
                [TestCase(17, 99, 255, 255, 255, 4, ExpectedResult = 856)]
                [TestCase(18, 99, 255, 255, 255, 0, ExpectedResult = 0)]
                [TestCase(19, 99, 255, 1, 255, 1, ExpectedResult = 2)]
                [TestCase(20, 45, 60, 10, 200, 1, ExpectedResult = 2)]
                [TestCase(21, 20, 30, 5, 250, 1, ExpectedResult = 1)]
                public int TestDamageFormula(int testCaseId, int attackingLv, int movePwr, int attackingStat, int defendingStat, float mod)
                {
                    // ARRANGE
                    MoveType moveType;
                    if (testCaseId % 2 != 0)
                    {
                        moveType = MoveType.Special;
                    }
                    else
                    {
                        moveType = MoveType.Physical;
                    }

                    Pokemon attacker = new Pokemon("Attacker", PokemonType.Bug) { Level = attackingLv };
                    Pokemon defender = new Pokemon("Defender", PokemonType.Rock) { Defense = defendingStat, SpDefense = defendingStat };

                    if (moveType == MoveType.Physical)
                    {
                        attacker.Attack = attackingStat;
                        defender.Defense = defendingStat;
                    }
                    else
                    {
                        attacker.SpAttack = attackingStat;
                        defender.SpDefense = defendingStat;
                    }

                    Move move = new Move("Test Move", PokemonType.Bug, moveType, movePwr);

                    // ACT
                    int actualDmg = DamageCalculator.Calculate(attacker, defender, move, mod);

                    // ASSERT
                    return actualDmg;
                }*/

        [TestCase(1, 1, 1, 1, 1, 1, 0)]
        [TestCase(2, 1, 1, 1, 1, 1, 1)]
        [TestCase(3, 5, 50, 100, 50, 2, 16)]
        [TestCase(4, 5, 50, 100, 50, 1, 5)]
        [TestCase(5, 10, 20, 30, 15, 1, 5)]
        [TestCase(6, 12, 40, 60, 80, 2, 9)]
        [TestCase(7, 25, 80, 120, 60, 1, 40)]
        [TestCase(8, 30, 100, 50, 100, 4, 58)]
        [TestCase(9, 40, 150, 200, 150, 1, 37)]
        [TestCase(10, 50, 128, 200, 100, 1, 58)]
        [TestCase(11, 50, 128, 200, 100, 4, 455)]
        [TestCase(12, 60, 200, 250, 200, 1, 132)]
        [TestCase(13, 70, 180, 200, 100, 2, 435)]
        [TestCase(14, 80, 90, 45, 90, 1, 33)]
        [TestCase(15, 90, 255, 200, 50, 2, 1554)]
        [TestCase(16, 99, 255, 255, 1, 2, 108206)]
        [TestCase(17, 99, 255, 255, 255, 4, 856)]
        [TestCase(18, 99, 255, 255, 255, 0, 0)]
        [TestCase(19, 99, 255, 1, 255, 1, 2)]
        [TestCase(20, 45, 60, 10, 200, 1, 2)]
        [TestCase(21, 20, 30, 5, 250, 1, 1)]
        public void TestDamageFormula(int testCaseId, int attackingLv, int movePwr, int attackingStat, int defendingStat, float mod, int expected)
        {
            // ARRANGE
            MoveType moveType = testCaseId % 2 != 0 ? MoveType.Special : MoveType.Physical;

            Pokemon attacker = new Pokemon("Attacker", PokemonType.Bug) { Level = attackingLv };
            Pokemon defender = new Pokemon("Defender", PokemonType.Rock) { Defense = defendingStat, SpDefense = defendingStat };

            if (moveType == MoveType.Physical)
            {
                attacker.Attack = attackingStat;
                defender.Defense = defendingStat;
            }
            else
            {
                attacker.SpAttack = attackingStat;
                defender.SpDefense = defendingStat;
            }

            Move move = new Move("Test Move", PokemonType.Bug, moveType, movePwr);

            // ACT
            int actual = DamageCalculator.Calculate(attacker, defender, move, mod);

            // ASSERT (margen de error de ±1)
            Assert.That(actual, Is.InRange(expected - 1, expected + 1),
                $"TestCase {testCaseId}: Esperado {expected}, obtenido {actual}");
        }

    }
}