using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DAE.BoardSystem;

namespace DAE.HexesSystem.Moves
{
    class MovementHelper<TPosition,TPiece>
        where TPiece : IPiece
    {
        private Board<TPosition, TPiece> _board;
        private Grid<TPosition> _grid;
        private TPiece _piece;

        private List<TPosition> _validPositions = new List<TPosition>();

        public MovementHelper(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece)
        {
            _board = board;
            _grid = grid;
            _piece = piece;
        }

        //public MovementHelper<TPosition,TPiece> North(int numTiles = int.MaxValue, params Validator[] validators)
        //    => Move(0, 1, numTiles, validators);


        public MovementHelper<TPosition, TPiece> NorthEast(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(1, -1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> East(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(1, 0, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> SouthEast(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(0, 1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> South(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(0, -1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> SouthWest(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(-1, 1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> West(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(-1, 0, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> NorthWest(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(0, -1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> CaptureNorthEast()
        {
            return this;
        }

        public MovementHelper<TPosition, TPiece> CaptureNorthWest()
        {
            return this;
        }

        public delegate bool Validator(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position);

        public MovementHelper<TPosition,TPiece> Move(int xOffset, int yOffset, int numTiles = int.MaxValue, params Validator[] validators)
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

                nextXCoordinate += coordinate.x + xOffset;
                nextYCoordinate += coordinate.y + yOffset;

                hasNextPosition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out nextPosition);

                step++;
            }
            return this;
        }

        public List<TPosition> Collect()
        {
            return _validPositions;
        }

        public static bool IsEmptyTile(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position)
            => !board.TryGetPieceAt(position, out _);

        public static bool HasEnemyPiece(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position)
            => board.TryGetPieceAt(position, out var enemyPiece) /*&& enemyPiece.PlayerID != piece.PlayerID*/;

    }
}