using DAE.BoardSystem;
using DAE.StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.GameSystem.GameStates
{
    class StartState: GameStateBase
    {
        private Board<Position, Piece> _board;

        public StartState(StateMachine<GameStateBase> stateMachine, Board<Position, Piece> board) : base(stateMachine)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("We entered StartState");
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        internal override void ToPlayState()
        {
            StateMachine.MoveState(GameState.PlayState);
        }
    }
}
