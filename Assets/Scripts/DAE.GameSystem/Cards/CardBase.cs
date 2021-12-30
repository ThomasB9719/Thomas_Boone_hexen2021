using System;
using System.Collections.Generic;
using DAE.BoardSystem;
using DAE.HexesSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DAE.GameSystem.Cards
{
    class DragEventArgs : EventArgs
    {
        public CardBase Card { get; }
        public DragEventArgs(CardBase card)
            => Card = card;
    }

    [RequireComponent(typeof(Draggable))]
    abstract class CardBase : MonoBehaviour, ICard<Position, Piece>, IBeginDragHandler
    {
        public event EventHandler<DragEventArgs> Dragged;

        //
        private Board<Position, Piece> _board;
        private Grid<Position> _grid;
        private Piece _piece;

        private Position _position;

        private List<Position> _validPositions = new List<Position>();

        public List<Vector2> GiveDirectionNorthEast(int radius)
        {
            List<Vector2> northEastDirections = new List<Vector2>();

            for (int i = 1; i < radius + 1; i++)
            {
                northEastDirections.Add(new Vector2(0, i));
            }

            return northEastDirections;
        }

        public List<Vector2> GiveDirectionNorthWest(int radius)
        {
            List<Vector2> northWestDirections = new List<Vector2>();

            for (int i = 1; i < radius + 1; i++)
            {
                northWestDirections.Add(new Vector2(-i, i));
                Debug.Log(i);
            }

            //northWestDirections.Add(new Vector2(-2, 2));
            //northWestDirections.Add(new Vector2(-3, 3));
            return northWestDirections;
        }

        public List<Position> Collect()
        {
            return _validPositions;
        }
        //

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnDragCard(this, new DragEventArgs(GetComponent<CardBase>()));
        }

        public bool InHand
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value); }
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        public virtual void Execute(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position)
        {
            if (board.TryGetPieceAt(position, out var toPiece))
                board.Take(toPiece);

            board.Move(piece, position);
        }

        public abstract List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position);
        public virtual void OnDragCard(object source, DragEventArgs e)
        {
            var handle = Dragged;
            handle?.Invoke(this, e);
        }

    }
}
