using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisGame.Tetris.Controls
{
    using TetrisGame.Tetris.Game;

    class Controls
    {
        private Grid grid;

        public Controls(Grid grid)
        {
            this.grid = grid;
            new Thread(InputProcess).Start();
        }

        private void InputProcess()
        {
            while(true)
            {
            Start:
                if ((byte)Console.ReadKey(false).Key == 40)
                {
                    for (int row = grid.GetGrid().GetLength(0)-1; row >= 0; row--)
                    {
                        for (int column = 0; column < grid.GetGrid().GetLength(1); column++)
                        {
                            if (grid.GetGrid()[row, column] == 2)
                            {
                                if (row < grid.GetGrid().GetLength(0) - 1)
                                {
                                    grid.GetGrid()[row, column] = 0;
                                    grid.GetGrid()[row + 1, column] = 2;
                                }
                                else
                                    goto Start;
                            }
                        }
                    }
                }

                if ((byte)Console.ReadKey(false).Key == 37) // left
                {
                    for (int row = 0; row < grid.GetGrid().GetLength(0); row++)
                    {
                        for (int column = 0; column < grid.GetGrid().GetLength(1); column++)
                        {
                            if (grid.GetGrid()[row, column] == 2)
                            {
                                if (column > 0)
                                {
                                    grid.GetGrid()[row, column] = 0;
                                    grid.GetGrid()[row, column - 1] = 2;
                                }
                                else
                                    goto Start;
                            }
                        }
                    }
                }

                if ((byte)Console.ReadKey(false).Key == 39) // right
                {
                    for (int row = 0; row < grid.GetGrid().GetLength(0); row++)
                    {
                        for (int column = grid.GetGrid().GetLength(1)-1; column >= 0; column--)
                        {
                            if (grid.GetGrid()[row, column] == 2)
                            {
                                if (column < grid.GetGrid().GetLength(1) - 1)
                                {
                                    grid.GetGrid()[row, column] = 0;
                                    grid.GetGrid()[row, column + 1] = 2;
                                }
                                else
                                    goto Start;
                            }
                        }
                    }
                }
            }
        }
    }
}
