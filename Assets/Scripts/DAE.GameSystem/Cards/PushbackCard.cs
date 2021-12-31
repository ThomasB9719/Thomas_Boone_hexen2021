using System.Collections.Generic;
using DAE.BoardSystem;
using DAE.HexesSystem.Moves;
using UnityEngine;
using System.Linq;
using System;

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

        //public List<Position> SelectedPositions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position positionBoard)
        //{
        //    //var selectedPositions = new List<Position>();

        //    ////foreach (var direction in MovementHelper<Position, Piece>.Directions)
        //    ////{
        //    //var list = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).NorthEast(1)
        //    //    .NorthWest(1)
        //    //    .West(1)
        //    //    .East(1)
        //    //    .SouthEast(1)
        //    //    .SouthWest(1)
        //    //    .Collect();
        //    //if (list.Contains(positionBoard))
        //    //{
        //    //    int positionList = list.IndexOf(positionBoard);
        //    //    int positionListNext = positionList + 1;
        //    //    int positionListPrevious = positionList - 1;

        //    //    Position previousPosition = list.ElementAt(positionListPrevious);
        //    //    Position nextPosition = list.ElementAt(positionListNext);
        //    //    list.Add(previousPosition);
        //    //    list.Add(nextPosition);
        //    //    return list;
        //    //}
        //    ////selectedPositions.AddRange(list);
        //    ////selectedPositions.Add(positionBoard);
        //    //selectedPositions.AddRange(list);

        //    //return selectedPositions;

        //    //var allPositions = new List<Position>();
        //    //Vector2Int[] directions = MovementHelper<Position, Piece>.Directions;
        //    //foreach (var direction in /*MovementHelper<Position, Piece>.Directions*/ directions)
        //    //{
        //    //    int positionArray = Array.IndexOf(directions, direction);
        //    //    int positionArrayNext = positionArray + 1;
        //    //    int positionArrayPrevious = positionArray - 1;

        //    //    Vector2Int nextDirection = directions.ElementAt(positionArrayNext);
        //    //    Vector2Int previousDirection = directions.ElementAt(positionArrayPrevious);

        //    //    var listPrevious = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).Move(previousDirection.x, previousDirection.y).Collect();
        //    //    var listNext = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).Move(nextDirection.x, nextDirection.y).Collect();               

        //    //    var list = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).Move(direction.x, direction.y).Collect();
        //    //    if (list.Contains(positionBoard))
        //    //    {

        //    //        return list;
        //    //    }

        //    //    allPositions.AddRange(list);
        //    //    allPositions.AddRange(listNext);
        //    //    allPositions.AddRange(listPrevious);
        //    //}
        //    //return allPositions;
        //}

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

            //if (!grid.TryGetPositionAt(coordinate.x, coordinate.y, out var playerPosition))
            //{
            //    Debug.Log("No playerposition");
            //}
            //return new List<Position>() { newPosition };
            //else
            //    return new List<Position>(0);



            List<Position> positions = Positions(board, grid, piece, position);
            //List<Position> selectedPositions = SelectedPositions(board, grid, piece, position);

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

                    //int distanceX = playerPosition.x + playerPosition.x - enemyPosition.x;
                    //int distanceY = playerPosition.y + playerPosition.y - enemyPosition.y;

                    //int distanceX = enemyPosition.x + (playerPosition.x + enemyPosition.x);
                    //int distanceY = enemyPosition.y + (playerPosition.y + enemyPosition.y);

                    //int distanceX = playerPosition.x + (playerPosition.x + enemyPosition.x);
                    //int distanceY = playerPosition.y + (playerPosition.y + enemyPosition.y);

                    //Debug.Log($"{piece}: + {playerPosition}");
                    //Debug.Log($"{toPiece} + {enemyPosition}");

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
