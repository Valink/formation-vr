using NUnit.Framework;

namespace App._4.bowling.score
{
    public class BowlingGameTests
    {
        private BowlingGame _bowlingGame;

        [SetUp]
        public void SetUp()
        {
            _bowlingGame = new BowlingGame(10);
        }
        
        [Test]
        public void RollNoPinInGame()
        {
            RollMany(20, 0);
            Assert.AreEqual(0, _bowlingGame.ComputeScore());
        }

        [Test]
        public void Roll20PinsInGame()
        {
            RollMany(20, 1);
            Assert.AreEqual(20, _bowlingGame.ComputeScore());
        }

        [Test]
        public void RollASpare()
        {
            Roll(6);
            Roll(4);
            Roll(1);
            Assert.AreEqual(12, _bowlingGame.ComputeScore());
        }

        [Test]
        public void RollAStrike()
        {
            Roll(10);
            Roll(4);
            Roll(1);
            Assert.AreEqual(20, _bowlingGame.ComputeScore());
        }

        [Test]
        public void RollAPerfectGame()
        {
            RollMany(12, 10);
            Assert.AreEqual(300, _bowlingGame.ComputeScore());
        }

        [Test]
        public void RollAAlmostPerfectGame()
        {
            RollMany(11, 10);
            Roll(1);
            Assert.AreEqual(291, _bowlingGame.ComputeScore());
        }

        [Test]
        public void RollASpecialSpare()
        {
            Roll( 0);
            Roll( 10);
            Roll( 1);
            Roll( 1);
            Assert.AreEqual(13, _bowlingGame.ComputeScore());
        }
        
        [Test]
        public void DummyPlay1()
        {
            Roll( 5);
            Roll( 5);
            Roll( 4);
            Roll( 5);
            Roll( 8);
            Roll( 2);
            Roll( 10);
            Roll( 0);
            Roll( 10);
            Roll( 10);
            Roll( 6);
            Roll( 2);
            Roll( 10);
            Roll( 4);
            Roll( 6);
            Roll( 10);
            Roll( 10);
            Roll( 0);
            Assert.AreEqual(169, _bowlingGame.ComputeScore());
        }

        [Test]
        public void DummyPlay2()
        {
            Roll( 5);
            Roll( 5);
            Roll( 4);
            Roll( 0);
            Roll( 8);
            Roll( 1);
            Roll( 10);
            Roll( 0);
            Roll( 10);
            Roll( 10);
            Roll( 10);
            Roll( 10);
            Roll( 4);
            Roll( 6);
            Roll( 10);
            Roll( 10);
            Roll( 5);
            Assert.AreEqual(186, _bowlingGame.ComputeScore());
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
            _bowlingGame.Roll(pinsCount);
        }
    }
}