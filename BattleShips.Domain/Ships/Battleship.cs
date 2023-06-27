namespace BattleShips.Domain.Ships
{
    public class Battleship : Ship
    {
        public static ushort ShipLength => 5;
        internal override ushort Length => ShipLength;

        public Battleship(Coordinate shipBowCoordinate, ShipOrientation orientation) : base(shipBowCoordinate, orientation)
        {
        }
    }
}