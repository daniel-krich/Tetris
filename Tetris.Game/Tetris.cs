﻿using System;
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
        private Stats stats { get; set; }

        public void Run(int rows, int columns)
        {
            this.stats = new Stats();
            this.grid = new Grid(rows, columns, this.stats);
            this.display = new Display(this.grid, this.stats);
            this.controls = new Controls(this.grid, this.display);

            while(grid.UpdateGame())
            {
                this.controls.InputProcess();
            }

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nGame Over! you scored {0} points\n", this.stats.GetScore());
            Console.ResetColor();
        }
    }
}
