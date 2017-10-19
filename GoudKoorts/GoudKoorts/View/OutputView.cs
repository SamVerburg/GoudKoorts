using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GoudKoorts
{
    public class OutputView
    {

        public void PrintGame(string[,] playingField, int totalGold, Boolean OnLockdown)
        {

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;

            //WATER

            bool after = false;
            if (playingField[1, 11].Equals("B") || playingField[1, 12].Equals("B"))
            {
                after = true;
            }

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < playingField.GetLength(1); y++)
                {
                    Console.Write(playingField[x, y]);
                }
                Console.WriteLine();
            }

            Console.BackgroundColor = ConsoleColor.Black;

            //TRACK
            for (int x = 2; x < playingField.GetLength(0); x++)
            {
                for (int y = 0; y < playingField.GetLength(1); y++)
                {
                    if (x == 9 && y <= 9)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    else if (x == 5 && y == 4 || x == 5 && y == 10 || x == 5 && y == 6 || x == 7 && y == 9 || x == 7 && y == 7)
                    {
                        if (OnLockdown)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(playingField[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Goud: " + totalGold);
            Console.WriteLine("Knoppen: 1,2,3,4,5");
        }

        public void ShowLoseMessage(int TotalGold)
        {
            Console.WriteLine("╔════════╗");
            Console.WriteLine("║VERLOREN║");
            Console.WriteLine("╚════════╝");

            Console.ReadLine();
        }
    }
}