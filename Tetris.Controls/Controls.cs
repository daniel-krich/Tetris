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

        private Dictionary<VirtualKeyCodes, bool> PressMap;
        private long FallDelay;

        public Controls(Grid grid, Display display, Stats stats)
        {
            this.grid = grid;
            this.display = display;
            this.stats = stats;

            this.PressMap = new Dictionary<VirtualKeyCodes, bool>() {
                { VirtualKeyCodes.VK_DOWN, false },
                { VirtualKeyCodes.VK_RIGHT, false },
                { VirtualKeyCodes.VK_LEFT, false },
                { VirtualKeyCodes.X_KEY, false },
            };
            this.display.UpdateFrame();
        }

        /// <summary>
        /// Processes the key presses and moving/rotating the shape
        /// </summary>
        public void InputProcess()
        {
            foreach (KeyValuePair<VirtualKeyCodes, bool> entry in this.PressMap)
            {
                if (GetKeyState((int)entry.Key) < 0 && !this.PressMap[entry.Key])
                {
                    if (entry.Key != VirtualKeyCodes.X_KEY)
                        this.grid.GetCurrentCube().Move(entry.Key);
                    else
                        this.grid.GetCurrentCube().Rotate();

                    this.display.UpdateFrame();
                    this.PressMap[entry.Key] = true;
                }
                else if(GetKeyState((int)entry.Key) >= 0 && this.PressMap[entry.Key])
                {
                    this.PressMap[entry.Key] = false;
                }
            }
            if(FallDelay < DateTime.Now.Ticks)
            {
                this.grid.GetCurrentCube().Move(VirtualKeyCodes.VK_DOWN);
                this.display.UpdateFrame();
                FallDelay = DateTime.Now.Ticks + 10000 * this.stats.DelayLevel;
            }
        }
    }
}
