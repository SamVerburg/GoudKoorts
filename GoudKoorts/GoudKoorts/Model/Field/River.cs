using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class River : Field
    {
        public string PrintValue { get; set; }

        public Field Quay { get; set; }

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
            if (Quay == null)
            {
                return " ";
            }
            return "K";
        }
    }
}