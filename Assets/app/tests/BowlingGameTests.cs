using app.bowling.logic;
using NUnit.Framework;

namespace app.tests
{
    public class BowlingGameTests
    {
        private Game _game;
        
        [SetUp]
        public void SetUp()
        {
            _game = new Game(10);
        }

        [Test]
        public void RollNoPinInGame()
        {
            RollMany(20, 0);
            Assert.AreEqual(0, _game.ComputeScore());
        }

        [Test]
        public void Roll20PinsInGame()
        {
            RollMany(20, 1);
            Assert.AreEqual(20, _game.ComputeScore());
        }

        [Test]
        public void RollASpareGame()
        {
            RollMany(21, 5);
            Assert.AreEqual(10 * (10 + 5), _game.ComputeScore());
        }

        [Test]
        public void RollASpare()
        {
            Roll(6);
            Roll(4);
            Roll(1);
            Assert.AreEqual(12, _game.ComputeScore());
        }

        [Test]
        public void RollAStrike()
        {
            Roll(10);
            Roll(4);
            Roll(1);
            Assert.AreEqual(20, _game.ComputeScore());
        }

        [Test]
        public void RollAPerfectGame()
        {
            RollMany(12, 10);
            Assert.AreEqual(300, _game.ComputeScore());
        }

        [Test]
        public void RollAAlmostPerfectGame()
        {
            RollMany(11, 10);
            Roll(1);
            Assert.AreEqual(291, _game.ComputeScore());
        }

        [Test]
        public void RollASpecialSpare()
        {
            Roll(0);
            Roll(10);
            Roll(1);
            Roll(1);
            Assert.AreEqual(13, _game.ComputeScore());
        }

        [Test]
        public void DummyPlay1()
        {
            Roll(5);
            Roll(5);
            Roll(4);
            Roll(5);
            Roll(8);
            Roll(2);
            Roll(10);
            Roll(0);
            Roll(10);
            Roll(10);
            Roll(6);
            Roll(2);
            Roll(10);
            Roll(4);
            Roll(6);
            Roll(10);
            Roll(10);
            Roll(0);
            Assert.AreEqual(169, _game.ComputeScore());
        }

        [Test]
        public void DummyPlay2()
        {
            Roll(5);
            Roll(5);
            Roll(4);
            Roll(0);
            Roll(8);
            Roll(1);
            Roll(10);
            Roll(0);
            Roll(10);
            Roll(10);
            Roll(10);
            Roll(10);
            Roll(4);
            Roll(6);
            Roll(10);
            Roll(10);
            Roll(5);
            Assert.AreEqual(186, _game.ComputeScore());
        }

        private void RollMany(int rollNumber, int pinsCount)
        {
            for (var rollIndex = 1; rollIndex <= rollNumber; rollIndex++)
            {
                Roll(pinsCount);
            }
        }

        private void Roll(int pinsCount)
        {
            _game.Roll(pinsCount);
        }
    }
}
