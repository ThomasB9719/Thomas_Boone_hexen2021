using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DAE.SelectionSystem;
using DAE.BoardSystem;
using DAE.HexesSystem;
//using DAE.StateSystem;
using DAE.GameSystem;
//using DAE.GameSystem.GameStates;

namespace DAE.GameSystem
{

    class GameLoop: MonoBehaviour
    {
        //public delegate void PieceAction(Piece piece);

        [SerializeField]
        private GenerateGrid _generateGrid;
        
        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        private Transform _boardParent;

        private SelectionManager<Piece> _selectionManager; //we keep it as a global variable so it doesn't get cleaned up!
        private Grid<Position> _grid;
        private Board<Position, Piece> _board;
        //private MoveManager<Piece> _moveManager;
        //private StateMachine<GameStateBase> _gameStateMachine;

        public void Start()
        {
            _grid = new Grid<Position>(/*8, 8*/);
            ConnectGrid(_grid);
            _board = new Board<Position, Piece>();

            _selectionManager = new SelectionManager<Piece>();
            ConnectPiece(_selectionManager, _grid, _board);

            //_moveManager = new MoveManager<Piece>(_board, _grid);
            //_gameStateMachine = new StateMachine<GameStateBase>();
            //_gameStateMachine.Register(GameState.GamePlayState, 
            //    new GamePlayState(_gameStateMachine, _selectionManager, _moveManager));
            //_gameStateMachine.InitialState = GameState.GamePlayState;

            _board.Moved += (s, e) =>
            {
                if(_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    //var worldPosition = _positionHelper.ToWorldPosition
                    //    (_grid, _boardParent, toCoordinate.x, toCoordinate.y);
                    var worldPosition = _generateGrid.WorldCoordinates;                   

                    //e.Piece.transform.position = worldPosition;
                    e.Piece.MoveTo(worldPosition);
                }
            };

            _board.Placed += (s, e) =>
            {
                if (_grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    //var worldPosition = _positionHelper.ToWorldPosition
                    //    (_grid, _boardParent, toCoordinate.x, toCoordinate.y);
                    var worldPosition = _generateGrid.WorldCoordinates;
                    e.Piece.Place(worldPosition);
                }
            };

            _board.Taken += (s, e) =>
            {
                e.Piece.Taken();
            };

            //    _selectionManager.Selected += (s, e) =>
            //    {
            //        //if (_board.TryGetPositionOf(e.SelectableItem, out var tile))
            //        //{
            //        //    Debug.Log($"Piece {e.SelectableItem} on tile ${tile.gameObject.name}");
            //        //}

            //        //highlight
            //        var positions = _moveManager.ValidPositionFor(e.SelectableItem);
            //        foreach (var position in positions)
            //        {
            //            position.Activate();
            //        }
            //    };

            //    _selectionManager.Deselected += (s, e) =>
            //    {
            //        var positions = _moveManager.ValidPositionFor(e.SelectableItem);
            //        foreach (var position in positions)
            //        {
            //            position.Deactivate();
            //        }
            //    };
            //}

            //public void Activate()
            //{

            //}

            //public void Deactivate()
            //{

        }

        public void DeselectAll()
        {
            _selectionManager.DeselectAll();
        }

        //public void OnPieceSelected(Piece piece)
        //{
        //    _selectionManager.Toggle(piece);
        //}

        public void DoSomething(Action a)
        {

        }

        private void ConnectGrid(Grid<Position> grid)
        {
            var views = FindObjectsOfType<PositionView>();
            foreach (var view in views)
            {
                //Debug.Log($"Value of Tile {view.name} is X: {x} and Y: {y}");

                //TODO: attach model to view
                var position = new Position();
                view.Model = position;
                //view.Clicked += (s, e) => _gameStateMachine.CurrentState.Select(e.Position);

                //var (x,y) = _positionHelper.ToGridPosition(grid, _boardParent, view.transform.position);
                var (x, y) = _generateGrid.GridCoordinates;
                Debug.Log((x, y));

                grid.Register(x, y, position);

                view.gameObject.name = $"Tile ({x}, { y})";
            }
        }

        private void ConnectPiece(SelectionManager<Piece> selectionManager, Grid<Position> grid, Board<Position, Piece> board)
        {
            var pieces = FindObjectsOfType<Piece>();
            foreach (var piece in pieces)
            {
                //var (x, y) = _positionHelper.ToGridPosition(grid, _boardParent, piece.transform.position);
                var (x, y) = _generateGrid.GridCoordinates;

                if (grid.TryGetPositionAt(x, y, out var position))
                {
                    //piece.Callback = OnPieceSelected;
                    //piece.Clicked += (s, e) => _gameStateMachine.CurrentState.Select(piece);
                    //{
                    //    selectionManager.DeselectAll();
                    //    selectionManager.Toggle(s as Piece);
                    //};

                    //callback makes sure that if we click (action), something else will react
                    //piece.Clicked += (s, e) => Debug.Log($"Piece clicked{e.Piece}");

                    board.Place(piece, position);
                }
                
            }
        }
    }
}

