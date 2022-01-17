using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAE.BoardSystem;
using DAE.GameSystem.Cards;
using DAE.HexesSystem;
using DAE.StateSystem;

namespace DAE.GameSystem.GameStates
{
    class GameStateBase: IState<GameStateBase>
    {
        public StateMachine<GameStateBase> StateMachine { get; set; }
        
        public GameStateBase(StateMachine<GameStateBase> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }

        public virtual void Entered(Position position, CardBase selectedCard, Piece playerPiece, Grid<Position> grid)
        {
            
        }

        public virtual void Exited(Position position, CardBase selectedCard, Piece playerPiece, Grid<Position> grid)
        {
         
        }

        public virtual void DroppedAt(Position position, Deck<Position, Piece, CardBase> deck, CardBase selectedCard, Piece playerPiece, Grid<Position> grid)
        {
            
        }

        internal virtual void Forward()
        {

        }

        internal virtual void ToPlayState()
        {

        }

        internal virtual void ToEndState()
        {

        }
    }
}
