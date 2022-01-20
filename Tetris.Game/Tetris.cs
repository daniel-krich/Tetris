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

        public void Run(int rows, int columns)
        {
            this.grid = new Grid(rows, columns);
            this.display = new Display(this.grid);
            this.controls = new Controls(this.grid, this.display);

            while(grid.UpdateGame())
            {
                Thread.Sleep(1);
            }

            this.controls.isFinished = true;

        WaitForThreadToFinish:
            if (this.controls.currentThread.IsAlive)
                goto WaitForThreadToFinish;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("Game is finished...");
        }
    }
}
