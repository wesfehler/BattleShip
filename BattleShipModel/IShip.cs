namespace BattleShipModel
{
    public interface IShip
    {
        BoardCoordinate StartCoordinate { get; }
        BoardCoordinate EndCoordinate { get; }
        BoardCoordinate[] Coordinates { get; }
        int Length { get; }
        void Hit(BoardCoordinate coordinate);
        bool Sunk();
    }
}