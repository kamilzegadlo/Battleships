using Battleships.UI.Console.Infrastructure;
using BattleShips.Domain;
using Xunit;

namespace Battleships.Infrastructure.Tests
{
    public class CoordinateParserTests
    {
        [Fact]
        public void Map_Size_Not_Supported()
        {
            Assert.True(CoordinateParser.Parse("A5",
                new UnitTestGameSettings { SizeX = 11, SizeY = 8, NumberOfDestroyers = 2, NumberOfBattleships = 1 }).IsFailure);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("A")]
        [InlineData("AA")]
        [InlineData("5")]
        [InlineData("55")]
        [InlineData("AA5")]
        [InlineData("A55")]
        [InlineData("Z5")]
        [InlineData("-5")]
        [InlineData("<5")]
        [InlineData("A-")]
        [InlineData("A<")]
        public void Incorrect_Input(string? input)
        {
            Assert.True(CoordinateParser.Parse(input,
                new UnitTestGameSettings { SizeX = 10, SizeY = 10, NumberOfDestroyers = 2, NumberOfBattleships = 1 }).IsFailure);
        }

        [Fact]
        public void Happy_Case()
        {
            var parseResult = CoordinateParser.Parse("A5",
                new UnitTestGameSettings {SizeX = 10, SizeY = 10, NumberOfDestroyers = 2, NumberOfBattleships = 1});
            Assert.True(parseResult.IsSuccess);
            Assert.Equal(new Coordinate(0, 5), parseResult.Value);
        }

        [Fact]
        public void Should_Be_No_Case_Sensitive()
        {
            var parseResult = CoordinateParser.Parse("a5",
                new UnitTestGameSettings { SizeX = 10, SizeY = 10, NumberOfDestroyers = 2, NumberOfBattleships = 1 });
            Assert.True(parseResult.IsSuccess);
            Assert.Equal(new Coordinate(0, 5), parseResult.Value);
        }

        class UnitTestGameSettings : IGameSettings
        {
            public ushort SizeX { get; set; }
            public ushort SizeY { get; set; }
            public ushort NumberOfDestroyers { get; set; }
            public ushort NumberOfBattleships { get; set; }
        }
    }
}
