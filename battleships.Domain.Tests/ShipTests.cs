using BattleShips.Domain;
using BattleShips.Domain.Ships;
using Xunit;

namespace battleships.Domain.Tests
{
    public class ShipTests
    {
        [Fact]
        public void Should_Return_Proper_Coordinates_For_Destroyer_Horizontally()
        {
            var ship = new Destroyer( new Coordinate(1,2), ShipOrientation.Horizontally);

            var coordinates = ship.Coordinates.ToList();

            Assert.True(coordinates.Count == 4);

            Assert.Contains(new Coordinate(1, 2), coordinates);
            Assert.Contains(new Coordinate(2, 2), coordinates);
            Assert.Contains(new Coordinate(3, 2), coordinates);
            Assert.Contains(new Coordinate(4, 2), coordinates);
        }

        [Fact]
        public void Should_Return_Proper_Coordinates_For_Destroyer_Vertically()
        {
            var ship = new Destroyer(new Coordinate(1, 2), ShipOrientation.Vertically);

            var coordinates = ship.Coordinates.ToList();

            Assert.True(coordinates.Count == 4);

            Assert.Contains(new Coordinate(1, 2), coordinates);
            Assert.Contains(new Coordinate(1, 3), coordinates);
            Assert.Contains(new Coordinate(1, 4), coordinates);
            Assert.Contains(new Coordinate(1, 5), coordinates);
        }

        [Fact]
        public void Should_Not_Throw_Exception_When_Ship_Has_0_Length()
        {
            var ship = new UnitTestShip(new Coordinate(1, 2), ShipOrientation.Vertically);

            var coordinates = ship.Coordinates.ToList();

            Assert.True(coordinates.Count == 0);
        }

        private class UnitTestShip : Ship
        {
            internal override ushort Length => 0;

            public UnitTestShip(Coordinate shipBowCoordinate, ShipOrientation orientation) : base(shipBowCoordinate, orientation)
            {
            }
        }
    }
}