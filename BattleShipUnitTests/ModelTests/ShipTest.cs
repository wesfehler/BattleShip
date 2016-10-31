using System;
using BattleShipModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipUnitTests.ModelTests
{
    [TestClass]
    public class ShipTest
    {
        [TestMethod]
        public void CanCreateCruiser()
        {
            var ship = new Cruiser(new BoardCoordinate("A1"), new BoardCoordinate("A3") );
            Assert.IsNotNull(ship);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalid coordinate was allowed.")]
        public void CannotCreateShipTooLong()
        {
            var ship = new Cruiser(new BoardCoordinate("A1"), new BoardCoordinate("A4"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalid coordinate was allowed.")]
        public void CannotCreateShipTooShort()
        {
            var ship = new Cruiser(new BoardCoordinate("A1"), new BoardCoordinate("A1"));
        }

        [TestMethod]
        public void CanCreateCruiserVerticalCoordinates()
        {
            var ship = new Cruiser(new BoardCoordinate("A1"), new BoardCoordinate("A3"));
            Assert.AreEqual(ship.Coordinates.Length, 3, "Incorrect length");
            Assert.AreEqual(ship.Coordinates[0].ToString(), "A1", "Incorrect Coordinates");
            Assert.AreEqual(ship.Coordinates[1].ToString(), "A2", "Incorrect Coordinates");
            Assert.AreEqual(ship.Coordinates[2].ToString(), "A3", "Incorrect Coordinates");
        }

        [TestMethod]
        public void CanCreateCruiserHorizontalCoordinates()
        {
            var ship = new Cruiser(new BoardCoordinate("A1"), new BoardCoordinate("C1"));
            Assert.AreEqual(ship.Coordinates.Length, 3, "Incorrect length");
            Assert.AreEqual(ship.Coordinates[0].ToString(), "A1", "Incorrect Coordinates");
            Assert.AreEqual(ship.Coordinates[1].ToString(), "B1", "Incorrect Coordinates");
            Assert.AreEqual(ship.Coordinates[2].ToString(), "C1", "Incorrect Coordinates");
        }

    }
}
