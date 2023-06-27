using BattleShips.Domain;

namespace Battleships.UI.Console
{
    internal class GameSettings : IGameSettings
    {
        public ushort SizeX => 10;

        public ushort SizeY => 10;

        public ushort NumberOfDestroyers => 2;

        public ushort NumberOfBattleships => 1;
    }
}
