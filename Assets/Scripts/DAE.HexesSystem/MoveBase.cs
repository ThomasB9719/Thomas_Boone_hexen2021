using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAE.BoardSystem;
//using DAE.ReplaySystem;

namespace DAE.HexesSystem
{
    abstract class MoveBase<TPosition, TPiece> : IMove<TPosition, TPiece>
        where TPiece : IPiece
    {
        //protected ReplayManager ReplayManager;

        //protected MoveBase(ReplayManager replayManager)
        //{
        //    ReplayManager = replayManager;
        //}

        public virtual bool CanExecute(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece)
        {
            return true;
        }

        public virtual void Execute(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position)
        {
            if (board.TryGetPieceAt(position, out var toPiece))
                board.Take(toPiece);

            board.Move(piece, position);

            
            //var hasEnemyPiece = board.TryGetPieceAt(position, out var toPiece);
            //board.TryGetPositionOf(piece, out var fromPosition);

            //Action forward = () =>
            //{
            //if (hasEnemyPiece)
            //    board.Take(toPiece);
            //board.Move(piece, position);
            //};

            //Action backward = () =>
            //{
            //board.Move(piece, fromPosition);

            //if (hasEnemyPiece)
            //    board.Place(toPiece, position);
            //};

            //ReplayManager.Execute(new DelegateReplayCommand(forward, backward));
        }

        public abstract List<TPosition> Positions(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece);
    }
}
