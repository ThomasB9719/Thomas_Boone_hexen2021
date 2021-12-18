using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexesSystem
{
    interface IMove<TPosition,TPiece>
        where TPiece : IPiece
    {
        bool CanExecute(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece);

        void Execute(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position);

        List<TPosition> Positions(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece);
    }
}
