namespace BattleShips.Domain.Ships
{
    public class Destroyer : Ship
    {
        public static ushort ShipLength => 4;
        internal override ushort Length => ShipLength;

        public Destroyer(Coordinate shipBowCoordinate, ShipOrientation orientation) : base(shipBowCoordinate, orientation)
        {
        }
    }
}
