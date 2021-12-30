using System.Collections.Generic;
using DAE.BoardSystem;
using DAE.HexesSystem.Moves;
using UnityEngine;

namespace DAE.GameSystem.Cards
{
    class PushbackCard : CardBase
    {
        public override List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position positionBoard)
        {
            //if (!board.TryGetPositionOf(piece, out var position))
            //    return new List<Position>(0);

            //if (!grid.TryGetCoordinateOf(position, out var coordinate))
            //    return new List<Position>(0);

            //if (grid.TryGetPositionAt(coordinate.x, coordinate.y, out var newPosition))
            //    return new List<Position>() { newPosition };
            //else
            //    return new List<Position>(0);

            //var allPositions = new List<Position>();
            //foreach (var direction in MovementHelper<Position, Piece>.Directions)
            //{
            //    var list = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).Move(direction.x, direction.y).Collect();
            //    if (list.Contains(positionBoard))
            //    {
            //        //if (board.TryGetPieceAt(positionBoard, out var toPiece))
            //        //    board.Take(toPiece);
            //        return list;
            //    }

            //    allPositions.AddRange(list);
            //}
            //return allPositions;

            var allPositions = new List<Position>();

            //foreach (var direction in MovementHelper<Position, Piece>.Directions)
            //{
            var list = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).NorthEast(1)
                .NorthWest(1)
                .West(1)
                .East(1)
                .SouthEast(1)
                .SouthWest(1)
                .Collect();
            if (list.Contains(positionBoard))
            {
                //if (board.TryGetPieceAt(positionBoard, out var toPiece))
                //    board.Take(toPiece);
                return list;
            }
            allPositions.AddRange(list);
            //}
            return allPositions;
        }

        public override void Execute(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position)
        {
            if (!board.TryGetPositionOf(piece, out var positionPlayer))
            {
                Debug.Log("No position");
            }

            if (!grid.TryGetCoordinateOf(position, out var coordinate))
            {
                Debug.Log("No coordinates");
            }

            (int x, int y) playerPosition = (coordinate.x, coordinate.y);

            //if (!grid.TryGetPositionAt(coordinate.x, coordinate.y, out var playerPosition))
            //{
            //    Debug.Log("No playerposition");
            //}
                //return new List<Position>() { newPosition };
            //else
            //    return new List<Position>(0);



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

                    int distanceX = playerPosition.x + playerPosition.x - enemyPosition.x;
                    int distanceY = playerPosition.y + playerPosition.y - enemyPosition.y;

                    if (!grid.TryGetPositionAt(distanceX, distanceY, out Position distancePosition))
                    {
                        Debug.Log("No distancePosition");
                    }


                    board.Move(toPiece, distancePosition);
                    //if (!grid.TryGetPositionAt(coordinateEnemy.x, coordinateEnemy.y, out var enemyPosition))
                    //{
                    //    Debug.Log("No playerposition");
                    //}
                }
                    //board.Take(toPiece);
                    
            }

            //board.Move(piece, position);
        }


    }
}
