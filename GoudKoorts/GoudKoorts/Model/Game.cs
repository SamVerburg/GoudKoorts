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

        public Switch[] Switches { get; set; } = new Switch[5];

        public bool IsLocked { get; set; }

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
            Objects.Insert(0, boat);
        }

        private Field MakeMultipleLinks(Field field, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (field is River)
                {
                    field.Next = new River();
                    field = (River)field.Next;
                }
                else if (field is Rail)
                {
                    field.Next = new Rail();
                    field = (Rail)field.Next;
                }
                else if (field is Shunter)
                {
                    field.Next = new Shunter();
                    field = (Shunter)field.Next;
                }
            }
            return field;
        }

        public void MoveAllObjects()
        {
            foreach (var o in Objects)
            {
                o.Move();
                if (o is Cart)
                {
                    CheckCrashed((Cart)o);
                }
            }
            UpdateTotalGold();
        }

        private bool CheckCrashed(Cart o)
        {
            if (((Cart)o).HasCrashed)
            {
                IsPlaying = false;
                return true;
            }
            return false;
        }

        public void SwitchSwitch(int nr)
        {
            Switches[nr].SwitchState();
        }

        public void CheckSpawnBoat()
        {
            foreach (var o in Objects)
            {
                if (o is Boat && o.Field != null)
                {
                    Boat b = (Boat)o;
                    if (((River)b.Field).Quay == null) return;
                    if (b.Load == 8 && ((River)b.Field).MovableObject == b)
                    {
                        Boat boat = new Boat() { Field = RiverFirst };
                        RiverFirst.MovableObject = boat;
                        Objects.Insert(0, boat);
                        return;
                    }
                }
            }
        }

        private void UpdateTotalGold()
        {
            int totalGold = 0;
            foreach (var o in Objects)
            {
                if (o is Boat)
                {
                    Boat b = (Boat)o;
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

            if (r.Next(10) < 1)
            {
                Cart a = new Cart() { Field = AFirst.Next };
                AFirst.Next.MovableObject = a;
                Objects.Add(a);
            }

            if (r.Next(10) < 1)
            {
                Cart b = new Cart() { Field = BFirst.Next };
                BFirst.Next.MovableObject = b;
                Objects.Add(b);
            }

            if (r.Next(10) < 1)
            {
                Cart c = new Cart() { Field = CFirst.Next };
                CFirst.Next.MovableObject = c;
                Objects.Add(c);
            }
        }

        public void CreateGame()
        {
            //aanmaken van rivier
            RiverFirst = new River();
            River currentR = (River)RiverFirst;

            currentR = (River)MakeMultipleLinks(currentR, 10);

            River quayField = currentR;

            currentR.Next = new River();
            currentR = (River)currentR.Next;
            currentR.Next = new River();

            AFirst = new Rail();
            Field currentA = AFirst;

            currentA = MakeMultipleLinks(currentA, 4);

            BFirst = new Rail();
            Field currentB = BFirst;

            currentB = MakeMultipleLinks(currentB, 4);

            CFirst = new Rail();
            Field currentC = CFirst;

            currentC = MakeMultipleLinks(currentC, 7);

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

            currentD = MakeMultipleLinks(currentD, 4);

            Switch switch5 = new Switch { State = State.FROMUPPER, Upper = (Rail)currentD, Lower = (Rail)switch4.Upper.Next, Next = new Rail() };
            currentD.Next = switch5;
            switch4.Upper.Next.Next = switch5;

            Field currentE = switch5.Next;
            //Rails vanaf switch 5.next tot kade
            currentE = MakeMultipleLinks(currentE, 6);

            //Kade wordt gedefinieert
            quayField.Quay = currentE;

            //Rest van de 9 rails na kade
            currentE = MakeMultipleLinks(currentE, 10);

            Field CurrentF = switch4.Lower;

            CurrentF = MakeMultipleLinks(CurrentF, 6);

            CurrentF.Next = new Shunter();
            CurrentF = CurrentF.Next;

            CurrentF = MakeMultipleLinks(CurrentF, 7);

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

            ((Rail)AFirst).printValue = "A";
            ((Rail)BFirst).printValue = "B";
            ((Rail)CFirst).printValue = "C";

            Switches[0] = switch1;
            Switches[1] = switch2;
            Switches[2] = switch3;
            Switches[3] = switch4;
            Switches[4] = switch5;
        }
    }
}