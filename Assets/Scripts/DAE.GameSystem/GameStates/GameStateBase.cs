﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //public virtual void Select()
        //{
            //chess example: select piece and position
        //}

        //public virtual void Deselect()
        //{
            //chess example: deselect piece
        //}

        internal virtual void Forward()
        {

        }

        internal virtual void Backward()
        {

        }
    }
}