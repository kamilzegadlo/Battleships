using CSharpFunctionalExtensions;

namespace BattleShips.Domain
{
    public class Game
    {
        public Map Map { get; private set; }
        public GameStatus Status { get; private set; }

        private Game(Map map)
        {
            Map = map;
        }

        internal static  Result<Game> Create(IPlacementAllocator placementAllocator, IPlacementValidator placementValidator,
            IGameSettings settings)
        {
            var mapResult = Map.Create(placementValidator, settings.SizeX, settings.SizeY);
            if (mapResult.IsFailure)
                return Result.Failure<Game>(mapResult.Error);

            var game = new Game(mapResult.Value);

            var placementResult = placementAllocator.AllocateShips(game.Map, settings.NumberOfDestroyers, settings.NumberOfBattleships);
            if (placementResult.IsFailure)
                return Result.Failure<Game>(placementResult.Error);

            game.Status = GameStatus.Started;

            return game;
        }

        public Result<bool> Shoot(Coordinate coordinate)
        {
            if (Status == GameStatus.Finished)
                return Result.Failure<bool>("Game already finished;");

            var shootResult = Map.Shoot(coordinate);
            if (Map.AreAllShipSunk())
                Status = GameStatus.Finished;

            return shootResult;
        }
    }
}
