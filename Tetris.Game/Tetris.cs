using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisGame.Tetris.Game
{
    using TetrisGame.Tetris.Display;
    using TetrisGame.Tetris.Controls;

    class Tetris
    {
        private Grid grid { get; set; }
        private Display display { get; set; }
        private Controls controls { get; set; }

        public void Run()
        {
            this.grid = new Grid();
            this.display = new Display(this.grid);
            this.controls = new Controls(this.grid);

            while(grid.UpdateGame())
            {
                this.display.UpdateFrame();
            }
        }
    }
}
