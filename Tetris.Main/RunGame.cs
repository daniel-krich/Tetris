using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisGame.Tetris.Main
{
    using TetrisGame.Tetris.Game;
    class RunGame
    {
        static void Main()
        {
            Tetris t = new Tetris();
            t.Run(20, 10);

            while (true)
                Thread.Sleep(1000);
        }
    }
}
