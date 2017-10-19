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
            Thread thread = new Thread(PlayGame);
            thread.Start();
        }

        private void PlayGame()
        {
            while (Game.IsPlaying)
            {
                int threadTimer = 300 + (int)(300 / Math.Sqrt(Game.TotalGold + 1));

                //Moving, lockdown = true
                Game.IsLocked = true;
                OutputPrint(Game.IsLocked);

                Game.MoveAllObjects();
                Game.CheckSpawnBoat();
                Game.SpawnCarts();
                Thread.Sleep(300);

                //Not moving, lockdown = false
                Game.IsLocked = false;
                OutputPrint(Game.IsLocked);
                Thread.Sleep(threadTimer);
            }
            OutputPrint(Game.IsLocked);
            OutputView.ShowLoseMessage(Game.TotalGold);
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

            InsertWater();
            InsertUntilDivergingSwitch(Game.AFirst, 0, 4);
            InsertUntilDivergingSwitch(Game.BFirst, 0, 6);
            InsertUntilDivergingSwitch(Game.CFirst, 0, 8);

            return playingField;
        }

        private void InsertWater()
        {
            int counter = 0;
            int row = 1;

            Boat b = null;
            bool QuayFound = false;
            //River
            for (River f = (River)Game.RiverFirst; f != null; f = (River)f.Next)
            {
                if (f.MovableObject is Boat)
                {
                    b = (Boat)f.MovableObject;
                }
                else if (b != null && !QuayFound)
                {
                    if (f.Quay != null)
                    {
                        
                    }
                    else if (f.Next != null)
                    {
                        if (((River)f.Next).Quay != null)
                        {
                            f.PrintValue = ">";
                            QuayFound = true;
                        }
                        else
                        {
                            f.PrintValue = "-";
                        }
                    }
                    else
                    {
                        f.PrintValue = ">";
                    }
                }
                else
                {
                    f.PrintValue = null;
                }
                playingField[row, counter] = f.ToString();

                counter++;
            }
        }

        private void InsertUntilDivergingSwitch(Field f, int counter, int row)
        {
            for (Field r = f; r != null; r = r.Next)
            {
                playingField[row, counter] = r.ToString();

                if (row == 5 && counter == 12)
                {
                    AddQuayRow(r, counter, row);
                    break;
                }
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
                playingField[row - 1, counter++] = f.Upper.PrintValue;
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
                playingField[row + 1, counter++] = f.Lower.PrintValue;
            }

            for (Field r = f.Lower.Next; r != null; r = r.Next)
            {
                playingField[row + 1, counter] = r.ToString();

                if (row == 7 && counter == 12)
                {
                    AddShunter(r, counter, row);
                    break;
                }
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

        private void AddShunter(Field f, int counter, int row)
        {
            playingField[row + 1, counter] = ((Rail)f).ToString();
            playingField[row + 2, counter] = ((Rail)f.Next).ToString();

            //Maak de rest inc. Shunters
            counter--;
            for (Field r = f.Next.Next; r != null; r = r.Next)
            {
                playingField[row + 2, counter] = r.ToString();
                counter--;
            }
        }

        private void AddQuayRow(Field f, int counter, int row)
        {
            playingField[row, counter] = ((Rail)f).ToString();
            playingField[row - 1, counter] = ((Rail)f.Next).ToString();
            playingField[row - 2, counter] = ((Rail)f.Next.Next).ToString();
            playingField[row - 3, counter] = ((Rail)f.Next.Next.Next).ToString();

            for (Field r = f.Next.Next.Next.Next; r != null; r = r.Next)
            {
                playingField[row - 3, counter - 1] = ((Rail)r).ToString();
                counter--;
            }
        }

        public void OutputPrint(bool OnLockdown)
        {
            OutputView.PrintGame(GetPlayingField(), Game.TotalGold, OnLockdown);
        }
    }
}