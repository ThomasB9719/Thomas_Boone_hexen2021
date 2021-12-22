using System.Collections.Generic;
using DAE.BoardSystem;

namespace DAE.GameSystem.Cards
{
    class PushbackCard : CardBase
    {

        public override List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position positionBoard)
        {
            if (!board.TryGetPositionOf(piece, out var position))
                return new List<Position>(0);

            if (!grid.TryGetCoordinateOf(position, out var coordinate))
                return new List<Position>(0);

            if (grid.TryGetPositionAt(coordinate.x, coordinate.y, out var newPosition))
                return new List<Position>() { newPosition };
            else
                return new List<Position>(0);
        }


    }
}
