using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Tetris.Main
{
    using TetrisGame.Tetris.Game;
    class RunGame
    {
        static void Main()
        {
            Tetris t = new Tetris();
            t.Run();
        }
    }
}
