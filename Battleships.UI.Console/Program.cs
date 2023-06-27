using Battleships.UI.Console;
using Battleships.UI.Console.Infrastructure;
using BattleShips.Domain;
using BattleShips.Domain.Plumbing;
using BattleShips.Domain.Ships;
using Castle.MicroKernel.Registration;
using Castle.Windsor;


/*
 * As the requirements are very specific: map size, number of ships each type, the used algorithm is simple.
 * As here density is low - 13% the more sophisticated algorithm is not needed.
 * To adhere YAGNI, i will not implement very sophisticated packing algorithms. Just leave a possibility of so.
 */
//

var gameSettings = new GameSettings();
Console.WriteLine($"Battleship game.");
Console.WriteLine($"Map size: {gameSettings.SizeX}x{gameSettings.SizeY}");
Console.WriteLine($"Number of destroyers: {gameSettings.NumberOfDestroyers}");
Console.WriteLine($"Size of a destroyer: {Destroyer.ShipLength}x1");
Console.WriteLine($"Number of battleships: {gameSettings.NumberOfBattleships}");
Console.WriteLine($"Size of a battleship: {Battleship.ShipLength}x1");

var container = new WindsorContainer().Install(new RepositoriesInstaller());
var gameResult = container.Resolve<IGameFactory>().Create(gameSettings);

if (gameResult.IsFailure)
{
    Console.WriteLine(gameResult.Error);
    return;
}

do
{
    Console.WriteLine(
        $"Please type coordinates like: A5,  where A is the column and 5 is the row, to specify a square to target. A1 is the top left corner.");
    var userCoordinates = Console.ReadLine();

    var coordinateResult = CoordinateParser.Parse(userCoordinates, gameSettings);
    if (coordinateResult.IsFailure)
    {
        Console.WriteLine(coordinateResult.Error);
        continue;
    }

    var shootResult = gameResult.Value.Shoot(coordinateResult.Value);
    if (shootResult.IsFailure)
    {
        Console.WriteLine(shootResult.Error);
        continue;
    }

    if (shootResult.Value)
        Console.WriteLine("You scored a hit!");
    else
        Console.WriteLine("You missed!");

    var displayedMapResult = MapPresenter.Display(gameResult.Value.Map, args.Length == 1 && args[0] == "cheat");
    if (displayedMapResult.IsSuccess)
        Console.WriteLine(displayedMapResult.Value);
    else
        Console.WriteLine(displayedMapResult.Error);

} while (gameResult.Value.Status != GameStatus.Finished);

Console.WriteLine($"Game Over");


