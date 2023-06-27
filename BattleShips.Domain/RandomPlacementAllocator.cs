using BattleShips.Domain.Ships;
using CSharpFunctionalExtensions;

namespace BattleShips.Domain
{
    public interface IPlacementAllocator
    {
        Result AllocateShips(Map map, ushort numberOfDestroyers, ushort numberOfBattleships);
    }

    internal class RandomPlacementAllocator : IPlacementAllocator
    {
        private readonly Random _random;

        public RandomPlacementAllocator()
        {
            _random = new Random();
        }

        public Result AllocateShips(Map map, ushort numberOfDestroyers, ushort numberOfBattleships)
        {
            if (map.Ships.Any())
                return Result.Failure("Map already contains ships");

            if (map.SizeX * map.SizeY <
                numberOfDestroyers * Destroyer.ShipLength + numberOfBattleships * Battleship.ShipLength)
                return Result.Failure("Given Map cannot contain to many ships");

            if (numberOfDestroyers == 0 && numberOfBattleships == 0)
                return Result.Failure("Map has to contain at least one ship.");

            AllocateDestroyers(map, numberOfDestroyers);
            AllocateBattleships(map, numberOfBattleships);

            return Result.Success();
        }

        private void AllocateDestroyers(Map map, ushort numberOfDestroyers)
        {
            for (ushort i = 0; i < numberOfDestroyers;)
            {
                var newShipOrientation = RandomizeShipOrientation();

                var newShipBowCoordinate = RandomizeShipBowCoordinate(map, newShipOrientation, Destroyer.ShipLength);

                var newShip = new Destroyer(newShipBowCoordinate, newShipOrientation);

                if (map.AddShip(newShip).IsSuccess)
                    ++i;
            }
        }

        private void AllocateBattleships(Map map, ushort numberOfBattleships)
        {
            for (ushort i = 0; i < numberOfBattleships;)
            {
                var newShipOrientation = RandomizeShipOrientation();

                var newShipBowCoordinate = RandomizeShipBowCoordinate(map, newShipOrientation, Battleship.ShipLength);

                var newShip = new Battleship(newShipBowCoordinate, newShipOrientation);

                if (map.AddShip(newShip).IsSuccess)
                    ++i;
            }
        }

        private ShipOrientation RandomizeShipOrientation() =>
            _random.Next(0, 2) == 1 ? ShipOrientation.Horizontally : ShipOrientation.Vertically;

        private Coordinate RandomizeShipBowCoordinate(Map map, ShipOrientation orientation, ushort shipLength) =>
            orientation == ShipOrientation.Horizontally
                ? new Coordinate((ushort) _random.Next(0, map.SizeX - shipLength + 1),
                    (ushort) _random.Next(0, map.SizeY))
                : new Coordinate((ushort) _random.Next(0, map.SizeX),
                    (ushort) _random.Next(0, map.SizeY - shipLength + 1));
    }
}
