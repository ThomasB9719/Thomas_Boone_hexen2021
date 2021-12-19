using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DAE.SelectionSystem;
using DAE.BoardSystem;
using DAE.HexesSystem;
using DAE.StateSystem;
using DAE.GameSystem;
using DAE.GameSystem.GameStates;

namespace DAE.GameSystem
{

    class GameLoop : MonoBehaviour
    {
        //public delegate void PieceAction(Piece piece);
        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        private Transform _boardParent;

        private SelectionManager<Piece> _selectionManager; //we keep it as a global variable so it doesn't get cleaned up!
        private Grid<Position> _grid;
        private Board<Position, Piece> _board;
        private MoveManager<Position,Piece> _moveManager;
        //private StateMachine<GameStateBase> _gameStateMachine;

        public void Start()
        {
            _grid = new Grid<Position>(3);
            ConnectGrid(_grid);
            _board = new Board<Position, Piece>();

            _moveManager = new MoveManager<Position, Piece>(_board, _grid/*, _replayManager*/);

            //_gameStateMachine = new StateMachine<GameStateBase>();

            //var gameplayState = new GamePlayState(_gameStateMachine, _selectionManager, _board, _moveManager);
            //_gameStateMachine.Register(GameState.GamePlayState, gameplayState);

            _selectionManager = new SelectionManager<Piece>();
            ConnectPiece(_selectionManager, _grid, _board);

            //_moveManager = new MoveManager<Piece>(_board, _grid);
            //_gameStateMachine = new StateMachine<GameStateBase>();
            //_gameStateMachine.Register(GameState.GamePlayState, 
            //    new GamePlayState(_gameStateMachine, _selectionManager, _moveManager));
            //_gameStateMachine.InitialState = GameState.GamePlayState;

            _board.Moved += (s, e) =>
            {
                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition
                        (_grid, _boardParent, toCoordinate.x, toCoordinate.y);

                    //e.Piece.transform.position = worldPosition;
                    e.Piece.MoveTo(worldPosition);
                }
            };

            _board.Placed += (s, e) =>
            {
                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition = _positionHelper.ToWorldPosition
                        (_grid, _boardParent, toCoordinate.x, toCoordinate.y);
                    e.Piece.Place(worldPosition);
                }
            };

            _board.Taken += (s, e) =>
            {
                e.Piece.Taken();
            };

            _selectionManager.Selected += (s, e) =>
            {
                //if (_board.TryGetPositionOf(e.SelectableItem, out var position))
                //{
                //    position.Activated();
                //    Debug.Log($"Piece {e.SelectableItem} on tile ${position.gameObject.name}");
                //}

                ////highlight
                var positions = _moveManager.ValidPositionFor(e.SelectableItem);
                foreach (var position in positions)
                {
                    position.Activated();
                    Debug.Log(position);
                }

                //if (validPositions.Contains(position))
                //{
                //    _selectionManager.DeselectAll();
                //}
            };

            _selectionManager.Deselected += (s, e) =>
            {
                var positions = _moveManager.ValidPositionFor(e.SelectableItem);
                foreach (var position in positions)
                {
                    position.Deactivated();
                }
            };

            //}

            //public void Activate()
            //{

            //}

            //public void Deactivate()
            //{

        }

        //public void DeselectAll()
        //{
        //    _selectionManager.DeselectAll();
        //}

        //public void OnPieceSelected(Piece piece)
        //{
        //    _selectionManager.Toggle(piece);
        //}

        public void DoSomething(Action a)
        {

        }

        private void ConnectGrid(Grid<Position> grid)
        {
            var positions = FindObjectsOfType<Position>();
            //Debug.Log(positions.Length);
            foreach (var position in positions)
            {
                //Debug.Log($"Value of Tile {view.name} is X: {x} and Y: {y}");
                //TODO: attach model to view
                //view.Clicked += (s, e) => _gameStateMachine.CurrentState.Select(e.Position);

                position.Clicked += (s, e) =>
                {
                    if (!_selectionManager.HasSelection)
                        return;

                    var selectedPiece = _selectionManager.SelectedItem;
                    var validPositions = _moveManager.ValidPositionFor(_selectionManager.SelectedItem);
                    //Debug.Log(validPositions);

                    if (validPositions.Contains(position))
                    {
                        _selectionManager.DeselectAll();
                        _moveManager.Move(selectedPiece, position);
                        //_currentPlayerID = (_currentPlayerID + 1) % 2;
                    }
                };

                var (x, y) = _positionHelper.ToGridPosition(grid, _boardParent, position.transform.position);

                grid.Register((int)x, (int)y, position);
                //Debug.Log($"{view} + {x} +  ,  + {y}");
                //view.gameObject.name = $"Tile ({x}, {y})";
            }
        }

        private void ConnectPiece(SelectionManager<Piece> selectionManager, Grid<Position> grid, Board<Position, Piece> board)
        {
            var pieces = FindObjectsOfType<Piece>();
            foreach (var piece in pieces)
            {
                var (x, y) = _positionHelper.ToGridPosition(grid, _boardParent, piece.transform.position);

                if (grid.TryGetPositionAt(x, y, out var position))
                {
                    //piece.Callback = OnPieceSelected;
                    piece.Clicked += (s, e) => /*_gameStateMachine.CurrentState.*//*Select(e.Piece);*/
                    {
                        selectionManager.DeselectAll();
                        selectionManager.Toggle(/*s as Piece*/e.Piece);
                    };

                    //callback makes sure that if we click (action), something else will react
                    piece.Clicked += (s, e) => /*Debug.Log($"Piece clicked{e.Piece}");*/

                    board.Place(piece, position);
                }

            }
        }

        //internal void Select(Piece piece)
        //=> _gameStateMachine.CurrentState.Select(piece);

        //internal void Select(Position position)
        //=> _gameStateMachine.CurrentState.Select(position);
    }
}

