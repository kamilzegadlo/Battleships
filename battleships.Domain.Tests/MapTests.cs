using BattleShips.Domain;
using BattleShips.Domain.Ships;
using CSharpFunctionalExtensions;
using FakeItEasy;
using Xunit;

namespace Battleships.Domain.Tests
{
    public class MapTests
    {
        [Fact]
        public void Should_Init_Map()
        {
            ushort sizeX = 2;
            ushort sizeY = 5;

            var mapResult = Map.Create(new PlacementValidator(), sizeX, sizeY);

            Assert.True(mapResult.IsSuccess);

            for(ushort x=0; x<sizeX; ++x)
                for (ushort y = 0; y < sizeY; ++y)
                {
                    var cellResult = mapResult.Value.GetCell(x, y);
                    Assert.True(cellResult.IsSuccess);
                    Assert.True(cellResult.Value.IsEmpty);
                    Assert.False(cellResult.Value.WasShot);
                }
        }

        [Theory]
        [InlineData(0,5)]
        [InlineData(2, 0)]
        [InlineData(0, 0)]
        [InlineData(101, 0)]
        [InlineData(0, 101)]
        [InlineData(101, 101)]
        public void Incorrect_Map_Size(ushort x, ushort y)
        {
            Assert.True(Map.Create(new PlacementValidator(), x, y).IsFailure);
        }

        [Fact]
        public void GetCell_Incorrect_Coordinates()
        {
            ushort sizeX = 2;
            ushort sizeY = 5;

            var mapResult = Map.Create(new PlacementValidator(), sizeX, sizeY);

            Assert.True(mapResult.Value.GetCell(sizeX, 1).IsFailure);
        }

        [Fact]
        public void Should_Add_Ship()
        {
            var placementValdiator = A.Fake<IPlacementValidator>();
            var newShip = new Destroyer(new Coordinate(1, 1), ShipOrientation.Horizontally);

            A.CallTo(() => placementValdiator.Validate(A<Map>._, newShip)).Returns(Result.Success());

            ushort sizeX = 5;
            ushort sizeY = 2;

            var map = Map.Create(new PlacementValidator(), sizeX, sizeY).Value;

            Assert.True(map.AddShip(newShip).IsSuccess);

            Assert.Single(map.Ships);

            for(ushort x=0; x<sizeX; ++x)
                for(ushort y=0; y<sizeY;++y)
                    if (newShip.Coordinates.Contains(new Coordinate(x, y)))
                    {
                        Assert.False(map.GetCell(x, y).Value.IsEmpty);
                        Assert.False(map.GetCell(x, y).Value.WasShot);
                    }
                    else
                    {
                        Assert.True(map.GetCell(x, y).Value.IsEmpty);
                        Assert.False(map.GetCell(x, y).Value.WasShot);
                    }
        }

        [Fact]
        public void Cannot_Add_Ship()
        {
            var placementValdiator = A.Fake<IPlacementValidator>();
            var newShip = new Destroyer(new Coordinate(1, 1), ShipOrientation.Horizontally);

            A.CallTo(() => placementValdiator.Validate(A<Map>._, newShip)).Returns(Result.Failure("unit test failure"));

            ushort sizeX = 5;
            ushort sizeY = 2;

            var map = Map.Create(placementValdiator, sizeX, sizeY).Value;

            Assert.True(map.AddShip(newShip).IsFailure);

            Assert.Empty(map.Ships);

            for (ushort x = 0; x < sizeX; ++x)
                for (ushort y = 0; y < sizeY; ++y)
                {
                    Assert.True(map.GetCell(x, y).Value.IsEmpty);
                    Assert.False(map.GetCell(x, y).Value.WasShot);
                }
        }
    }
}
