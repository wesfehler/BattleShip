using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BattleShipModel
{
    public class Board
    {
        private readonly Hashtable markers;

        public Board()
        {
            markers = new Hashtable(64);
            Ships = new BoardShipList(this);
        }

        public BoardShipList Ships { get; set; }

        public bool CoordinateIsMarked(string coordinate)
        {
            return CoordinateIsMarked(new BoardCoordinate(coordinate));
        }

        public bool CoordinateIsMarked(BoardCoordinate coordinate)
        {
            return markers.Contains(coordinate.GetHashCode());
        }

        public MarkerType GetMarkerFromCoordinate(BoardCoordinate coordinate)
        {
            Object m = markers[coordinate.GetHashCode()];
            if (m == null) throw new ArgumentNullException("coordinate");

            if (m is MarkerType)
                return (MarkerType) m;

            if (m is int)
                return MarkerType.Ship;

            return MarkerType.Unknown;
        }


        public void PlaceMarker(BoardCoordinate coordinate, int hashCode)
        {
            markers[coordinate.GetHashCode()] = hashCode;
        }

        public void PlaceMarker(BoardCoordinate coordinate, MarkerType marker)
        {
            if (marker == MarkerType.Hit)
            {
                // get the ship from the board coordinates and update its hit count
                // if this is the Opponent Tracking board, there will be no ship to update.
                if (markers.Contains(coordinate.GetHashCode()))
                {
                    var hashCode = (int) markers[coordinate.GetHashCode()];
                    IShip hitShip = Ships.First(ship => ship.GetHashCode() == hashCode);
                    if (hitShip != null)
                    {
                        hitShip.Hit(coordinate);
                        // if ship is sunk, remove it from ShipList
                        if (hitShip.Sunk()) Ships.Remove(hitShip);
                    }
                }
            }

            // place marker on the board
            markers[coordinate.GetHashCode()] = marker;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("  A B C D E F G H");

            for (int row = 1; row <= 8; row++)
            {
                sb.Append(row.ToString(CultureInfo.InvariantCulture));
                sb.Append(" ");
                for (int col = 1; col <= 8; col++)
                {
                    int h = new BoardCoordinate(col, row).GetHashCode();
                    if (markers.Contains(h))
                    {
                        object o = markers[h];
                        if (o.GetType() == typeof (MarkerType))
                        {
                            var mt = (MarkerType) o;
                            if (mt == MarkerType.Hit)
                            {
                                sb.Append("H ");
                            }
                            else if (mt == MarkerType.Miss)
                            {
                                sb.Append("X ");
                            }
                            else
                            {
                                sb.Append("- ");
                            }
                        }
                        else
                        {
                            sb.Append("S ");
                        }
                    }
                    else
                    {
                        sb.Append("- ");
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}