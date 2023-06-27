using BattleShips.Domain;
using BattleShips.Domain.Ships;
using Xunit;

namespace Battleships.Domain.Tests
{
    public  class PlacementValidatorTests
    {
        private PlacementValidator _sut;

        public PlacementValidatorTests()
        {
            _sut = new PlacementValidator();
        }

        [Fact]
        public void Should_Pass()
        {
            var map = Map.Create(_sut, 10, 10).Value;
            var ship = new Destroyer(new Coordinate(1, 1), ShipOrientation.Horizontally);

            Assert.True(_sut.Validate(map, ship).IsSuccess);
        }

        [Theory]
        [InlineData(7,1, ShipOrientation.Horizontally)]
        [InlineData(1, 7, ShipOrientation.Vertically)]
        public void Not_Fully_On_Map(ushort x, ushort y, ShipOrientation orientation)
        {
            var map = Map.Create(_sut, 10, 10).Value;
            var ship = new Destroyer(new Coordinate(x, y), orientation);

            Assert.True(_sut.Validate(map, ship).IsFailure);
        }

        [Fact]
        public void Overlaps_With_Other_Ship()
        {
            var map = Map.Create(_sut, 10, 10).Value;
            var ship = new Destroyer(new Coordinate(1, 1), ShipOrientation.Horizontally);
            map.AddShip(ship);
            var ship2 = new Destroyer(new Coordinate(4, 1), ShipOrientation.Horizontally);

            Assert.True(_sut.Validate(map, ship2).IsFailure);
        }

        [Fact]
        public void Should_Pass_2_Ships()
        {
            var map = Map.Create(_sut, 10, 10).Value;
            var ship = new Destroyer(new Coordinate(1, 1), ShipOrientation.Horizontally);
            map.AddShip(ship);
            var ship2 = new Destroyer(new Coordinate(5, 1), ShipOrientation.Horizontally);

            Assert.True(_sut.Validate(map, ship2).IsSuccess);
        }
    }
}
