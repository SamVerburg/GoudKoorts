using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class OutputView
    {
        public void PrintGame(string[,] playingField)
        {
            
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int x = 0; x < playingField.GetLength(0); x++)
            {
                if (x == 2)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                for (int y = 0; y < playingField.GetLength(1); y++)
                {
                    Console.Write(playingField[x, y]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

    }
}