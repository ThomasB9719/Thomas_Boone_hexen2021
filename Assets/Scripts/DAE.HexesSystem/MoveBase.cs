using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAE.BoardSystem;
//using DAE.ReplaySystem;

namespace DAE.HexesSystem
{
    abstract class MoveBase<TPiece> : IMove<TPiece>
        where TPiece : IPiece
    {
        //protected ReplayManager ReplayManager;

        //protected MoveBase(ReplayManager replayManager)
        //{
        //    ReplayManager = replayManager;
        //}

        public virtual bool CanExecute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece)
        {
            return true;
        }

        public virtual void Execute(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece, Position position)
        {
            var hasEnemyPiece = board.TryGetPieceAt(position, out var toPiece);
            board.TryGetPositionOf(piece, out var fromPosition);

            Action forward = () =>
            {
                if (hasEnemyPiece)
                    board.Take(toPiece);
                board.Move(piece, position);
            };

            Action backward = () =>
            {
                board.Move(piece, fromPosition);

                if (hasEnemyPiece)
                    board.Place(toPiece, position);
            };

            //ReplayManager.Execute(new DelegateReplayCommand(forward, backward));
        }

        public abstract List<Position> Positions(Board<Position, TPiece> board, Grid<Position> grid, TPiece piece);
    }
}
