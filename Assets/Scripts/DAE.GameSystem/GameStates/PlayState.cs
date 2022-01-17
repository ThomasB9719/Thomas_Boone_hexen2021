using DAE.BoardSystem;
using DAE.StateSystem;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAE.GameSystem.Cards;
using DAE.HexesSystem;

namespace DAE.GameSystem.GameStates
{
    class PlayState: GameStateBase
    {
        private Board<Position, Piece> _board;
        private Grid<Position> _grid;
        private CardBase _selectedCard;
        private Piece _playerPiece;

        public PlayState(StateMachine<GameStateBase> stateMachine, Board<Position, Piece> board): base(stateMachine)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("We entered PlayState");
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Entered(Position position, CardBase selectedCard, Piece playerPiece, Grid<Position> grid)
        {
            _selectedCard = selectedCard;
            _playerPiece = playerPiece;
            _grid = grid;

            if (_selectedCard == null)
                return;

            if (!_playerPiece)
                return;

            var validPositions = _selectedCard.Positions(_board, _grid, _playerPiece, position);
            foreach (var validPosition in validPositions)
                validPosition.Activated();
        }

        public override void Exited(Position position, CardBase selectedCard, Piece playerPiece, Grid<Position> grid)
        {
            _selectedCard = selectedCard;
            _playerPiece = playerPiece;
            _grid = grid;

            if (_selectedCard == null)
                return;

            if (!_playerPiece)
                return;

            var validPositions = _selectedCard.Positions(_board, _grid, _playerPiece, position);
            foreach (var validPosition in validPositions)
                validPosition.Deactivated();
        }

        public override void DroppedAt(Position position, Deck<Position, Piece, CardBase> deck, CardBase selectedCard, Piece playerPiece, Grid<Position> grid)
        {
            _selectedCard = selectedCard;
            _playerPiece = playerPiece;
            _grid = grid;

            Debug.Log("DroppedAt called");
            if (_selectedCard == null)
                return;

            if (!_playerPiece)
                return;

            var validPositions = _selectedCard.Positions(_board, _grid, _playerPiece, position);
            foreach (var validPosition in validPositions)
                validPosition.Deactivated();

            if (validPositions.Contains(position))
                deck.Move(_selectedCard, _playerPiece, position);

            _selectedCard = null;
        }

        internal override void ToEndState()
        {
            StateMachine.MoveState(GameState.EndState);
        }
    }
}
