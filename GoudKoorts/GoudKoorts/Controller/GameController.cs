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
            // printMatrix();
            return playingField;
        }



        private void InsertUntilDivergingSwitch(Field f, int counter, int row)
        {
            for (Field r = f; r != null; r = r.Next)
            {
                playingField[row, counter] = r.ToString();

                if (row == 6 && counter == 12)
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


                if (row == 8 && counter == 12)
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
            //Komt hierin wanneer row 9 , en counter 12 is, en tekent de bochten op plaats 9-12 en 10-12

            if (f.MovableObject != null)
            {
                playingField[row + 1, counter] = ((Rail)f).ToString();
            }
            else
            {
                playingField[row + 1, counter] = ((Rail)f).printValue;

            }
            if (f.Next.MovableObject != null)
            {
                playingField[row + 2, counter] = ((Rail)f.Next).ToString();

            }
            else
            {
                playingField[row + 2, counter] = ((Rail)f.Next).printValue;

            }

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
            //Netter maken ~~! 

            if (hutnerf.MovableObject != null)
            {
                playingField[row, counter] = ((Rail)f).ToString();
            }
            
            else
            {
                playingField[row, counter] = ((Rail)f).printValue;

            }
            if (f.Next.MovableObject != null)
            {
                playingField[row -1, counter] = ((Rail)f.Next).ToString();
            }

            else
            {
                playingField[row -1, counter] = ((Rail)f.Next).printValue;

            }
            if (f.Next.Next.MovableObject != null)
            {
                playingField[row -2 , counter] = ((Rail)f.Next.Next).ToString();
            }

            else
            {
                playingField[row -2, counter] = ((Rail)f.Next.Next).printValue;

            }
            if (f.Next.Next.Next.MovableObject != null)
            {
                playingField[row -3, counter] = ((Rail)f.Next.Next.Next).ToString();
            }

            else
            {
                playingField[row -3, counter] = ((Rail)f.Next.Next.Next).printValue;

            }
            for (Field r = f.Next.Next.Next.Next; r != null; r = r.Next)
            {

                playingField[row - 3, counter-1] = ((Rail)r).ToString();
                counter--;
            }
        }

        private void printMatrix()
        {
            int rowLength = playingField.GetLength(0);
            int colLength = playingField.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", playingField[i, j] + " " + i + " " + j));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ReadLine();
        }
    }
}