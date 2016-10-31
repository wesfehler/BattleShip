using System;
using BattleShipModel;
using BattleShipService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipUnitTests.ServiceTests
{
    [TestClass]
    public class GameSimulation
    {
        [TestMethod]
        public void RunGame()
        {
            Game game = new Game();

            Player player1 = new Player("Player One");
            Player player2 = new Player("Player Two");

            game.PlayerOne = player1;
            game.PlayerTwo = player2;

            game.PlaceShip(player1, ShipType.Cruiser, "A1", "A3");
            game.PlaceShip(player2, ShipType.Cruiser, "A1", "C1");

            // Game is not over
            Assert.AreEqual(false, game.Over);

            game.Start();

            MarkerType result;

            // Player 1 Miss
            result = game.Call(new BoardCoordinate("D5"));
            Assert.AreEqual(result, MarkerType.Miss);

            // Game is not over
            Assert.AreEqual(false, game.Over);

            // Player 2 Hit
            result = game.Call(new BoardCoordinate("A1"));
            Assert.AreEqual(result, MarkerType.Hit);

            // Game is not over
            Assert.AreEqual(false, game.Over);

            // Player 1 Fire same spot
            result = game.Call(new BoardCoordinate("D5"));
            Assert.AreEqual(result, MarkerType.Miss);

            // Game is not over
            Assert.AreEqual(false, game.Over);

            // Player 2 Fire same spot
            result = game.Call(new BoardCoordinate("A1"));
            Assert.AreEqual(result, MarkerType.Hit);

            // Game is not over
            Assert.AreEqual(false, game.Over);

            // Player 1 Miss
            result = game.Call(new BoardCoordinate("D6"));
            Assert.AreEqual(result, MarkerType.Miss);

            // Game is not over
            Assert.AreEqual(false, game.Over);

            // Player 2 Hit
            result = game.Call(new BoardCoordinate("A2"));
            Assert.AreEqual(result, MarkerType.Hit);

            // Game is not over
            Assert.AreEqual(false, game.Over);

            // Player 1 Miss
            result = game.Call(new BoardCoordinate("E5"));
            Assert.AreEqual(result, MarkerType.Miss);

            // Game is not over
            Assert.AreEqual(false, game.Over);

            // Player 2 Hit
            result = game.Call(new BoardCoordinate("A3"));
            Assert.AreEqual(result, MarkerType.Hit);

            // Game over
            Assert.AreEqual(true, game.Over);

            // Player 2 Winner
            Assert.AreEqual(player2, game.Winner);

            // Player 1 is not the Winner
            Assert.AreNotEqual(player1, game.Winner);


        }
    }
}
