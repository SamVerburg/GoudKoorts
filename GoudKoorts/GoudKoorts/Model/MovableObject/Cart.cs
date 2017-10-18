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
            if (CanMove() && HasCrashed == false)
            {
                Field.MovableObject = null;
                Field = Field.Next ?? null;
                Field.MovableObject = this;
            }
        }

        public bool CanMove()
        {
            if (Field.Next == null)
            {
                if (!(this.Field is Shunter))
                {
                    this.Field.MovableObject = null;
                    return false;
                }
                return true;
            }

            Cart c = (Cart)Field.Next.MovableObject;
            if (c != null)
            {
                if (c.CanMove())
                {
                    return true;
                }
                HasCrashed = true;
            }
            //NEXT FIELD IS SWITCH

            if (Field.Next is Switch)
            {
                Switch s = (Switch) Field.Next;

                //CONVERGING
                if (s.isConverging())
                {
                    
                }
                //DIVERGING
                else
                {
                    return true;
                }

            }




            //NOT SWITCH
            


            return false;
        }

        public override string ToString()
        {
            if (HasLoad)
            {
                return "L";
            }
            else if (HasCrashed)
            {
                return "X";
            }
            return "U";
        }
    }
}