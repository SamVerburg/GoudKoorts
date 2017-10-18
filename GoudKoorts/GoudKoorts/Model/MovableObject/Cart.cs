using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class Cart : MovableObject
    {
        public bool HasLoad { get; set; }

        public bool HasCrashed { get; set; }

        public override void Move()
        {
            if (Field.Next == null)
            {
                return;
            }
            
            if (!(Field.Next is Switch))
            {
                if (Field.Next.MovableObject == null)
                {
                    Field.MovableObject = null;
                    Field = Field.Next ?? null;
                    Field.MovableObject = this;
                }
                else
                {
                    if (Field.Next is Shunter)
                    {
                        return;
                    }
                    HasCrashed = true;
                }
            }
            else if (!((Switch)Field.Next).isConverging())
            {
                if (Field.Next.MovableObject == null)
                {
                    Field.MovableObject = null;
                    Field = Field.Next ?? null;
                    Field.MovableObject = this;
                }
                else
                {
                    HasCrashed = true;
                }
            }
            else if (((Switch)Field.Next).CanMoveTo(this))
            {
                if (Field.Next.MovableObject == null)
                {
                    Field.MovableObject = null;
                    Field = Field.Next ?? null;
                    Field.MovableObject = this;
                }
                else
                {
                    HasCrashed = true;
                }
            }
        }
    }
}