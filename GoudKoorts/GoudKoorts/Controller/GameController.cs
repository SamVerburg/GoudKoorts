using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoudKoorts
{
    public class GameController
    {
        public OutputView OutputView { get; set; } = new OutputView();

        public Game Game { get; set; } = new Game();

    }
}