using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace GoudKoorts
{
    public class Switch : Field
    {
        public Rail Upper { get; set; }
        public Rail Lower { get; set; }
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
                    this.Next = Upper;
                    return;
                case State.TOUPPER:
                    State = State.TOLOWER;
                    this.Next = Lower;
                    return;
            }
        }

        public override string ToString()
        {
            if (this.MovableObject != null)
            {
                return MovableObject.ToString();
            }
            switch (State)
            {
                case State.FROMLOWER:
                    return "╔";
                case State.FROMUPPER:
                    return "╚";
                case State.TOLOWER:
                    return "╗";
                case State.TOUPPER:
                    return "╝";
            }
            return null;
        }
        
        public bool isConverging()
        {
            if (State == State.FROMLOWER || State == State.FROMUPPER)
            {
                return true;
            }
            return false;
        }
    }
}