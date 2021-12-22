using DAE.BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexesSystem
{
    public interface ICard<TPosition, TPiece>
    {
        
        bool InHand { get; set; }

        void Remove();

        void Execute(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position);

        List<TPosition> Positions(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece, TPosition position);
    }
}
