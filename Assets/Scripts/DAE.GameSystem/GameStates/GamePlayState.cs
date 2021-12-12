using DAE.HexesSystem;
using DAE.SelectionSystem;
using DAE.StateSystem;
using DAE.BoardSystem;

namespace DAE.GameSystem.GameStates
{
    class GamePlayState : GameStateBase
    {
        private SelectionManager<Piece> _selectionManager; //we keep it as a global variable so it doesn't get cleaned up!
        //private Grid<Position> _grid;
        private MoveManager<Piece> _moveManager;
        private Board<Position, Piece> _board;

        //private int _currentPlayerID = 0;

        public GamePlayState(StateMachine<GameStateBase> stateMachine,
                SelectionManager<Piece> selectionManager,
                Board<Position, Piece> board,
                MoveManager<Piece> moveManager) : base(stateMachine)
        {
            _selectionManager = selectionManager;
            _moveManager = moveManager;
            _board = board;

            _selectionManager.Selected += (s, e) =>
            {

            };

            _selectionManager.Deselected += (s, e) =>
            {

            };
        }

        public override void OnEnter()
        {
            _selectionManager.Selected += OnPieceSelected;
            _selectionManager.Deselected += OnPieceDeselected;
        }

        public override void OnExit()
        {
            _selectionManager.Selected -= OnPieceSelected;
            _selectionManager.Deselected -= OnPieceDeselected;
        }


        public override void Select(Position position)
        {
            var hasPiece = _board.TryGetPieceAt(position, out var piece);
            if (hasPiece /*&& piece.PlayerID == _currentPlayerID*/)
            {
                Select(piece);
                return;
            }
            else
            {
                if (!_selectionManager.HasSelection)
                    return;

                var selectedPiece = _selectionManager.SelectedItem;
                var validPositions = _moveManager.ValidPositionFor(_selectionManager.SelectedItem);

                if (validPositions.Contains(position))
                {
                    _selectionManager.DeselectAll();
                    _moveManager.Move(selectedPiece, position);
                    //_currentPlayerID = (_currentPlayerID + 1) % 2;
                }
            }
        }

        public override void Select(Piece piece)
        {
            //if (piece.PlayerID == _currentPlayerID)
            //{
            //    _selectionManager.DeselectAll();
            //    _selectionManager.Toggle(piece);
            //}
            //else
            //{
                if (_board.TryGetPositionOf(piece, out var toPosition))
                {
                    Select(toPosition);
                }
            //}
        }

        public override void Deselect(Piece piece)
        {
            _selectionManager.DeselectAll();
            _selectionManager.Toggle(piece);
        }

        private void OnPieceSelected(object source, SelectableItemEventArgs<Piece> eventArgs)
        {
            //if (_board.TryGetPositionOf(e.SelectableItem, out var tile))
            //{
            //    Debug.Log($"Piece {e.SelectableItem} on tile ${tile.gameObject.name}");
            //}

            //highlight
            var positions = _moveManager.ValidPositionFor(eventArgs.SelectableItem);
            foreach (var position in positions)
            {
                position.Activate();
            }
        }

        private void OnPieceDeselected(object source, SelectableItemEventArgs<Piece> eventArgs)
        {
            var positions = _moveManager.ValidPositionFor(eventArgs.SelectableItem);
            foreach (var position in positions)
            {
                position.Deactivate();
            }
        }

        internal override void Backward()
        {
            StateMachine.MoveState(GameState.ReplayState);
        }
    }
}
