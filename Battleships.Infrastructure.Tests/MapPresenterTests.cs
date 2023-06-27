using Battleships.UI.Console.Infrastructure;
using BattleShips.Domain;
using BattleShips.Domain.Ships;
using Xunit;

namespace Battleships.Infrastructure.Tests
{
    public class MapPresenterTests
    {
        [Fact]
        public void Should_Display_Map()
        {
            var map = Map.Create(new PlacementValidator(), 10, 10).Value;
            map.AddShip(new Destroyer(new Coordinate(1, 1), ShipOrientation.Horizontally));
            map.AddShip(new Battleship(new Coordinate(2, 4), ShipOrientation.Vertically));
            map.Shoot(new Coordinate(2, 1));
            map.Shoot(new Coordinate(1, 2));

            var displayedMapResult = MapPresenter.Display(map);

            Assert.True(displayedMapResult.IsSuccess);

            Assert.Equal(
                "Legend: 0 - missed, X - hit, ? - not hit yet\r\n ABCDEFGHIJ\r\n0??????????\r\n1??X???????\r\n2?0????????\r\n3??????????\r\n4??????????\r\n5??????????\r\n6??????????\r\n7??????????\r\n8??????????\r\n9??????????\r\n",
                displayedMapResult.Value);
        }

        [Fact]
        public void Map_Size_Not_Supported()
        {
            var map = Map.Create(new PlacementValidator(), 11, 10).Value;

            Assert.True(MapPresenter.Display(map).IsFailure);
        }
    }
}