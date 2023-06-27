namespace BattleShips.Domain
{
    public interface IGameSettings
    {
        ushort SizeX { get; }
        ushort SizeY { get; }
        ushort NumberOfDestroyers { get; }
        ushort NumberOfBattleships { get; }
    }
}
