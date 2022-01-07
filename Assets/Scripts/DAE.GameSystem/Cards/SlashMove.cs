using System.Collections.Generic;
using DAE.BoardSystem;
using DAE.HexesSystem.Moves;
using DAE.ReplaySystem;

namespace DAE.GameSystem.Cards
{
    class SlashMove : CardBase
    {
        public SlashMove(ReplayManager replayManager) : base(replayManager)
        {

        }

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

            var allPositions = new List<Position>();
            var list = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).NorthEast(1)
                .NorthWest(1)
                .West(1)
                .East(1)
                .SouthEast(1)
                .SouthWest(1)
                .Collect();
            if (list.Contains(positionBoard))
            {
                return list;
            }
            allPositions.AddRange(list);
            return allPositions;

            //var allPositionsSpecific = new List<Position>();
            //var listSpecific = new MovementHelper<Position, Piece>(board, grid, piece, positionBoard).NorthEastSpecific(1)
            //    .EastSpecific(1)
            //    .SouthEastSpecific(1)
            //    .SouthWestSpecific(1)
            //    .WestSpecific(1)
            //    .NorthWestSpecific(1)
            //    .Collect();

            //if (listSpecific.Contains(positionBoard))
            //{
            //    return listSpecific;
            //}

            //allPositionsSpecific.AddRange(listSpecific);
            //return allPositionsSpecific;
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
