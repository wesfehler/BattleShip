using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipModel
{
    public class BoardShipList : IList<IShip>
    {
        private readonly Board _board;
        private readonly List<IShip> _list = new List<IShip>();

        public BoardShipList(Board board)
        {
            _board = board;
        }

        public IEnumerator<IShip> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IShip item)
        {
            _list.Add(item);
            if (item.Coordinates.Any(boardCoordinate => _board.CoordinateIsMarked(boardCoordinate)))
                throw new ApplicationException("Board coordinate not empty");

            foreach (BoardCoordinate boardCoordinate in item.Coordinates)
            {
                _board.PlaceMarker(boardCoordinate, item.GetHashCode());
                    // unique hash code is used to retrieve the ship object later
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IShip item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(IShip[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(IShip item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly { get; private set; }

        public int IndexOf(IShip item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, IShip item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public IShip this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }
    }
}