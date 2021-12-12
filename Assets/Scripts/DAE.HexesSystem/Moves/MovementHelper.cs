using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DAE.BoardSystem;

namespace DAE.HexesSystem.Moves
{
    class MovementHelper<TPiece>
        where TPiece : IPiece
    {
        private Board<Position, TPiece> _board;
        private Grid<Position> _grid;
        private TPiece _piece;

        private List<Position> _validPositions = new List<Position>();

        public MovementHelper(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece)
        {
            _board = board;
            _grid = grid;
            _piece = piece;
        }

        public MovementHelper<TPiece> North(int numTiles = int.MaxValue, params Validator[] validators)
            => Move(0, 1, numTiles, validators);


        public MovementHelper<TPiece> NorthEast(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(1, 1, numTiles, validators);
        }

        public MovementHelper<TPiece> East(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(1, 0, numTiles, validators);
        }

        public MovementHelper<TPiece> SouthEast(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(1, -1, numTiles, validators);
        }

        public MovementHelper<TPiece> South(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(0, -1, numTiles, validators);
        }

        public MovementHelper<TPiece> SouthWest(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(-1, -1, numTiles, validators);
        }

        public MovementHelper<TPiece> West(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(-1, 0, numTiles, validators);
        }

        public MovementHelper<TPiece> NorthWest(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(-1, 1, numTiles, validators);
        }

        public MovementHelper<TPiece> CaptureNorthEast()
        {
            return this;
        }

        public MovementHelper<TPiece> CaptureNorthWest()
        {
            return this;
        }

        public delegate bool Validator(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position);

        public MovementHelper<TPiece> Move(int xOffset, int yOffset, int numTiles = int.MaxValue, params Validator[] validators)
        {
            //if (_piece.PlayerID == 1) //black
            //{
            //    xOffset *= -1;
            //    yOffset *= -1;
            //}

            if (!_board.TryGetPositionOf(_piece, out var position))
                return this;
            if (!_grid.TryGetCoordinateOf(position, out var coordinate))
                return this;

            var nextXCoordinate = coordinate.x + xOffset;
            var nextYCoordinate = coordinate.y + yOffset;

            var hasNextPosition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out var nextPosition);

            int step = 0;

            while (hasNextPosition && step < numTiles)
            {
                var isOk = validators.All((v) => v(_board, _grid, _piece, nextPosition));
                if (!isOk)
                    return this;

                //does the same as the above, but in more steps
                //bool isOk = true;
                //foreach (var validator in validators)
                //{
                //     isOk &= validator(_board, _piece, nextTile);
                //}
                //if (!isOk)
                //    return this;



                var hasPiece = _board.TryGetPieceAt(nextPosition, out var nextPiece);
                if (!hasPiece)
                {
                    _validPositions.Add(nextPosition);
                }
                else
                {
                    //if (nextPiece.PlayerID == _piece.PlayerID)
                    //{
                    //    return this;
                    //}

                    _validPositions.Add(nextPosition);
                    return this;
                }

                nextXCoordinate = coordinate.x + xOffset;
                nextYCoordinate = coordinate.y + yOffset;

                hasNextPosition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out nextPosition);

                step++;
            }
            return this;
        }

        public List<Position> Collect()
        {
            return _validPositions;
        }

        public static bool IsEmptyTile(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position)
            => !board.TryGetPieceAt(position, out _);

        public static bool HasEnemyPiece(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position)
            => board.TryGetPieceAt(position, out var enemyPiece) /*&& enemyPiece.PlayerID != piece.PlayerID*/;

    }
}