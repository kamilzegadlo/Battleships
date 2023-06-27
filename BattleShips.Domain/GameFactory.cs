using CSharpFunctionalExtensions;

namespace BattleShips.Domain
{
    public interface IGameFactory
    {
        Result<Game> Create(IGameSettings settings);
    }

    internal class GameFactory : IGameFactory
    {
        private readonly IPlacementAllocator _placementAllocator;
        private readonly IPlacementValidator _placementValidator;

        public GameFactory(IPlacementAllocator placementAllocator, IPlacementValidator placementValidator)
        {
            _placementAllocator = placementAllocator;
            _placementValidator= placementValidator;
        }

        public Result<Game> Create(IGameSettings settings) => Game.Create(_placementAllocator, _placementValidator, settings);
    }
}
