using System.Collections.Generic;
using DAE.BoardSystem;
using DAE.HexesSystem.Moves;
using UnityEngine;
using System.Linq;
using System;
using DAE.ReplaySystem;

namespace DAE.GameSystem.Cards
{
    class PushbackCard : CardBase
    {
        public PushbackCard(ReplayManager replayManager) : base(replayManager)
        {

        }

        public override List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position positionBoard)
        {
            var allPositions = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard)
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
                    allPositions[(index - 1) >= 0 ? index - 1 : allPositions.Count - 1  ],
                    allPositions[index],
                    allPositions[(index + 1) % allPositions.Count]
                };
            }
            return allPositions;
        }

        public override void Execute(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position)
        {
            if (!board.TryGetPositionOf(piece, out var positionPlayer))
            {
                Debug.Log("No position");
            }

            if (!grid.TryGetCoordinateOf(positionPlayer, out var coordinate))
            {
                Debug.Log("No coordinates");
            }

            (int x, int y) playerPosition = (coordinate.x, coordinate.y);

            List<Position> positions = Positions(board, grid, piece, position);

            foreach (Position availablePosition in positions)
            {
                if (board.TryGetPieceAt(availablePosition, out var toPiece))
                {
                    if (!board.TryGetPositionOf(toPiece, out var positionEnemy))
                    {
                        Debug.Log("No position");
                    }

                    if (!grid.TryGetCoordinateOf(positionEnemy, out var coordinateEnemy))
                    {
                        Debug.Log("No coordinates");
                    }

                    (int x, int y) enemyPosition = (coordinateEnemy.x, coordinateEnemy.y);

                    int newPositionX = enemyPosition.x + (enemyPosition.x - playerPosition.x);
                    int newPositionY = enemyPosition.y + (enemyPosition.y - playerPosition.y);

                    if (!grid.TryGetPositionAt(newPositionX, newPositionY, out Position newPosition))
                    {
                        Debug.Log("No distancePosition");
                    }

                    board.Move(toPiece, newPosition);
                }
            }
        }
    }
}
