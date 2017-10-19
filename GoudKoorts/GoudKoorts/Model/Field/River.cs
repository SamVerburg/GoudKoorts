﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class River : Field
    {
        public Field Quay { get; set; }

        public override string ToString()
        {
            if (this.MovableObject != null)
            {
                return MovableObject.ToString();
            }
            if (Quay == null)
            {
                return " ";
            }
            return "K";
        }
    }
}