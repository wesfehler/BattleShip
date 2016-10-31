using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BattleShipModel
{
    public class BoardCoordinate
    {
        [DebuggerStepThrough]
        public BoardCoordinate(string coordinates)
        {
            // validate
            if (String.IsNullOrWhiteSpace(coordinates)) throw new ArgumentNullException();
            if (!CoordinatesAreValid(coordinates)) throw new ArgumentOutOfRangeException();

            char[] coordinateArray = coordinates.ToCharArray();
            Column = char.ToUpper(coordinateArray[0]) - 64;
            Row = Convert.ToInt32(char.GetNumericValue(coordinateArray[1]));
        }

        [DebuggerStepThrough]
        public BoardCoordinate(int column, int row)
        {
            // validate
            if (column < 1 || column > 8 || row < 1 || row > 8) throw new ArgumentOutOfRangeException();

            Column = column;
            Row = row;
        }

        public int Column { get; private set; }
        public int Row { get; private set; }

        public override int GetHashCode()
        {
            return (Column*10) + Row;
        }

        public override string ToString()
        {
            string row = Row.ToString(CultureInfo.InvariantCulture);
            string column = ((char) (Column + 64)).ToString(CultureInfo.InvariantCulture);
            return String.Concat(column, row);
        }

        private static bool CoordinatesAreValid(string coordinates)
        {
            return (Regex.IsMatch(coordinates, "^[A-Ha-h]{1}[1-8]{1}$"));
        }
    }
}