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

        public void CreateGame()
        {
            //aanmaken van rivier
            RiverFirst = new River();
            River currentR = (River)RiverFirst;

            for (int i = 0; i < 10; i++)
            {
                currentR.Next = new River();
                currentR = (River)currentR.Next;
            }

            River quayField = currentR;

            currentR.Next = new River();
            currentR = (River)currentR.Next;
            currentR.Next = new River();

            //aanmaken van rails
            AFirst = new Rail();
            BFirst = new Rail();

            Field currentA = AFirst;
            Field currentB = BFirst;

            for (int i = 0; i < 3; i++)
            {
                currentA.Next = new Rail();
                currentA = currentA.Next;
                currentB.Next = new Rail();
                currentB = currentB.Next;
            }

            Switch switch1 = new Switch
            {
                Lower = currentB,
                Upper = currentA,
                State = State.FROMLOWER
            };

            currentB.Next = switch1;
            switch1.Next = new Rail();

            Switch switch2 = new Switch
            {
                Upper = new Rail(),
                Lower = new Rail(),
                State = State.TOUPPER
            };

            switch1.Next.Next = switch2;
            currentA = switch2.Upper;

            for (int i = 0; i < 4; i++)
            {
                currentA.Next = new Rail();
                currentA = currentA.Next;
            }

            switch2.Lower.Next = new Rail();

            CFirst = new Rail();
            Field currentC = CFirst;

            for (int i = 0; i < 6; i++)
            {
                currentC.Next = new Rail();
                currentC = currentC.Next;
            }

            Switch switch3 = new Switch()
            {
                Upper = switch2.Lower.Next,
                Lower = currentC,
                State = State.TOLOWER
            };

            Switch switch4 = new Switch()
            {
                State = State.TOLOWER,
                Upper = new Rail(),
                Lower = new Rail(),
            };

            switch3.Next = new Rail { Next = switch4 };

            for (int i = 0; i < 6; i++)
            {
                currentC.Next = new Rail();
                currentC = currentC.Next;
            }

            for (int i = 0; i < 8; i++)
            {
                currentC.Next = new Shunter();
                currentC = currentC.Next;
            }

            switch4.Upper.Next = new Rail();

            Switch switch5 = new Switch
            {
                Lower = switch4.Upper.Next,
                Upper = currentA,
                State = State.FROMUPPER
            };

            currentB = switch5;
            for (int i = 0; i < 7; i++)
            {
                currentB.Next = new Rail();
                currentB = currentB.Next;
            }

            quayField.Quay = currentB;

            for (int i = 0; i < 9; i++)
            {
                currentB.Next = new Rail();
                currentB = currentB.Next;
            }
        }
    }
}