using System.Collections.Generic;
using DAE.BoardSystem;
using DAE.GameSystem;
using DAE.HexesSystem.Moves;
using DAE.ReplaySystem;
using UnityEngine;

namespace DAE.GameSystem.Cards
{
    class StrikeMove : CardBase
    {
        public StrikeMove(ReplayManager replayManager) : base(replayManager)
        {
            ReplayManager = replayManager;
        }

        public override List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position positionBoard)
        {
            var allPositions = new List<Position>();
            foreach (var direction in MovementHelper<Position, Piece>.Directions)
            {
                var list = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).Move(direction.x, direction.y).Collect();
                if (list.Contains(positionBoard))
                {
                    return list;
                }

                allPositions.AddRange(list);
            }
            return allPositions;
        }

        public override void Execute(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position)
        {
            List<Position> positions = Positions(board, grid, piece, position);

            foreach (Position availablePosition in positions)
            {
                if (board.TryGetPieceAt(availablePosition, out var toPiece))
                    if (toPiece.PieceType == HexesSystem.PieceType.Player)
                    {
                        return;
                    }
                    else
                    {
                        board.Take(toPiece);
                    }
            }
        }
    }
}
