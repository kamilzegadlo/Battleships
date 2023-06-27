using BattleShips.Domain;
using System.Text;
using CSharpFunctionalExtensions;

namespace Battleships.UI.Console.Infrastructure
{
    internal class MapPresenter
    {
        public static Result<string> Display(Map map, bool cheat = false)
        {
            if (map.SizeX > 10 || map.SizeY > 10)
                return Result.Failure<string>("Presenting map algorithm limitation is map's size up to 10x10.");

            var displayedMap = new StringBuilder("Legend: 0 - missed, X - hit");
            if (cheat) displayedMap.AppendLine(", WORKING IN CHEAT MODE: E - empty, S - ship.");
            else displayedMap.AppendLine(", ? - not hit yet");

            AppendColumnNames(displayedMap, map.SizeX);

            for (ushort y = 0; y < map.SizeY; ++y)
            {
                displayedMap.Append(y);

                for (ushort x = 0; x < map.SizeX; ++x)
                {
                    var cell = map.GetCell(x, y);
                    if(cell.IsFailure)
                        return Result.Failure<string>(cell.Error);

                    if (cell.Value.IsEmpty && cell.Value.WasShot)
                        displayedMap.Append('0');
                    else if (!cell.Value.IsEmpty && cell.Value.WasShot)
                        displayedMap.Append('X');
                    else if (cheat)
                    {
                        if (cell.Value.IsEmpty)
                            displayedMap.Append('E');
                        else
                            displayedMap.Append('S');
                    }
                    else
                        displayedMap.Append('?');
                }

                displayedMap.AppendLine();
            }

            return displayedMap.ToString();
        }

        private static void AppendColumnNames(StringBuilder s, ushort sizeX)
        {
            s.Append(' ');
            for (ushort x = 0; x < sizeX; ++x)
                s.Append((char)('A' + x));
            s.AppendLine();
        }
    }
}
