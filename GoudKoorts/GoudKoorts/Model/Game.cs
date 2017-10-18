using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Timer = System.Threading.Timer;

namespace GoudKoorts
{
    public class Game
    {
        public List<MovableObject> Objects { get; set; } = new List<MovableObject>();
        public bool IsPlaying { get; set; } = true;

        public Field AFirst { get; set; }

        public Field BFirst { get; set; }

        public Field CFirst { get; set; }

        public Field RiverFirst { get; set; }

        public int TotalGold { get; set; }

        public Game()
        {
            CreateGame();
            Boat boat = new Boat() { Field = RiverFirst };
            RiverFirst.MovableObject = boat;
            Objects.Add(boat);
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

            AFirst = new Rail();
            Field currentA = AFirst;

            for (int i = 0; i < 4; i++)
            {
                currentA.Next = new Rail();
                currentA = currentA.Next;
            }

            BFirst = new Rail();
            Field currentB = BFirst;

            for (int i = 0; i < 4; i++)
            {
                currentB.Next = new Rail();
                currentB = currentB.Next;
            }

            CFirst = new Rail();
            Field currentC = CFirst;

            for (int i = 0; i < 7; i++)
            {
                currentC.Next = new Rail();
                currentC = currentC.Next;
            }

            Switch switch1 = new Switch { State = State.FROMLOWER, Upper = (Rail)currentA, Lower = (Rail)currentB, Next = new Rail() };
            currentA.Next = switch1;
            currentB.Next = switch1;

            Switch switch2 = new Switch { State = State.TOUPPER, Upper = new Rail(), Lower = new Rail() };
            switch2.Next = switch2.Upper;

            switch1.Next.Next = switch2;
            switch2.Lower.Next = new Rail();

            Switch switch3 = new Switch { State = State.FROMLOWER, Upper = (Rail)switch2.Lower.Next, Lower = (Rail)currentC, Next = new Rail() };
            switch2.Lower.Next.Next = switch3;
            currentC.Next = switch3;

            Switch switch4 = new Switch { State = State.TOLOWER, Upper = new Rail(), Lower = new Rail() };
            switch4.Next = switch4.Lower;
            switch4.Upper.Next = new Rail();

            switch3.Next.Next = switch4;

            Field currentD = switch2.Upper;

            for (int i = 0; i < 4; i++)
            {
                currentD.Next = new Rail();
                currentD = currentD.Next;
            }

            Switch switch5 = new Switch { State = State.FROMUPPER, Upper = (Rail)currentD, Lower = (Rail)switch4.Upper.Next, Next = new Rail() };
            currentD.Next = switch5;
            switch4.Upper.Next.Next = switch5;

            Field currentE = switch5.Next;
            //Rails vanaf switch 5.next tot kade
            for (int i = 0; i < 7; i++)
            {
                currentE.Next = new Rail();
                currentE = currentE.Next;
            }

            //Kade wordt gedefinieert
            quayField.Quay = currentE;

            //Rest van de 9 rails na kade
            for (int i = 0; i < 9; i++)
            {
                currentE.Next = new Rail();
                currentE = currentE.Next;
            }

            Field CurrentF = switch4.Lower;
            for (int i = 0; i < 6; i++)
            {
                CurrentF.Next = new Rail();
                CurrentF = CurrentF.Next;
            }

            for (int i = 0; i < 8; i++)
            {
                CurrentF.Next = new Shunter();
                CurrentF = CurrentF.Next;
            }

            switch2.Upper.printValue = "╔";
            switch2.Lower.printValue = "╚";
            switch4.Upper.printValue = "╔";
            switch4.Lower.printValue = "╚";
            ((Rail)switch4.Lower.Next.Next.Next).printValue = "╗";
            ((Rail)switch4.Lower.Next.Next.Next.Next).printValue = "╝";

            //6-12 en 3-12
            ((Rail)switch5.Next.Next).printValue = "╝";
            ((Rail)switch5.Next.Next.Next).printValue = "║";
            ((Rail)switch5.Next.Next.Next.Next).printValue = "║";
            ((Rail)switch5.Next.Next.Next.Next.Next).printValue = "╗";
        }

        public void MoveAllObjects()
        {
            foreach (var o in Objects)
            {
                o.Move();
                if (o is Cart)
                {
                    if (((Cart) o).HasCrashed)
                    {
                        IsPlaying = false;
                        return;
                    }
                }
            }
            UpdateTotalGold();
        }

        private void UpdateTotalGold()
        {
            int totalGold = 0;
            foreach (var o in Objects)
            {
                if (o is Boat)
                {
                    Boat b = (Boat) o;
                    totalGold += b.Load;
                    if (b.Load == 8)
                    {
                        totalGold += 10;
                    }
                }
            }
            this.TotalGold = totalGold;
        }

        public void SpawnCarts()
        {
            Random r = new Random();

            /*if (r.Next(5) < 1)
            {
                Cart a = new Cart() { Field = AFirst };
                AFirst.MovableObject = a;
                Objects.Add(a);
            }*/

            if (r.Next(5) < 1)
            {
                Cart b = new Cart() { Field = BFirst };
                BFirst.MovableObject = b;
                Objects.Add(b);
            }

            if (r.Next(5) < 1)
            {
                Cart c = new Cart() { Field = CFirst };
                CFirst.MovableObject = c;
                Objects.Add(c);
            }
        }
    }
}