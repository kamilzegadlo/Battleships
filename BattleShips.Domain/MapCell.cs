namespace BattleShips.Domain
{
    public class MapCell
    {
        public bool IsEmpty { get; set; }
        public bool WasShot { get; set; }

        public MapCell()
        {
            IsEmpty = true;
            WasShot = false;
        }
    }
}
