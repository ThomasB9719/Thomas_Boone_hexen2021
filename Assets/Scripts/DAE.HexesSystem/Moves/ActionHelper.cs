using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DAE.BoardSystem;

namespace DAE.HexesSystem.Moves
{
    class ActionHelper<TCard, TPosition, TPiece> where TPiece : IPiece
    {
        private Board<TPosition, TPiece> _board;
        private Grid<TPosition> _grid;
        private TPiece _piece;

        private List<TPosition> _validPositions = new List<TPosition>();

        public Vector2[] Directions = new Vector2[6]
        {
            new Vector2(1,0),
            new Vector2(1,-1),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(-1,1),
            new Vector2(0,1)
        };

        public delegate bool Validator(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position);

        internal ActionHelper<TCard, TPosition, TPiece> East(int numTiles = int.MaxValue, params Validator[] validators)
          => CardMove((int)Directions[0].X, (int)Directions[0].Y, numTiles, validators);

        internal ActionHelper<TCard, TPosition, TPiece> SouthEast(int numTiles = int.MaxValue, params Validator[] validators)
          => CardMove((int)Directions[1].X, (int)Directions[1].Y, numTiles, validators);

        internal ActionHelper<TCard, TPosition, TPiece> SouthWest(int numTiles = int.MaxValue, params Validator[] validators)
         => CardMove((int)Directions[2].X, (int)Directions[2].Y, numTiles, validators);

        internal ActionHelper<TCard, TPosition, TPiece> West(int numTiles = int.MaxValue, params Validator[] validators)
        => CardMove((int)Directions[3].X, (int)Directions[3].Y, numTiles, validators);

        internal ActionHelper<TCard, TPosition, TPiece> NorthWest(int numTiles = int.MaxValue, params Validator[] validators)
       => CardMove((int)Directions[4].X, (int)Directions[4].Y, numTiles, validators);

        internal ActionHelper<TCard, TPosition, TPiece> NorthEast(int numTiles = int.MaxValue, params Validator[] validators)
     => CardMove((int)Directions[5].X, (int)Directions[5].Y, numTiles, validators);

        public ActionHelper<TCard, TPosition, TPiece> CardMove(int xOffset, int yOffset, int numTiles = int.MaxValue, params Validator[] validators)
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
