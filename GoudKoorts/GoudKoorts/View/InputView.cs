using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GoudKoorts
{
    public class InputView
    {
        public GameController GameController { get; set; } = new GameController();

        public InputView()
        {
            Thread t = new Thread(GetInputs);
            t.Start();
        }

        private void GetInputs()
        {
            while (true)
            {
                GetPlayerInput();
                GameController.OutputPrint();
            }
        }

        private void GetPlayerInput()
        {
            var ch = Console.ReadKey(false).Key;
            switch (ch)
            {
                case ConsoleKey.D1:
                    GameController.Game.SwitchSwitch(0);
                    break;
                case ConsoleKey.D2:
                    GameController.Game.SwitchSwitch(1);
                    break;
                case ConsoleKey.D3:
                    GameController.Game.SwitchSwitch(2);
                    break;
                case ConsoleKey.D4:
                    GameController.Game.SwitchSwitch(3);
                    break;
                case ConsoleKey.D5:
                    GameController.Game.SwitchSwitch(4);
                    break;
            }
        }
    }
}