using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;

namespace GoudKoorts
{
    public class GameController
    {
        private string[,] playingField = new string[11, 14];

        public OutputView OutputView { get; set; } = new OutputView();

        public Game Game { get; set; } = new Game();

        public GameController()
        {
            while (true)
            {
                Thread.Sleep(500);
                Game.MoveAllObjects();
                OutputView.PrintGame(GetPlayingField());
            }
        }

        private string[,] GetPlayingField()
        {
            for (int x = 0; x < playingField.GetLength(0); x++)
            {
                for (int y = 0; y < playingField.GetLength(1); y++)
                {
                    playingField[x, y] = " ";
                }
            }

            int counter = 0;
            int row = 1;

            //River
            for (Field f = Game.RiverFirst; f != null; f = f.Next)
            {
                playingField[row, counter] = f.ToString();
                counter++;
            }

            InsertUntilDivergingSwitch(Game.AFirst, 0, 5);
            InsertUntilDivergingSwitch(Game.BFirst, 0, 7);
            InsertUntilDivergingSwitch(Game.CFirst, 0, 9);
            
            return playingField;
        }



        private void InsertUntilDivergingSwitch(Field f, int counter, int row)
        {
            for (Field r = f; r != null; r = r.Next)
            {
                playingField[row, counter] = r.ToString();
                counter++;

                if (counter > 12)
                {
                    break;
                }

                if (!(r.Next is Switch)) continue;

                Switch s = ((Switch)r.Next);
                if (s.isConverging())
                {
                    if (s.Upper == r)
                    {
                        row++;
                    }
                    else
                    {
                        row--;
                    }
                    counter--;
                }
                else
                {
                  
                    InsertUntilConvergingSwitch(s, counter, row);
                    break;
                }
            }
        }

        private void InsertUntilConvergingSwitch(Switch f, int counter, int row)
        {
            //DRAWSWITCH
            playingField[row, counter] = f.ToString();

            //UpperSwitch
            Switch temp = null;

            //UPPER
            int oldCounter = counter;
            if (f.Upper.MovableObject != null)
            {
                playingField[row - 1, counter++] = f.Upper.ToString();
            }
            else
            {
                playingField[row - 1, counter++] = f.Upper.printValue;
            }

            for (Field r = f.Upper.Next; r != null; r = r.Next)
            {
                playingField[row - 1, counter] = r.ToString();

                counter++;
                if (counter > 12)
                {
                    break;
                }

                if (r.Next is Switch)
                {
                    temp = (Switch)r.Next;
                    break;
                }
            }

            //LOWER
            counter = oldCounter;

            if (f.Lower.MovableObject != null)
            {
                playingField[row + 1, counter++] = f.Lower.ToString();
            }
            else
            {
                playingField[row + 1, counter++] = f.Lower.printValue;
            }

            for (Field r = f.Lower.Next; r != null; r = r.Next)
            {
                playingField[row + 1, counter] = r.ToString();

                counter++;
                if (counter > 12)
                {
                    break;
                }

                if (r.Next is Switch)
                {
                    InsertUntilDivergingSwitch(temp, counter + 2, row);
                    break;
                }
            }
        }
        private void AddShunter()
        {
            
        }
    }
}