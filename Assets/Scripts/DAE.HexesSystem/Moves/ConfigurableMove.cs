using DAE.BoardSystem;
//using DAE.ReplaySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexesSystem.Moves
{
    class ConfigurableMove<TPosition, TPiece> : MoveBase<TPosition,TPiece>
        where TPiece : IPiece
    {
        public delegate List<TPosition> PositionCollector(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece);

        private PositionCollector _positionCollector;

        public ConfigurableMove(/*ReplayManager replayManager,*/ PositionCollector positionCollector)/*: base(replayManager)*/
        {
            _positionCollector = positionCollector;
        }

        public override List<TPosition> Positions(Board<TPosition, TPiece> board, Grid<TPosition> grid, TPiece piece)
            => _positionCollector(board, grid, piece);
    }
}
