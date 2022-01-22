using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

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

    enum VirtualKeyCodes
    {
        VK_DOWN	 = 0x28,
        VK_LEFT	 = 0x25,
        VK_RIGHT = 0x27,
        X_KEY    = 0x58
    }

    class Controls
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int nVirtKey);

        private Grid grid;
        private Display display;
        private Stats stats;

        private long PressDelay;
        private long FallDelay;

        public Controls(Grid grid, Display display, Stats stats)
        {
            this.grid = grid;
            this.display = display;
            this.stats = stats;

            this.display.UpdateFrame();
        }

        /// <summary>
        /// Processes the key presses and moving/rotating the shape
        /// </summary>
        public void InputProcess()
        {
            if (PressDelay < DateTime.Now.Ticks)
            {
                if ((GetKeyState((int)VirtualKeyCodes.VK_DOWN) & 0x8000) != 0)
                {
                    this.grid.GetCurrentCube().Move(GameKeys.Down);
                    this.display.UpdateFrame();
                    PressDelay = DateTime.Now.Ticks + 10000 * 50;
                }
                else if ((GetKeyState((int)VirtualKeyCodes.VK_RIGHT) & 0x8000) != 0)
                {
                    this.grid.GetCurrentCube().Move(GameKeys.Right);
                    this.display.UpdateFrame();
                    PressDelay = DateTime.Now.Ticks + 10000 * 50;
                }
                else if ((GetKeyState((int)VirtualKeyCodes.VK_LEFT) & 0x8000) != 0)
                {
                    this.grid.GetCurrentCube().Move(GameKeys.Left);
                    this.display.UpdateFrame();
                    PressDelay = DateTime.Now.Ticks + 10000 * 50;
                }
                else if ((GetKeyState((int)VirtualKeyCodes.X_KEY) & 0x8000) != 0)
                {
                    this.grid.GetCurrentCube().Rotate();
                    this.display.UpdateFrame();
                    PressDelay = DateTime.Now.Ticks + 10000 * 50;
                }
            }
            if(FallDelay < DateTime.Now.Ticks)
            {
                this.grid.GetCurrentCube().Move(GameKeys.Down);
                this.display.UpdateFrame();
                FallDelay = DateTime.Now.Ticks + 10000 * this.stats.DelayLevel;
            }
        }
    }
}
