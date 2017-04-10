using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexToStateMaschine
{
    public class State
    {
        private List<StatusTransition> statusTransitions_ = new List<StatusTransition>();

        public List<StatusTransition> StatusTransitions_
        {
            get { return statusTransitions_; }
            set { statusTransitions_ = value; }
        }

        private bool isErrorState;

        public bool IsErrorState
        {
            get { return isErrorState; }
            set { isErrorState = value; }
        }

        private bool isPreviewEndeState;

        public bool IsPreviewEndeState
        {
            get { return isPreviewEndeState; }
            set { isPreviewEndeState = value; }
        }

        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        
        public State(int number, string value, bool isErrorState = false)
        {
            this.value = value;
            this.number = number;
            this.isErrorState = isErrorState;
        }

        public State Check(char value, StateMaschine stateMaschine)
        {
            foreach(StatusTransition statusTransition in StatusTransitions_)
            {                               
                if(statusTransition.Status_.Value == value.ToString())
                {
                    return statusTransition.Status_;
                }
                else if (statusTransition.Status_.Value == LexerItems.Point.ToString())
                {
                    return statusTransition.Status_;
                }
                else if (statusTransition.Status_.Value == LexerItems.Star.ToString())
                {
                    State returnState = statusTransition.Status_.Check(value, stateMaschine);
                    if (!returnState.IsErrorState)
                    {
                        return returnState;
                    }
                }                
                else if (statusTransition.Status_.Value == LexerItems.QuestionMark.ToString())
                {
                    return statusTransition.Status_;
                }
                else if (statusTransition.Status_.Value == LexerItems.OpenBracket.ToString())
                {
                    State returnState = statusTransition.Status_.Check(value, stateMaschine);
                    if (!returnState.IsErrorState)
                    {
                        return returnState;
                    }
                }
                else if (statusTransition.Status_.Value == LexerItems.CloseBracket.ToString())
                {
                    State returnState = statusTransition.Status_.Check(value, stateMaschine);
                    if (!returnState.IsErrorState)
                    {
                        return returnState;
                    }
                }
                else if (statusTransition.Status_.Value == LexerItems.Pipe.ToString())
                {
                    State returnState = statusTransition.Status_.Check(value, stateMaschine);
                    if (!returnState.IsErrorState)
                    {
                        return returnState;
                    }
                }                
            }
            return new State(-1, "Error", true);
        }        
    }
}
