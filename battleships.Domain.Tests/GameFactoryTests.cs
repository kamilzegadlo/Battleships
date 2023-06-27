using BattleShips.Domain;
using CSharpFunctionalExtensions;
using FakeItEasy;
using Xunit;

namespace Battleships.Domain.Tests
{
    public class GameFactoryTests
    {
        private readonly IPlacementAllocator _placementAllocatorFake;
        private readonly IPlacementValidator _placementValidatorFake;

        public GameFactoryTests()
        {
            _placementAllocatorFake = A.Fake<IPlacementAllocator>();
            _placementValidatorFake = A.Fake<IPlacementValidator>();
        }

        [Fact]
        public void Should_Init_Game()
        {
            var settings = new GameSettings {NumberOfDestroyers = 1, NumberOfBattleships = 2, SizeX = 3, SizeY = 4};
            var gameResult = new GameFactory(_placementAllocatorFake, _placementValidatorFake).Create(settings);

            Assert.True(gameResult.IsSuccess);

            Assert.Equal(3, gameResult.Value.Map.SizeX);
            Assert.Equal(4, gameResult.Value.Map.SizeY);
            Assert.Equal(GameStatus.Started, gameResult.Value.Status);
            A.CallTo(() =>
                _placementAllocatorFake.AllocateShips(gameResult.Value.Map, settings.NumberOfDestroyers,
                    settings.NumberOfBattleships)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void PlacementAllocator_Returns_Failure()
        {
            var settings = new GameSettings { NumberOfDestroyers = 1, NumberOfBattleships = 2, SizeX = 3, SizeY = 4 };

            A.CallTo(() => _placementAllocatorFake.AllocateShips(A<Map>._, settings.NumberOfDestroyers,
                settings.NumberOfBattleships)).Returns(Result.Failure("unit test failure"));

            var gameResult = new GameFactory(_placementAllocatorFake, _placementValidatorFake).Create(settings);

            Assert.True(gameResult.IsFailure);
        }
    }
}
