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
            DisplayControls();
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

        public void DisplayControls()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Use \"Left\", \"Right\", \"Down\" buttons to control the shape.");
            Console.WriteLine("Use \"X\" to flip the shape\n");
            Console.ResetColor();
        }
    }
}
