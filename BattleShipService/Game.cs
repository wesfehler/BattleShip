using System;
using BattleShipModel;

namespace BattleShipService
{
    public class Game
    {
        private readonly ShipFactory _shipFactory;

        public Game()
        {
            _shipFactory = new ShipFactory();
        }

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Player CurrentPlayer { get; set; }

        public Player OpponentPlayer
        {
            get
            {
                if (CurrentPlayer == null) return null;
                return CurrentPlayer == PlayerOne ? PlayerTwo : PlayerOne;
            }
        }

        public Player Winner
        {
            get
            {
                if (PlayerOne.Board.Ships.Count == 0) return PlayerTwo;
                if (PlayerTwo.Board.Ships.Count == 0) return PlayerOne;
                return null;
            }
        }

        public bool Over
        {
            get { return Winner != null; }
        }

        public void Start()
        {
            if (PlayerOne == null || PlayerTwo == null) throw new ApplicationException();
            if (PlayerOne.Board.Ships.Count == 0 || PlayerTwo.Board.Ships.Count == 0) throw new ApplicationException();
            CurrentPlayer = PlayerOne;
        }

        public void PlaceShip(Player player, ShipType shipType, string startCoordinates, string endCoordinates)
        {
            Ship ship = _shipFactory.Create(shipType, startCoordinates, endCoordinates);
            player.Board.Ships.Add(ship);
        }

        public MarkerType Call(string coordinate)
        {
            return Call(new BoardCoordinate(coordinate));
        }

        public MarkerType Call(BoardCoordinate coordinate)
        {
            // check current players board if already called
            if (CurrentPlayer.OpponentBoard.CoordinateIsMarked(coordinate))
            {
                MarkerType markerFromCoordinate = CurrentPlayer.OpponentBoard.GetMarkerFromCoordinate(coordinate);
                EndTurn();
                return markerFromCoordinate;
            }

            // check for a hit
            if (OpponentPlayer.Board.CoordinateIsMarked(coordinate))
            {
                OpponentPlayer.Board.PlaceMarker(coordinate, MarkerType.Hit);
                CurrentPlayer.OpponentBoard.PlaceMarker(coordinate, MarkerType.Hit);
                EndTurn();
                return MarkerType.Hit;
            }

            OpponentPlayer.Board.PlaceMarker(coordinate, MarkerType.Miss);
            CurrentPlayer.OpponentBoard.PlaceMarker(coordinate, MarkerType.Miss);

            EndTurn();
            return MarkerType.Miss;
        }

        private void EndTurn()
        {
            CurrentPlayer = CurrentPlayer == PlayerOne ? PlayerTwo : PlayerOne;
        }
    }
}