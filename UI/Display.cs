using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Enums;
using TetrisGame.ILogic;
using TetrisGame.Logic;
using TetrisGame.Models;

namespace TetrisGame.UI
{
    class Display : IDisplay
    {
        private IGrid _grid;
        private IStatsModel _statsModel;

        public Display(IGrid grid, IStatsModel statsModel)
        {
            _grid = grid;
            _statsModel = statsModel;
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
            for (int row = 0; row < _grid.CurrentGrid.GetLength(0); row++)
            {
                for (int column = 0; column < _grid.CurrentGrid.GetLength(1); column++)
                {
                    if (_grid.CurrentGrid[row, column] != CellType.None)
                    {
                        Console.BackgroundColor = _statsModel.LevelColor;
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
        private void DisplayControls()
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
        private void DisplayScore()
        {
            Console.BackgroundColor = _statsModel.LevelColor;
            Console.ForegroundColor = ConsoleColor.Black;
            string level = "Level: " + _statsModel.LevelName;
            Console.Write(level);

            for (int i = 0; i < _grid.CurrentGrid.GetLength(1) * 2 - level.Length; i++)
            {
                Console.Write("\x20");
            }
            Console.WriteLine();

            //
            string score = "Score: " + _statsModel.Score + " points";
            Console.Write(score);

            for (int i = 0; i < _grid.CurrentGrid.GetLength(1) * 2 - score.Length; i++)
            {
                Console.Write("\x20");
            }
            Console.WriteLine();

            Console.ResetColor();
        }
    }
}
