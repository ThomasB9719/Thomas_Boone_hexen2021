using System.Collections.Generic;
using System.Linq;
using DAE.BoardSystem;
using DAE.HexesSystem.Moves;

namespace DAE.GameSystem.Cards
{
    class TeleportMove : CardBase
    {
        public override List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position positionBoard)
        {
            List<Position> allPositions = new List<Position>();
            allPositions.Add(positionBoard);

            return allPositions;
        }

        public override void Execute(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position)
        {
            List<Position> positions = Positions(board, grid, piece, position);

            foreach (Position availablePosition in positions)
            {
                if (board.TryGetPieceAt(availablePosition, out var toPiece))
                    board.Take(toPiece);
            }

            board.Move(piece, position);
        }
    }
}
