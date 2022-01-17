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
     
        }

        public abstract List<Position> Positions(Board<Position, Piece> board, Grid<Position> grid, Piece piece, Position position);
        public virtual void OnDragCard(object source, DragEventArgs e)
        {
            var handle = Dragged;
            handle?.Invoke(this, e);
        }

    }
}
