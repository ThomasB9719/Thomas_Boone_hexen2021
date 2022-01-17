//using DAE.ReplaySystem;
//using DAE.StateSystem;
//using UnityEngine;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DAE.GameSystem.GameStates
//{
//    class ReplayState: GameStateBase
//    {
//        private readonly ReplayManager _replayManager;

//        public ReplayState(StateMachine<GameStateBase> stateMachine, ReplayManager replayManager) : base(stateMachine)
//        {
//            this._replayManager = replayManager;
//        }

//        public override void OnEnter()
//        {
//            _replayManager.Backward();
//            Debug.Log("We entered ReplayState");
//        }

//        public override void OnExit()
//        {
//            _replayManager.ForwardToEnd();
//        }

//        internal override void ToEndState()
//        {
//            _replayManager.Backward();
//        }

//        internal override void Forward()
//        {
//            _replayManager.Forward();

//            if(_replayManager.IsAtEnd)
//            {
//                StateMachine.MoveState(GameState.GamePlayState);
//            }
//        }
//    }
//}
