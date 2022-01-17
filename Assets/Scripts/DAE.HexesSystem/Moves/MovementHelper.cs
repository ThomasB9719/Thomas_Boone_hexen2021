using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DAE.BoardSystem;

namespace DAE.HexesSystem.Moves
{
    public class MovementHelper<TPosition, TPiece>
        where TPiece : IPiece
    {
        private Board<TPosition, TPiece> _board;
        private Grid<TPosition> _grid;
        private TPiece _piece;

        private TPosition _position;

        private List<TPosition> _validPositions = new List<TPosition>();

        public static Vector2Int[] Directions = new Vector2Int[6]
        {
            new Vector2Int(1,-1), //north east
            new Vector2Int(1,0),//east
            new Vector2Int(0,1), //south east
            new Vector2Int(-1,1), // south west
            new Vector2Int(-1,0), //west
            new Vector2Int(0,-1), //north west
        };

        public MovementHelper(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position)
        {
            _board = board;
            _grid = grid;
            _piece = piece;

            _position = position;
        }

        public MovementHelper<TPosition, TPiece> SouthEast(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(0, 1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> SouthEastBomb(TPosition position, int numTiles = int.MaxValue, params Validator[] validators)
        {
            return MoveBomb(0, 1, position, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> East(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(1, 0, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> EastBomb(TPosition position, int numTiles = int.MaxValue, params Validator[] validators)
        {
            return MoveBomb(1, 0, position, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> NorthEast(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(1, -1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> NorthEastBomb(TPosition position, int numTiles = int.MaxValue, params Validator[] validators)
        {
            return MoveBomb(1, -1, position, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> NorthWest(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(0, -1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> NorthWestBomb(TPosition position, int numTiles = int.MaxValue, params Validator[] validators)
        {
            return MoveBomb(0, -1, position, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> West(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(-1, 0, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> WestBomb(TPosition position, int numTiles = int.MaxValue, params Validator[] validators)
        {
            return MoveBomb(-1, 0, position, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> SouthWest(int numTiles = int.MaxValue, params Validator[] validators)
        {
            return Move(-1, 1, numTiles, validators);
        }

        public MovementHelper<TPosition, TPiece> SouthWestBomb(TPosition position, int numTiles = int.MaxValue, params Validator[] validators)
        {
            return MoveBomb(-1, 1, position, numTiles, validators);
        }

        public delegate bool Validator(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position);

        public MovementHelper<TPosition, TPiece> Move(int xOffset, int yOffset, int numTiles = int.MaxValue, params Validator[] validators)
        {
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

                _validPositions.Add(nextPosition);

                nextXCoordinate += xOffset;
                nextYCoordinate += yOffset;

                hasNextPosition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out nextPosition);

                step++;
            }
            return this;
        }

        public MovementHelper<TPosition, TPiece> MoveBomb(int xOffset, int yOffset, TPosition position, int numTiles = int.MaxValue, params Validator[] validators)
        {
            _position = position;

            if (!_grid.TryGetCoordinateOf(_position, out var coordinate))
                return this;

            var nextXCoordinate = coordinate.x + xOffset;
            var nextYCoordinate = coordinate.y + yOffset;

            var hasNextPosition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out var nextPosition);

            int step = 0;

            while (hasNextPosition && step < numTiles)
            {
                //var isOk = validators.All((v) => v(_board, _grid, _piece, nextPosition));
                //if (!isOk)
                //    return this;

                _validPositions.Add(nextPosition);

                nextXCoordinate += xOffset;
                nextYCoordinate += yOffset;

                hasNextPosition = _grid.TryGetPositionAt(nextXCoordinate, nextYCoordinate, out nextPosition);

                step++;
            }
            return this;
        }

        public List<TPosition> Collect()
        {
            return _validPositions;
        }

        //public static bool IsEmptyTile(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position)
        //    => !board.TryGetPieceAt(position, out _);

        //public static bool HasEnemyPiece(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position)
        //    => board.TryGetPieceAt(position, out var enemyPiece);
    }
}