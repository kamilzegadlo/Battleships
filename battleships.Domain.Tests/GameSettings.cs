using BattleShips.Domain;

namespace Battleships.Domain.Tests
{
    internal class GameSettings : IGameSettings
    {
        public ushort SizeX { get; set; }

        public ushort SizeY { get; set; }

        public ushort NumberOfDestroyers { get; set; }

        public ushort NumberOfBattleships { get; set; }
    }
}
