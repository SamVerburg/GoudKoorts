using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class Game
    {
        public List<MovableObject> Objects { get; set; } = new List<MovableObject>();

        public Field AFirst { get; set; }

        public Field BFirst { get; set; }

        public Field CFirst { get; set; }

        public Field RiverFirst { get; set; }

        public void Start()
        {
            
        }

        public void CreateGame()
        {
            //aanmaken van alle lijsten
        }
    }
}