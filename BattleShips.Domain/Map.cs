using BattleShips.Domain.Ships;
using CSharpFunctionalExtensions;

namespace BattleShips.Domain
{
    public class Map
    {
        private readonly MapCell[,] _map;
        private readonly IPlacementValidator _placementValidator;

        public ushort SizeX => (ushort) _map.GetLength(0);

        public ushort SizeY => (ushort) _map.GetLength(1);

        private ICollection<Ship> _ships { get; } = new List<Ship>();
        internal IEnumerable<Ship> Ships => _ships;

        private Map(IPlacementValidator placementValidator, ushort sizeX, ushort sizeY)
        {
            _placementValidator = placementValidator;

            _map = new MapCell[sizeX, sizeY];
            for (ushort x = 0; x < SizeX; ++x)
                for (ushort y = 0; y < SizeY; ++y)
                    _map[x, y] = new MapCell();
        }

        internal static Result<Map> Create(IPlacementValidator placementValidator, ushort sizeX, ushort sizeY)
        {
            if (sizeX == 0 || sizeY == 0 || sizeX > 100 || sizeY > 100)
                return Result.Failure<Map>("SizeX and sizeY have to be greater than 0 and smaller than 100");

            return Result.Success(new Map(placementValidator, sizeX, sizeY));
        }

        internal Result AddShip(Ship ship)
        {
            var placementValidationResult = _placementValidator.Validate(this, ship);

            if (placementValidationResult.IsSuccess)
            {
                _ships.Add(ship);

                foreach (var c in ship.Coordinates)
                    _map[c.X, c.Y].IsEmpty = false;

                return Result.Success();
            }

            return Result.Failure(placementValidationResult.Error);
        }

        internal Result<bool> Shoot(Coordinate c)
        {
            if (c.X >= SizeX || c.Y >= SizeY)
                return Result.Failure<bool>("Shoot outside of the map.");

            if (!_map[c.X, c.Y].WasShot)
            {
                _map[c.X, c.Y].WasShot = true;
                return !_map[c.X, c.Y].IsEmpty;
            }

            return false;
        }

        public Result<MapCell> GetCell(ushort x, ushort y) => x < SizeX && y < SizeY ? _map[x, y] : Result.Failure<MapCell>("Request out of map's range.");

        internal bool AreAllShipSunk() =>
            !(from MapCell mapCell in _map where !mapCell.IsEmpty && !mapCell.WasShot select mapCell).Any();
    }
}

