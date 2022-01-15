using System;
using System.Collections.Generic;
using System.Linq;
using DAE.BoardSystem;
using DAE.HexesSystem.Moves;
using DAE.ReplaySystem;

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
            //board.TryGetPositionOf(piece, out var fromPosition);

            board.Move(piece, position);

            //Action forward = () =>
            //{
            //    board.Move(piece, position);
            //};

            //Action backward = () =>
            //{
            //    board.Move(piece, fromPosition);
            //};

            //ReplayManager.Execute(new DelegateReplayCommand(forward, backward));
        }
    }
}
