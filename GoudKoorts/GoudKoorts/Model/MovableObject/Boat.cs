using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class Boat : MovableObject
    {
        public int Load { get; set; }

        public void TakeLoad()
        {
            if (((River)Field).Quay.MovableObject is Cart)
            {
                Load++;
                ((Cart)((River)Field).Quay.MovableObject).HasLoad = false;
            }
        }

        public override void Move()
        {
            if (Field == null)
            {
                return;
            }

            if (((River)Field).Quay != null)
            {
                if (Load != 8)
                {
                    TakeLoad();
                    return;
                }
            }

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

        public override string ToString()
        {
            return "B";
        }
    }
}