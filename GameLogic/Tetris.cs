using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TetrisGame.UI;
using TetrisGame.Models;

namespace TetrisGame.GameLogic
{

    public class Tetris : ITetris
    {
        private IGrid _grid;
        private IControls _controls;
        private IDisplay _display;
        private IStatsModel _statsModel;

        public int Rows { get; set; } = 20;
        public int Columns { get; set; } = 10;

        public Tetris(IGrid grid, IControls controls, IDisplay display, IStatsModel statsModel)
        {
            _grid = grid;
            _controls = controls;
            _display = display;
            _statsModel = statsModel;
        }

        /// <summary>
        /// Main function to run the game fully.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public void Run()
        {
            _grid.CreateGridTable(Rows, Columns);

            _display.UpdateFrame();

            while (_grid.UpdateGame())
            {
                _controls.InputProcess();
            }

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nGame Over, structure got too high! you scored {0} points\n", _statsModel.Score);
            Console.ResetColor();
        }
    }
}
