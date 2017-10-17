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

        public Game()
        {
            CreateGame();
        }

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
                Upper = (Rail)currentA,
                Lower = (Rail)currentB,
                State = State.FROMLOWER
            };

            currentA.Next = switch1;
            currentB.Next = switch1;
            switch1.Next = new Rail();

            Switch switch2 = new Switch
            {
                Upper = new Rail(),
                Lower = new Rail(),
                State = State.TOUPPER
            };

            switch2.Upper.printValue = "╔";
            switch2.Lower.printValue = "╚";
            switch2.Next = switch2.Upper;
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
                Upper = (Rail)switch2.Lower.Next,
                Lower = (Rail)currentC,
                State = State.FROMLOWER
            };

            currentC.Next = switch3;
            switch2.Lower.Next.Next = switch3;

            Switch switch4 = new Switch()
            {
                State = State.TOLOWER,
                Upper = new Rail(),
                Lower = new Rail(),
            };

            switch3.Next = new Rail { Next = switch4 };
            currentC = switch4.Lower;
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

            switch4.Upper.printValue = "╔";
            switch4.Lower.printValue = "╚";

            Switch switch5 = new Switch
            {
                Lower = (Rail)switch4.Upper.Next,
                Upper = (Rail)currentA,
                State = State.FROMUPPER
            };

            switch5.Upper.Next = switch5;
            switch5.Lower.Next = switch5;
            
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