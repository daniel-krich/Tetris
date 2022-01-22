using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisGame.Tetris.Controls
{
    using TetrisGame.Tetris.Game;
    using TetrisGame.Tetris.Display;

    /// <summary>
    /// Key press codes
    /// </summary>
    enum GameKeys
    {
        Left = 37,
        Right = 39,
        Down = 40,
        Rotate_Key = 88
    }

    class Controls
    {
        private Grid grid;
        private Display display;

        public Controls(Grid grid, Display display)
        {
            this.grid = grid;
            this.display = display;

            this.display.UpdateFrame();
        }

        /// <summary>
        /// Processes the key presses and moving/rotating the shape
        /// </summary>
        public void InputProcess()
        {
            GameKeys currentKey = (GameKeys)Console.ReadKey(false).Key;
            if (currentKey == GameKeys.Down || currentKey == GameKeys.Left || currentKey == GameKeys.Right)
            {
                this.grid.GetCurrentCube().Move(currentKey);
                this.display.UpdateFrame();
            }
            else if(currentKey == GameKeys.Rotate_Key)
            {
                this.grid.GetCurrentCube().Rotate();
                this.display.UpdateFrame();
            }
    }
    }
}
