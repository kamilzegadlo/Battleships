using BattleShips.Domain;
using BattleShips.Domain.Ships;
using FakeItEasy;
using Xunit;

namespace Battleships.Domain.Tests
{
    public class PlacementAllocatorTests
    {
        private readonly IPlacementValidator _placementValidator;

        private RandomPlacementAllocator _sut;

        public PlacementAllocatorTests()
        {
            _placementValidator = A.Fake<IPlacementValidator>();

            _sut = new RandomPlacementAllocator();
        }
        
        [Fact]
        public void Allocate_2_Destroyers_And_1_Battleship()
        {
            var map = Map.Create(_placementValidator, 10, 10).Value;

            Assert.True(_sut.AllocateShips(map, 2, 1).IsSuccess);

            Assert.Equal(3, map.Ships.Count());
        }

        [Fact]
        public void Map_Already_Contains_Ship()
        {
            var map = Map.Create(_placementValidator, 10, 10).Value;
            var ship = new Destroyer(new Coordinate(1, 1), ShipOrientation.Horizontally);
            map.AddShip(ship);

            Assert.True(_sut.AllocateShips(map, 2, 1).IsFailure);
        }

        [Fact]
        public void Too_Many_Ships()
        {
            var map = Map.Create(_placementValidator, 5, 6).Value;
            Assert.True(_sut.AllocateShips(map, 7, 1).IsFailure);
        }

        [Fact]
        public void No_Ships()
        {
            var map = Map.Create(_placementValidator, 10, 10).Value;
            Assert.True(_sut.AllocateShips(map, 0, 0).IsFailure);
        }
    }
}
