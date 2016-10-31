using System;

namespace BattleShipModel
{
    public abstract class Ship : IShip
    {
        private readonly BoardCoordinate _endCoordinate;
        private readonly BoardCoordinate _startCoordinate;
        private BoardCoordinate[] _coordinates;
        private int _hitCount;

        protected Ship(BoardCoordinate start, BoardCoordinate end)
        {
            if (!CoordinatesAreValid(start, end)) throw new ArgumentOutOfRangeException();
            _startCoordinate = start;
            _endCoordinate = end;
            CalculateCoordinates();
        }

        public BoardCoordinate StartCoordinate
        {
            get { return _startCoordinate; }
        }

        public BoardCoordinate EndCoordinate
        {
            get { return _endCoordinate; }
        }

        public BoardCoordinate[] Coordinates
        {
            get { return _coordinates; }
        }

        public void Hit(BoardCoordinate coordinate)
        {
            _hitCount++;
        }

        public bool Sunk()
        {
            return _hitCount == Length;
        }

        public abstract int Length { get; }

        public override int GetHashCode()
        {
            return StartCoordinate.GetHashCode(); // start position is sufficient for uniqueness; 
        }

        public abstract bool Is(ShipType shipType);

        private void CalculateCoordinates()
        {
            int minRow = StartCoordinate.Row > EndCoordinate.Row ? EndCoordinate.Row : StartCoordinate.Row;
            int maxRow = StartCoordinate.Row > EndCoordinate.Row ? StartCoordinate.Row : EndCoordinate.Row;
            int minCol = StartCoordinate.Column > EndCoordinate.Column ? EndCoordinate.Column : StartCoordinate.Column;
            int maxCol = StartCoordinate.Column > EndCoordinate.Column ? StartCoordinate.Column : EndCoordinate.Column;

            if (minRow == maxRow)
            {
                _coordinates = new BoardCoordinate[maxCol - minCol + 1];
                for (int i = 0; i < _coordinates.Length; i++)
                {
                    _coordinates[i] = new BoardCoordinate(minCol + i, minRow);
                }
            }
            else
            {
                _coordinates = new BoardCoordinate[maxRow - minRow + 1];
                for (int i = 0; i < _coordinates.Length; i++)
                {
                    _coordinates[i] = new BoardCoordinate(minCol, minRow + i);
                }
            }
        }

        private bool CoordinatesAreValid(BoardCoordinate start, BoardCoordinate end)
        {
            // must be on the either the same row or the same column
            if ((start.Row != end.Row) && (start.Column != end.Column)) return false;

            // must be correct length,  horizontally or vertically
            if ((start.Row == end.Row) && Math.Abs(end.Column - start.Column) != (Length - 1)) return false;
            if ((start.Column == end.Column) && Math.Abs(end.Row - start.Row) != (Length - 1)) return false;


            return true;
        }
    }
}