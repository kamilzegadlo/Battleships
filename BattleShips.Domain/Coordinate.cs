namespace BattleShips.Domain
{
    public  class Coordinate
    {
        public ushort X { get; }
        public ushort Y { get; }

        public Coordinate(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinate? other)
        {
            if (other is null)
                return false;

            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object? obj) => Equals(obj as Coordinate);
        public override int GetHashCode() => (X, Y).GetHashCode();
    }
}
