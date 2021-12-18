using DAE.BoardSystem;
//using DAE.ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexesSystem.Moves
{
    class PawnDoubleMove<TPosition, TPiece> : MoveBase<TPosition, TPiece>
        where TPiece : IPiece
    {
        public PawnDoubleMove(/*ReplayManager replayManager*/) /*:base(replayManager)*/
        {

        }

        public override bool CanExecute(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece)
        {
            if (piece.Moved)
                return false;

            if (!board.TryGetPositionOf(piece, out var position))
                return false;

            if (!grid.TryGetCoordinateOf(position, out var coordinate))
                return false;

            int yPlus1 = coordinate.y /*+ ((piece.PlayerID == 0) ? 1 : -1)*/;
            if (!IsEmpty(board, grid, piece, coordinate.x, yPlus1))
                return false;

            int yPlus2 = coordinate.y /*+ ((piece.PlayerID == 0) ? 2 : -2)*/;
            if (!IsEmpty(board, grid, piece, coordinate.x, yPlus2))
                return false;

            if (!IsOnPawnStartingRow(piece, coordinate))
                return false;

            return base.CanExecute(board, grid, piece);
        }

       

        public override List<TPosition> Positions(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece)
        {
            if (!board.TryGetPositionOf(piece, out var position))
                return new List<TPosition>(0);

            if (!grid.TryGetCoordinateOf(position, out var coordinate))
                return new List<TPosition>(0);

            //coordinate.y += (piece.PlayerID == 0) ? 2 : -2;

            if (grid.TryGetPositionAt(coordinate.x, coordinate.y, out var newPosition))
                return new List<TPosition>() { newPosition };
            else
                return new List<TPosition>(0);

        }

        private bool IsEmpty(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, int x, int y)
        {
            if (!grid.TryGetPositionAt(x, y, out var position))
                return false;

            if (board.TryGetPieceAt(position, out _))
                return false;

            return true;
        }

        private bool IsOnPawnStartingRow(TPiece piece, (int x, int y) coordinate)
            => (/*piece.PlayerID == 0 &&*/ coordinate.y == 1) || (/*piece.PlayerID == 1 &&*/ coordinate.y == 6);
        
    }
}
