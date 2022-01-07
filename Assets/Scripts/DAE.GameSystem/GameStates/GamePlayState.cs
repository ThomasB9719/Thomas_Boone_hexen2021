using DAE.BoardSystem;
using DAE.StateSystem;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.GameSystem.GameStates
{
    class GamePlayState: GameStateBase
    {
        private Board<Position, Piece> _board;

        public GamePlayState(StateMachine<GameStateBase> stateMachine, Board<Position, Piece> board): base(stateMachine)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("We entered GamePlayState");
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        internal override void Backward()
        {
            StateMachine.MoveState(GameState.ReplayState);
        }
    }
}
