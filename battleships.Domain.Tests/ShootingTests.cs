using BattleShips.Domain;
using FakeItEasy;
using Xunit;

namespace Battleships.Domain.Tests
{
    public class ShootingTests
    {
        private readonly GameSettings _settings;

        private readonly Game _sut;

        public ShootingTests()
        {
            _settings = new GameSettings { NumberOfDestroyers = 1, NumberOfBattleships = 0, SizeX = 3, SizeY = 4 };

            _sut = Game.Create(new RandomPlacementAllocator(), new PlacementValidator(), _settings).Value;
        }

        [Fact]
        public void Shoot_Returns_True_When_Hit()
        {
            var ship = _sut.Map.Ships.First();
            Assert.True(_sut.Shoot(ship.ShipBowCoordinate).Value);
        }

        [Fact]
        public void Shoot_Returns_False_When_Miss()
        {
            var ship = _sut.Map.Ships.First();

            for(ushort x = 0; x<_settings.SizeX; ++x)
                for(ushort y=0; y<_settings.SizeY; ++y)
                    if (!ship.Coordinates.Contains(new Coordinate(x, y)))
                    {
                        Assert.False(_sut.Shoot(new Coordinate(x, y)).Value);
                        break;
                    }
        }

        [Fact]
        public void Game_Already_Finished()
        {
            var ship = _sut.Map.Ships.First();
            foreach (var c in ship.Coordinates)
                Assert.True(_sut.Shoot(c).IsSuccess);

            Assert.True(_sut.Shoot(new Coordinate(1, 2)).IsFailure);
        }

        [Fact]
        public void Shoot_Outside_Of_The_Map()
        {
            Assert.True(_sut.Shoot(new Coordinate(_settings.SizeX, 1)).IsFailure);
        }

        [Fact]
        public void Returns_False_If_Coordinates_Already_Hit()
        {
            var ship = _sut.Map.Ships.First();
            _sut.Shoot(ship.ShipBowCoordinate);
            Assert.False(_sut.Shoot(ship.ShipBowCoordinate).Value);
        }

        [Fact]
        public void Still_Returns_False_If_Coordinates_Already_Missed()
        {
            var ship = _sut.Map.Ships.First();

            for (ushort x = 0; x < _settings.SizeX; ++x)
                for (ushort y = 0; y < _settings.SizeY; ++y)
                if (!ship.Coordinates.Contains(new Coordinate(x, y)))
                {
                    _sut.Shoot(new Coordinate(x, y));
                    Assert.False(_sut.Shoot(new Coordinate(x, y)).Value);
                    break;
                }
        }
    }
}
