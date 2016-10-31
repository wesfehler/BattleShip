namespace BattleShipModel
{
    public class Cruiser : Ship
    {
        public Cruiser(BoardCoordinate start, BoardCoordinate end) : base(start, end)
        {
        }

        public override int Length
        {
            get { return Constants.ShipLengths.Cruiser; }
        }

        public override bool Is(ShipType shipType)
        {
            return shipType == ShipType.Cruiser;
        }
    }
}