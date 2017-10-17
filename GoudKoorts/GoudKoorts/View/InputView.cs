using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class InputView
    {
        public GameController GameController { get; set; } = new GameController();

        public InputView()
        {
            Console.SetWindowSize(500,500);
            
        }
    }
}