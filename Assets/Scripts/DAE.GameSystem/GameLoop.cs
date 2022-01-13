using System.Collections.Generic;
using UnityEngine;
using DAE.BoardSystem;
using DAE.HexesSystem;
using DAE.GameSystem.Cards;
using DAE.GameSystem.GameStates;
using DAE.StateSystem;
using DAE.ReplaySystem;

namespace DAE.GameSystem
{
    class GameLoop : MonoBehaviour
    {
        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        private Transform _cardContainer;

        [SerializeField]
        private List<CardBase> _cardTypes;
        
        [SerializeField]
        private Piece _playerPiece;

        private int _maxNumberOfCards = 20;
        private Grid<Position> _grid;
        private Board<Position, Piece> _board;
        private Deck<Position,Piece, CardBase> _deck;
        private CardBase _selectedCard;
        private StateMachine<GameStateBase> _gameStateMachine;

        public void Start()
        {
            _grid = new Grid<Position>(3);
            _board = new Board<Position, Piece>();
            _deck = new Deck<Position, Piece, CardBase>(_board, _grid);

            _gameStateMachine = new StateMachine<GameStateBase>();

            var replayManager = new ReplayManager();

            var gameplayState = new GamePlayState(_gameStateMachine, _board);
            _gameStateMachine.Register(GameState.GamePlayState, gameplayState);

            var replayState = new ReplayState(_gameStateMachine, replayManager);
            _gameStateMachine.Register(GameState.ReplayState, replayState);

            _gameStateMachine.InitialState = GameState.GamePlayState;

            ConnectGrid(_grid);
            ConnectPiece(_grid, _board);
            GenerateCards();

            _deck.FillHand();

            _board.Moved += (s, e) =>
            {
                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition(_grid, toCoordinate.x, toCoordinate.y);
                    e.Piece.MoveTo(worldPosition);
                }
            };

            _board.Placed += (s, e) =>
            {
                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition(_grid, toCoordinate.x, toCoordinate.y);
                    e.Piece.Place(worldPosition);
                }
            };

            _board.Taken += (s, e) =>
            {
                e.Piece.Taken();
            };

            _deck.CardPlayed += (s, e) =>
            {
                _deck.FillHand();
            };
        }

        public void Forward()
        {
            _gameStateMachine.CurrentState.Forward();
        }

        public void Backward()
        {
            _gameStateMachine.CurrentState.Backward();
        }

        private void ConnectGrid(Grid<Position> grid)
        {
            var positions = FindObjectsOfType<Position>();
            foreach (var position in positions)
            {               
                position.Exited += (s, e) => Exited(e.Position);
                position.Entered += (s, e) => Entered(e.Position);
                position.Dropped += (s, e) => DroppedAt(e.Position);

                var (x, y) = _positionHelper.ToGridPosition(grid, position.transform.position);
                grid.Register((int)x, (int)y, position);
            }
        }

        private void ConnectPiece(Grid<Position> grid, Board<Position, Piece> board)
        {
            var pieces = FindObjectsOfType<Piece>();
            foreach (var piece in pieces)
            {
                var (x, y) = _positionHelper.ToGridPosition(grid, piece.transform.position);
                if (grid.TryGetPositionAt(x, y, out var position))
                {
                    board.Place(piece, position);
                    //Debug.Log($"{ piece} + {position}");
                }
            }
        }

        private void GenerateCards()
        {
            for(int i = 0; i < _maxNumberOfCards; i++)
            {
                var cardType = UnityEngine.Random.Range(0, _cardTypes.Count /*- 1*/);
                var card = Instantiate<CardBase>(_cardTypes[cardType], _cardContainer);

                card.Dragged += (s, e) => Dragged(e.Card);

                _deck.Add(card);
            }
        }

        private void Dragged(CardBase card)
        {
            _selectedCard = card;
        }

        private void DroppedAt(Position position)
        {
            if (_selectedCard == null)
                return;

            if (!_playerPiece)
                return;

            var validPositions = _selectedCard.Positions(_board, _grid, _playerPiece, position);
            foreach (var validPosition in validPositions)
                validPosition.Deactivated();

            if (validPositions.Contains(position))
                _deck.Move(_selectedCard, _playerPiece, position);

            _selectedCard = null;
        }

        private void Entered(Position position)
        {
            if (_selectedCard == null)
                return;

            if (!_playerPiece)
                return;

            var validPositions = _selectedCard.Positions(_board, _grid, _playerPiece, position);
            foreach (var validPosition in validPositions)
                validPosition.Activated();
        }

        private void Exited(Position position)
        {
            if (_selectedCard == null)
                return;

            if (!_playerPiece)
                return;

            var validPositions = _selectedCard.Positions(_board, _grid, _playerPiece, position);
            foreach (var validPosition in validPositions)
                validPosition.Deactivated();
        }
    }
}

