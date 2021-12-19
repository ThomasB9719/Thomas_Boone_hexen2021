using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAE.BoardSystem;
using DAE.HexesSystem;

namespace DAE.HexesSystem.Moves
{
    class TeleportCard<TPosition, TPiece> : MoveBase<TPosition, TPiece> where TPiece : IPiece
    {
        public TeleportCard()
        {

        }
        
        public override List<TPosition> Positions(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece)
        {
            if (!board.TryGetPositionOf(piece, out var position))
                return new List<TPosition>(0);

            if (!grid.TryGetCoordinateOf(position, out var coordinate))
                return new List<TPosition>(0);

            if (grid.TryGetPositionAt(coordinate.x, coordinate.y, out var newPosition))
                return new List<TPosition>() { newPosition };
            else
                return new List<TPosition>(0);
        }

        public override bool CanExecute(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece)
        {
            return base.CanExecute(board, grid, piece);
        }
    }
}
