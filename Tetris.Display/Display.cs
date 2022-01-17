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

        public Display(Grid grid)
        {
            this.grid = grid;
        }

        public void UpdateFrame()
        {
            Console.Clear();
            for (int row = 0; row < grid.GetGrid().GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetGrid().GetLength(1); column++)
                {
                    if (grid.GetGrid()[row, column] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("\x20\x20");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.Write("\x20\x20");
                    }
                    Console.ResetColor();
                }
                Console.Write("\n");
            }
        }
    }
}
