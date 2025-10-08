using NUnit.Framework.Legacy;

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
    }
}