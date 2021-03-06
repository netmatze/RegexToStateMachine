﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RegexToStateMaschine
{
    public class StateMaschine
    {
        private bool reachedEndeState = false;

        public bool ReachedEndeState
        {
            get 
            {
                if (TextLength >= MinLength)
                    return true;
                return false;
            }           
        }

        public State ActualState
        {
            get;
            set;
        }

        public State StartState
        {
            get;
            set;
        }

        public int MinLength
        {
            get;
            set;
        }

        public int TextLength
        {
            get;
            set;
        }


        public StateMaschine()
        {            
         
        }

        public State AddState(int number, string value, State previousState)
        {
            if (StartState == null)
            {
                StartState = new State(-1, "Start");
                ActualState = new State(number, value);
                StartState.StatusTransitions_.Add(new StatusTransition(ActualState));                
            }
            else
            {
                ActualState = new State(number, value);
                if (previousState != null)
                {
                    previousState.StatusTransitions_.Add(new StatusTransition(ActualState));
                }
            }
            return ActualState;
        }

        public State AddState(State actualState, State previousState)
        {
            if (StartState == null)
            {
                StartState = new State(-1, "Start");
                ActualState = actualState;
                StartState.StatusTransitions_.Add(new StatusTransition(ActualState));
            }
            else
            {
                ActualState = actualState;
                if (previousState != null)
                {
                    previousState.StatusTransitions_.Add(new StatusTransition(ActualState));
                }
            }            
            return ActualState;
        }

        public void RecognizeMode()
        {
            ActualState = StartState;
        }

        public bool Recognize(char chr)
        {            
            ActualState = ActualState.Check(chr, this);            
            if (!ActualState.IsErrorState)
            {
                return true;
            }
            return false;
        }

        public void Debug(string regularExpression)
        {
            Trace.WriteLine(regularExpression);
            ActualState = StartState;
            InnerDebug(ActualState);
        }

        private void InnerDebug(State state)
        {
            foreach (var stateTransition in state.StatusTransitions_)
            {
                Trace.WriteLine(string.Format(" from {0} ({1}) to {2} ({3}) ", 
                    state.Value, state.Number, stateTransition.Status_.Value, stateTransition.Status_.Number));
                if (!stateTransition.Marked)
                {
                    stateTransition.Marked = true;
                    if (stateTransition.Status_ != null)
                    {
                        InnerDebug(stateTransition.Status_);
                    }
                }                
            }
        }
    }
}
