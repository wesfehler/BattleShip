using System;
using BattleShipModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipUnitTests.ModelTests
{
    [TestClass]
    public class BoardCoordinatesTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CanCreateValidCoordinatesFromString()
        {
            var coordinate = new BoardCoordinate("A1");
            Assert.AreEqual(coordinate.Column, 1);
            Assert.AreEqual(coordinate.Row, 1);

        }

        [TestMethod]
        public void CanCreateValidCoordinatesFromInts()
        {
            var coordinate = new BoardCoordinate(2, 2);
            Assert.AreEqual(coordinate.Column, 2);
            Assert.AreEqual(coordinate.Row, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalid coordinate was allowed.")]
        [DeploymentItem("InvalidCoordinates.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\InvalidCoordinates.xml", "Row",
            DataAccessMethod.Sequential)]
        public void CannotCreateInvalidCoordinates()
        {
            var coordinate = new BoardCoordinate((string) TestContext.DataRow["Coordinate"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Invalid coordinate was allowed.")]
        public void CannotCreateWithoutCoordinates()
        {
            var coordinate = new BoardCoordinate(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentOutOfRangeException), "Invalid coordinate was allowed.")]
        public void CannotCreateInvalidCoordinatesFromInt()
        {
            var coordinate = new BoardCoordinate(0, 8);
        }

    }
}
