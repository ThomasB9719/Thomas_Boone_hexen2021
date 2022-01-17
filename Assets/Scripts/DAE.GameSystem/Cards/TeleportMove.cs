using System;
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

            if (board.TryGetPieceAt(positionBoard, out var toPiece))
                allPositions.Remove(positionBoard);

            return allPositions;
        }

        public override void Execute(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position)
        {
            board.Move(piece, position);
        }
    }
}
