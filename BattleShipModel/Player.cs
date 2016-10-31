namespace BattleShipModel
{
    public class Player
    {
        public Board Board;
        public Board OpponentBoard;

        public Player(string name)
        {
            Name = name;
            Board = new Board();
            OpponentBoard = new Board();
        }

        public string Name { get; set; }
    }
}