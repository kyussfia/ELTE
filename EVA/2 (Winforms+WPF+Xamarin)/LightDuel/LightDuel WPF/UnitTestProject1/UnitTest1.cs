using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightDuel_WinForms.Model;

namespace LightDuelWpfTest
{
    [TestClass]
    public class UnitTest1
    {
        private LightDuelModel model = new LightDuelModel();

        [TestMethod]
        public void testModelNotNull()
        {
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void testModelInitialized()
        {
            Assert.AreEqual(500, model.speed);
            Assert.AreEqual(0, model.GameSize);
            Assert.IsNull(model.fields);
            Assert.IsNull(model.Blue);
            Assert.IsNull(model.Red);
        }

        private void startGameTester(int size)
        {
            this.model.ticked += new EventHandler<PlayerMoveEventArgs>(tickedTest);
            this.model.newGame(size);
            Assert.IsTrue(this.model.isTimerWorks());
            Assert.IsFalse(this.model.blueLost);
            Assert.IsFalse(this.model.redLost);
            Assert.AreEqual(size, this.model.GameSize);
            Assert.AreEqual(size, this.model.fields.Count);
            Assert.IsNotNull(this.model.Blue);
            Assert.IsNotNull(this.model.Red);
            Assert.AreEqual((size / 2) - 1, this.model.Blue.row);
            Assert.AreEqual(0, this.model.Blue.col);
            Assert.AreEqual(2, this.model.Blue.dir);
            Assert.IsTrue(this.model.Blue.type == LightDuelModel.Players.Blue);
            Assert.AreEqual((size / 2), this.model.Red.row);
            Assert.AreEqual(size - 1, this.model.Red.col);
            Assert.AreEqual(4, this.model.Red.dir);
            Assert.IsTrue(this.model.Red.type == LightDuelModel.Players.Red);

            Assert.IsTrue(this.model.fields[0][0] == LightDuelModel.Players.No);
        }

        private void tickedTest(object sender, PlayerMoveEventArgs e)
        {
            Assert.IsNotNull(e.BlueX);
            Assert.IsNotNull(e.BlueY);
            Assert.IsNotNull(e.RedX);
            Assert.IsNotNull(e.RedY);
            Assert.IsTrue(this.model.fields[e.BlueX][e.BlueY] == LightDuelModel.Players.Blue);
            Assert.IsTrue(this.model.fields[e.RedX][e.RedY] == LightDuelModel.Players.Red);
            Assert.IsTrue(this.model.isTimerWorks());
        }

        [TestMethod]
        public void testStartGameLittle()
        {
            this.startGameTester(12);
        }

        [TestMethod]
        public void testStartGameMid()
        {
            this.startGameTester(24);
        }

        [TestMethod]
        public void testStartGameLarge()
        {
            this.startGameTester(36);
        }

        [TestMethod]
        public void testModelMethodIsBluePos()
        {
            this.startGameTester(12);
            Assert.IsTrue(this.model.isBluePosition(0, 5));
        }

        [TestMethod]
        public void testModelMethodIsRedPos()
        {
            this.startGameTester(12);
            Assert.IsTrue(this.model.isRedPosition(11, 6));
        }

        [TestMethod]
        public void testStartGameMultipleTimes()
        {
            this.startGameTester(12);
            this.startGameTester(24);
            this.startGameTester(36);
            this.startGameTester(12);
            Assert.IsTrue(this.model.fields.Count == 12);
        }

        [TestMethod]
        public void testPauseGame()
        {
            this.startGameTester(12);
            Assert.IsTrue(this.model.isTimerWorks());
            this.model.handlePausing(true);
            Assert.IsTrue(this.model.isTimerWorks());
            this.model.handlePausing(false);
            Assert.IsFalse(this.model.isTimerWorks());
            this.model.handlePausing(true);
            Assert.IsTrue(this.model.isTimerWorks());
        }

        [TestMethod]
        public void testControllingBluePlayer()
        {
            this.startGameTester(12);
            this.model.Blue.left();
            Assert.AreEqual(1, this.model.Blue.dir);
            this.model.Blue.left();
            Assert.AreEqual(4, this.model.Blue.dir);
            this.model.Blue.right();
            Assert.AreEqual(1, this.model.Blue.dir);
        }

        [TestMethod]
        public void testControllingRedPlayer()
        {
            this.startGameTester(12);
            this.model.Red.left();
            this.model.performTick();
            Assert.AreEqual(3, this.model.Red.dir);
            Assert.AreEqual(11, this.model.Red.col);
            Assert.AreEqual(7, this.model.Red.row);
            this.model.Red.left();
            Assert.AreEqual(2, this.model.Red.dir);
            this.model.Red.right();
            Assert.AreEqual(3, this.model.Red.dir);
        }

        [TestMethod]
        public void testTiedGame()
        {
            int gameSize = 12;
            this.startGameTester(gameSize);

            for (int i = 0; i < gameSize; i++)
            {
                this.model.performTick();
            }

            Assert.IsFalse(this.model.isTimerWorks());
            Assert.IsTrue(this.model.redLost && this.model.blueLost);
        }

        public void playerWonTester(LightDuelModel.Player winner, int gameSize)
        {
            for (int i = 0; i < gameSize; i++)
            {
                if (i == gameSize - 1)
                {
                    winner.left();
                }
                this.model.performTick();
            }

            if (winner.type == LightDuelModel.Players.Blue)
            {
                Assert.IsTrue(this.model.redLost);
                Assert.IsFalse(this.model.blueLost);
            }
            else
            {
                Assert.IsTrue(this.model.blueLost);
                Assert.IsFalse(this.model.redLost);
            }

            Assert.IsFalse(this.model.isTimerWorks());
        }

        [TestMethod]
        public void testBlueWon()
        {
            this.startGameTester(12);
            this.playerWonTester(this.model.Blue, 12);
            this.startGameTester(24);
            this.playerWonTester(this.model.Blue, 24);
            this.startGameTester(36);
            this.playerWonTester(this.model.Blue, 36);
        }

        [TestMethod]
        public void testRedWon()
        {
            this.startGameTester(12);
            this.playerWonTester(this.model.Red, 12);
            this.startGameTester(24);
            this.playerWonTester(this.model.Red, 24);
            this.startGameTester(36);
            this.playerWonTester(this.model.Red, 36);
        }
    }
}
