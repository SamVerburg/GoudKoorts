using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public abstract class MovableObject
    {
        public Field Field { get; set; }

        public abstract void Move();

        public abstract override string ToString();
    }
}