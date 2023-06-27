using BattleShips.Domain.Ships;
using CSharpFunctionalExtensions;

namespace BattleShips.Domain
{
    internal interface IPlacementValidator
    {
        Result Validate(Map map, Ship ship);
    }

    internal class PlacementValidator : IPlacementValidator
    {
        public Result Validate(Map map, Ship ship) =>
            Result.Combine(IsFullyLocatedOnMap(map, ship), IsNotOverlappingOtherShip(map, ship));
        
        private static Result IsFullyLocatedOnMap(Map map, Ship ship)
        {
            foreach (var c in ship.Coordinates)
                if (c.X >= map.SizeX || c.Y >= map.SizeY)
                    return Result.Failure("Ship is not fully located in the map.");

            return Result.Success();
        }

       private static Result IsNotOverlappingOtherShip(Map map, Ship ship) =>
           map.Ships.SelectMany(s => s.Coordinates).Intersect(ship.Coordinates).Any()
               ? Result.Failure("Ships overlap.")
               : Result.Success();
    }
}
