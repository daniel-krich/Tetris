using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Tetris.Display
{
    using TetrisGame.Tetris.Game;
    class Display
    {
        private Grid grid;
        private Stats stats;

        public Display(Grid grid, Stats stats)
        {
            this.grid = grid;
            this.stats = stats;
        }

        /// <summary>
        /// Updates the frame of the console to the current 2D martix of the grid and the cubes.
        /// Updates the score and controls.
        /// </summary>
        public void UpdateFrame()
        {
            Console.Clear();
            DisplayControls();
            DisplayScore();
            //
            for (int row = 0; row < grid.GetGrid().GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetGrid().GetLength(1); column++)
                {
                    if (grid.GetGrid()[row, column] == CellType.FixedCell)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write("\x20\x20");
                    }
                    else if (grid.GetGrid()[row, column] == CellType.PlayerCell)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write("\x20\x20");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("\x20\x20");
                    }
                    Console.ResetColor();
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Displays the controls to the console
        /// </summary>
        public void DisplayControls()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Use \"Left\", \"Right\", \"Down\" buttons to control the shape.");
            Console.WriteLine("Use \"X\" to flip the shape\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the current score to the console
        /// </summary>
        public void DisplayScore()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            string score = "Score: "+ this.stats.GetScore() + " points";

            Console.Write(score);

            for(int i = 0; i < this.grid.GetGrid().GetLength(1)*2 - score.Length; i++)
            {
                Console.Write("\x20");
            }
            Console.WriteLine();

            Console.ResetColor();
        }
    }
}
