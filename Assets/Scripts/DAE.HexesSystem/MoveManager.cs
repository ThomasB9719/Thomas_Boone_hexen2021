using DAE.BoardSystem;
using DAE.HexesSystem.Moves;
using DAE.Commons;
//using DAE.ReplaySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DAE.HexesSystem { 
    public class MoveManager<TPosition, TPiece>
        where TPiece: IPiece
    {
        private MultiValueDictionary<PieceType, IMove<TPosition,TPiece>> _moves = new MultiValueDictionary<PieceType, IMove<TPosition,TPiece>>();

        private readonly Board<TPosition, TPiece> _board;
        private readonly Grid<TPosition> _grid;
        //private readonly ReplayManager _replayManager;

        public MoveManager(Board<TPosition, TPiece> board, Grid<TPosition> grid/*, ReplayManager replayManager*/)
        {
            _board = board;
            _grid = grid;
            //_replayManager = replayManager;

            InitializeMoves();
        }

        

        public List<TPosition> ValidPositionFor(TPiece piece)
        {
            return _moves[piece.PieceType]
                .Where(m => m.CanExecute(_board, _grid, piece))
                .SelectMany(m => m.Positions(_board, _grid, piece))
                .ToList();
        }

        public void Move(TPiece piece, TPosition position)
        {
            _moves[piece.PieceType]
                .Where(m => m.CanExecute(_board, _grid, piece))
                .First(m => m.Positions(_board, _grid, piece).Contains(position))
                .Execute(_board, _grid, piece, position);
        }
        
        private void InitializeMoves()
        {
            _moves.Add(PieceType.Pawn, new ConfigurableMove<TPosition,TPiece>(/*_replayManager,*/
                (b,g,p) => new MovementHelper<TPosition,TPiece>(b, g, p)
                       //.North(1, MovementHelper<TPosition,TPiece>.IsEmptyTile)
                       //.South(1, MovementHelper<TPosition, TPiece>.IsEmptyTile)
                       .NorthEast(1, MovementHelper<TPosition, TPiece>.IsEmptyTile)
                    //.NorthEast(1, MovementHelper<TPosition,TPiece>.HasEnemyPiece)
                    //.NorthWest(1, MovementHelper<TPosition,TPiece>.HasEnemyPiece)
                    .Collect()));

            _moves.Add(PieceType.Pawn, new PawnDoubleMove<TPosition,TPiece>(/*_replayManager*/));

            _moves.Add(PieceType.King, new ConfigurableMove<TPosition,TPiece>(/*_replayManager,*/
                (b, g, p) => new MovementHelper<TPosition,TPiece>(b, g, p)
                    //.North(1)
                    .NorthEast(1)
                    .East(1)
                    .SouthEast(1)
                    .South(1)
                    .SouthWest(1)
                    .West(1)
                    .NorthWest(1)
                    .Collect()));
        }
    }
}
