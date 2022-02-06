using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TetrisGame.UI;
using TetrisGame.Models;
using TetrisGame.ILogic;

namespace TetrisGame.Logic
{

    public class Tetris : ITetris
    {
        private IGrid _grid;
        private IControls _controls;
        private IDisplay _display;
        private IStatsModel _statsModel;

        public Tetris(IGrid grid, IControls controls, IDisplay display, IStatsModel statsModel)
        {
            _grid = grid;
            _controls = controls;
            _display = display;
            _statsModel = statsModel;
        }

        /// <summary>
        /// Run the game.
        /// </summary>
        /// <param name="rows">Game height</param>
        /// <param name="columns">Game width</param>
        public void Run(int rows, int columns)
        {
            _grid.CreateGridTable(rows, columns);

            _display.UpdateFrame();

            while (_grid.UpdateGame())
            {
                _controls.InputProcess();
            }

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nGame Over! you scored {0} points\n", _statsModel.Score);
            Console.ResetColor();
        }
    }
}
