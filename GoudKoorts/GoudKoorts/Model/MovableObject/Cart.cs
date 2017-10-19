using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class Cart : MovableObject
    {
        public bool HasLoad { get; set; } = true;

        public bool HasCrashed { get; set; }

        public override void Move()
        {
            if (Field == null) return;
            if (!CanMove()) return;

            Field.MovableObject = null;
            if (Field.Next != null)
            {
                Field = Field.Next;
                Field.MovableObject = this;
            }
            else
            {
                Field = null;
            }
        }

        public bool CanMove()
        {
            bool canMove = true;

            if (Field.Next == null)
            {
                if (Field is Shunter)
                {
                    canMove = false;
                }
                return canMove;
            }

            Cart nextCart = (Cart)Field.Next.MovableObject;
            bool nextCartCanMove = false;
            if (nextCart != null)
            {
                nextCartCanMove = nextCart.CanMove();
            }

            //NEXT FIELD IS SWITCH

            if (Field.Next is Switch)
            {
                Switch s = (Switch)Field.Next;

                //CONVERGING
                if (s.isConverging())
                {
                    if (s.CanMoveTo(this))
                    {
                        if (nextCart != null && !nextCartCanMove)
                        {
                            HasCrashed = true;
                            canMove = false;
                        }
                    }
                    else
                    {
                        canMove = false;
                    }
                }
                //DIVERGING
                else if (!s.isConverging())
                {
                    if (nextCart != null)
                    {
                        HasCrashed = true;
                        canMove = false;
                    }
                }

            }
            //NOT SWITCH
            else if (nextCart != null)
            {
                canMove = nextCartCanMove;
                if (nextCartCanMove == false)
                {
                    if (!(Field is Shunter))
                    {
                        HasCrashed = true;
                    }
                }
            }

            return canMove;
        }

        public override string ToString()
        {
            if (HasCrashed)
            {
                return "X";
            }
            if (HasLoad)
            {
                return "L";
            }
            return "U";
        }
    }
}