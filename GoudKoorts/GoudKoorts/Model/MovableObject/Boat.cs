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
                ((Cart)((River)Field).MovableObject).HasLoad = false;
            }
        }

        public override void Move()
        {
            if (((River)Field).Quay == null || Load == 8)
            {
                Field = Field.Next ?? null;
                Field.MovableObject = null;
                Field.Next.MovableObject = this;
            }
            else
            {
                TakeLoad();
            }
        }

        public override string ToString()
        {
            return "B";
        }
    }
}