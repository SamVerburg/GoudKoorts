﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;

namespace GoudKoorts
{
    public class Rail : Field
    {
        public string PrintValue { get; set; }

        public override string ToString()
        {
            if (this.MovableObject != null)
            {
                return MovableObject.ToString();
            }
            if (PrintValue != null)
            {
                return PrintValue;
            }

            if (this.Next is Switch)
            {
                Switch s = (Switch)this.Next;
                if (s.isConverging())
                {
                    if (s.Upper == this)
                    {
                        switch (s.State)
                        {
                            case State.TOLOWER:
                                return "╔";
                            case State.FROMUPPER:
                                return "╗";
                            case State.TOUPPER:
                                return "╚";
                            case State.FROMLOWER:
                                return "╗";
                        }
                    }
                    else
                    {
                        switch (s.State)
                        {
                            case State.TOLOWER:
                                return "╚";
                            case State.FROMUPPER:
                                return "╝";
                            case State.TOUPPER:
                                return "╔";
                            case State.FROMLOWER:
                                return "╝";
                        }
                    }
                }
            }
            return "═";
        }
    }
}