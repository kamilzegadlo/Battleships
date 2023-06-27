using BattleShips.Domain;
using CSharpFunctionalExtensions;

namespace Battleships.UI.Console.Infrastructure
{
    internal class CoordinateParser
    {
        public static Result<Coordinate> Parse(string? coordinates, IGameSettings settings)
        {
            var algorithmLimit = "Algorithm for parsing coordinates is ready only for sizes up to 10x10";

            if (settings.SizeX > 10 || settings.SizeY > 10)
                return Result.Failure<Coordinate>(algorithmLimit);

            if (string.IsNullOrWhiteSpace(coordinates) || coordinates.Length != 2) 
                return Result.Failure<Coordinate>("Wrong coordinates. Please adhere to the following example: A5. A0 is the top left corner.");

            coordinates = coordinates.ToUpper();

            if (!ushort.TryParse(coordinates.AsSpan(1), out ushort y))
                return Result.Failure<Coordinate>("Wrong y coordinate. Please adhere to the following example: A5. A0 is the top left corner.");

            ushort x = (ushort)(coordinates[0] - 'A');

            if (x > 9  || y > 9)
                return Result.Failure<Coordinate>(algorithmLimit);

            return Result.Success(new Coordinate(x, y));
        }
    }
}
