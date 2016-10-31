using System;
using BattleShipModel;
using BattleShipService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipUnitTests.ServiceTests
{
    [TestClass]
    public class GameTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CanStartGameWithTwoPlayers()
        {

            try
            {
                var testGame = new Game { PlayerOne = new Player("one"), PlayerTwo = new Player("two") };
                testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, "A1", "A3");
                testGame.PlaceShip(testGame.PlayerTwo, ShipType.Cruiser, "A1", "A3");

                testGame.Start();
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Game allowed to start with invalid players.")]
        public void CannotStartGameWithoutTwoPlayers()
        {
            var testGame = new Game();

            testGame.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Game allowed to start without ships.")]
        public void CannotStartGameWithoutShips()
        {
            var testGame = new Game();

            testGame.PlayerOne = new Player("one");
            testGame.PlayerTwo = new Player("two");

            testGame.Start();
        }

        [TestMethod]
        public void NewGameShouldNotHaveWinner()
        {
            var testGame = new Game();

            testGame.PlayerOne = new Player("one");
            testGame.PlayerTwo = new Player("two");

            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, "A1", "C1");
            testGame.PlaceShip(testGame.PlayerTwo, ShipType.Cruiser, "A1", "A3");

            testGame.Start();

            Assert.IsNull(testGame.Winner);
        }

        [TestMethod]
        public void PlayersShouldHaveBoardsWhenGameIsStarted()
        {
            var testGame = new Game { PlayerOne = new Player("one"), PlayerTwo = new Player("two") };
            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, "A1", "A3");
            testGame.PlaceShip(testGame.PlayerTwo, ShipType.Cruiser, "A1", "A3");
            testGame.Start();
            Assert.IsNotNull(testGame.PlayerOne.Board);
            Assert.IsNotNull(testGame.PlayerTwo.Board);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Invalid coordinates were allowed.")]
        [DeploymentItem("BadCoordinateData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML","|DataDirectory|\\BadCoordinateData.xml","Row",DataAccessMethod.Sequential)]
        public void CannotPlaceShipWithInvalidCoordinates()
        {
            var testGame = new Game { PlayerOne = new Player("one"), PlayerTwo = new Player("two") };
            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, "A1", "A3");
            testGame.PlaceShip(testGame.PlayerTwo, ShipType.Cruiser, "A1", "A3");

            testGame.Start();
            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, (string)TestContext.DataRow["Coordinate1"], (string)TestContext.DataRow["Coordinate2"]);
        }

        [TestMethod]
        [DeploymentItem("GoodCoordinateData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\GoodCoordinateData.xml", "Row", 
            DataAccessMethod.Sequential)]
        public void CanPlaceShipWithValidCoordinates()
        {
            var testGame = new Game { PlayerOne = new Player("one"), PlayerTwo = new Player("two") };
            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, (string)TestContext.DataRow["Coordinate1"], (string)TestContext.DataRow["Coordinate2"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalid coordinates were allowed.")]
        [DeploymentItem("BadCallData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\BadCallData.xml", "Row",
            DataAccessMethod.Sequential)]
        public void CannotCallInvalidCoordinates()
        {
            var testGame = new Game { PlayerOne = new Player("one"), PlayerTwo = new Player("two") };
            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, "A1", "A3");
            testGame.PlaceShip(testGame.PlayerTwo, ShipType.Cruiser, "A1", "A3");
            testGame.Start();
            testGame.Call(new BoardCoordinate((string)TestContext.DataRow["Coordinate"]));
        }

        [TestMethod]
        public void CanCallValidCoordinates()
        {
            var testGame = new Game { PlayerOne = new Player("one"), PlayerTwo = new Player("two") };
            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, "A1", "A3");
            testGame.PlaceShip(testGame.PlayerTwo, ShipType.Cruiser, "A1", "A3");
            testGame.Start();
            testGame.Call(new BoardCoordinate("C3"));
        }

        [TestMethod]
        public void CanHitAPlacedShip()
        {
            var testGame = new Game { PlayerOne = new Player("one"), PlayerTwo = new Player("two") };
            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, "A1", "A3");
            testGame.PlaceShip(testGame.PlayerTwo, ShipType.Cruiser, "A1", "A3");
            testGame.Start();
            MarkerType markerType = testGame.Call(new BoardCoordinate("A1"));
            Assert.AreEqual(markerType, MarkerType.Hit);
        }
        [TestMethod]
        public void CanMissAPlacedShip()
        {
            var testGame = new Game { PlayerOne = new Player("one"), PlayerTwo = new Player("two") };
            testGame.PlaceShip(testGame.PlayerOne, ShipType.Cruiser, "A1", "A3");
            testGame.PlaceShip(testGame.PlayerTwo, ShipType.Cruiser, "A1", "A3");
            testGame.Start();
            MarkerType markerType = testGame.Call(new BoardCoordinate("B1"));
            Assert.AreEqual(markerType, MarkerType.Miss);
        }

        [TestMethod]
        [ExpectedException(typeof (ApplicationException), "Invalid coordinates were allowed.")]
        public void ShipsCannotOverlap()
        {
            Game game = new Game();

            Player player1 = new Player("Player One");
            Player player2 = new Player("Player Two");

            game.PlayerOne = player1;
            game.PlayerTwo = player2;

            game.PlaceShip(player1, ShipType.Cruiser, "A1", "A3");
            game.PlaceShip(player1, ShipType.Cruiser, "A1", "A3");
        }

        [TestMethod]
        public void CanSinkAShip()
        {
            Game game = new Game();

            Player player1 = new Player("Player One");
            Player player2 = new Player("Player Two");

            game.PlayerOne = player1;
            game.PlayerTwo = player2;

            game.PlaceShip(player1, ShipType.Cruiser, "A1", "A3");
            game.PlaceShip(player2, ShipType.Cruiser, "A1", "A3");

            Assert.AreEqual(1, game.PlayerOne.Board.Ships.Count); 

            game.Start();

            game.Call("A1"); // player 1
            game.Call("B1"); // player 2

            game.Call("A2"); // player 1
            game.Call("B2"); // player 2

            game.Call("A3"); // player 1
            game.Call("B3"); // player 2

            Assert.AreEqual(0, game.PlayerTwo.Board.Ships.Count); 
        }
    }
}
