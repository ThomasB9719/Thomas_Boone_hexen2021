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

    public class CardEventArgs<TCard, TPosition, TPiece> : EventArgs
        where TCard : ICard<TPosition, TPiece>
    {
        public TCard Card { get; }

        public CardEventArgs(TCard card)
        {
            Card = card;
        }
    }

    public class Deck<TPosition, TPiece, TCard>
        where TPiece: IPiece
        where TCard : ICard<TPosition, TPiece>
    {

        public EventHandler<CardEventArgs<TCard, TPosition, TPiece>> CardPlayed;

        private List<TCard> _cards = new List<TCard>();
        private List<TCard> _inHand = new List<TCard>();

        private readonly Board<TPosition, TPiece> _board;
        private readonly Grid<TPosition> _grid;

        public Deck(Board<TPosition, TPiece> board, Grid<TPosition> grid)
        {
            _board = board;
            _grid = grid;
        }

        public void Move(TCard card, TPiece piece, TPosition position)
        {
            if (!_inHand.Contains(card))
                return;

            if (!card.Positions(_board, _grid, piece, position).Contains(position))
                return;
           
            card.Execute(_board, _grid, piece, position);
            _inHand.Remove(card);
            card.Remove();

            OnCardPlayed(new CardEventArgs<TCard, TPosition, TPiece>(card));
        }
        
        public void Add(TCard card)
        {
            _cards.Add(card);
            card.InHand = false;
        }

        public void FillHand()
        {
            for(int i = _inHand.Count; i < 5; i++)
                AddCardToHand();
        }

        private void AddCardToHand()
        {
            if (_cards.Count <= 0)
                return;

            var idx = UnityEngine.Random.Range(0, _cards.Count - 1);
            
            var card = _cards[idx];

            _cards.RemoveAt(idx);

            _inHand.Add(card);

            card.InHand = true;
        }
  
        protected virtual void OnCardPlayed(CardEventArgs<TCard, TPosition, TPiece> eventArgs)
        {
            var handler = CardPlayed;
            handler?.Invoke(this, eventArgs);
        }
    }
}
