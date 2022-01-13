using System.Collections.Generic;
using DAE.BoardSystem;
using DAE.HexesSystem.Moves;
using DAE.ReplaySystem;
using UnityEngine;

namespace DAE.GameSystem.Cards
{
    class SlashMove : CardBase
    {
        public SlashMove(ReplayManager replayManager) : base(replayManager)
        {
            ReplayManager = replayManager;
        }

        public override List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position positionBoard)
        {
            var  allPositions = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard)
                .NorthEast(1)
                .East(1)
                .SouthEast(1)
                .SouthWest(1)
                .West(1)
                .NorthWest(1)
                .Collect();

            int index = allPositions.IndexOf(positionBoard);
            if (index != -1)
            {
                return new List<Position>()
                {     
                    allPositions[(index - 1) >= 0 ? index - 1 : allPositions.Count - 1],
                    allPositions[index],
                    allPositions[(index + 1) % allPositions.Count/* <= allPositions.Count - 1 ? index + 1 : 0*/]
                }; 
            }
            Debug.Log(allPositions.Count);
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
        }
    }
}
