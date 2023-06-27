namespace BattleShips.Domain.Ships
{
    public abstract class Ship
    {
        internal Coordinate ShipBowCoordinate { get; }
        internal abstract ushort Length { get; }
        internal ShipOrientation Orientation { get; }
        internal IEnumerable<Coordinate> Coordinates { get; }

        protected Ship(Coordinate shipBowCoordinate, ShipOrientation orientation)
        {
            ShipBowCoordinate = shipBowCoordinate;
            Orientation = orientation;
            Coordinates = GetCoordinates();
        }

        private IEnumerable<Coordinate> GetCoordinates()
        {
            var coordinates = new List<Coordinate>(Length);

            for (int i = 0; i < Length; ++i)
                coordinates.Add(Orientation == ShipOrientation.Horizontally
                    ? new Coordinate((ushort)(ShipBowCoordinate.X + i), ShipBowCoordinate.Y)
                    : new Coordinate(ShipBowCoordinate.X, (ushort)(ShipBowCoordinate.Y + i)));

            return coordinates;
        }
    }
}
