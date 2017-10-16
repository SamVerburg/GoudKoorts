using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class Switch : Field
    {
        public Field Upper { get; set; }
        public Field Lower { get; set; }
        public State State { get; set; }

        public bool CanMoveTo(Cart cart)
        {
            if (cart.Field == Upper)
            {
                if (State == State.FROMUPPER)
                {
                    return true;
                }
            }
            else
            {
                if (State == State.FROMLOWER)
                {
                    return true;
                }
            }

            return false;
        }

        public void SwitchState()
        {
            if (this.MovableObject != null)
            {
                return;
            }

            switch (State)
            {
                case State.FROMLOWER:
                    State = State.FROMUPPER;
                    return;
                case State.FROMUPPER:
                    State = State.FROMLOWER;
                    return;
                case State.TOLOWER:
                    State = State.TOUPPER;
                    return;
                case State.TOUPPER:
                    State = State.TOLOWER;
                    return;
            }
        }
    }
}