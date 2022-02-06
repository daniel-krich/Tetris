using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using TetrisGame.Enums;
using TetrisGame.Models;
using TetrisGame.UI;
using TetrisGame.Core;
using TetrisGame.ILogic;

namespace TetrisGame.Logic
{
    public class Controls : IControls
    {

        private IGrid _grid;
        private IDisplay _display;
        private IStatsModel _statsModel;

        private Dictionary<VirtualKeyCodes, bool> _pressMap;
        private long FallDelay;

        public Controls(IGrid grid, IDisplay display, IStatsModel statsModel)
        {
            _grid = grid;
            _display = display;
            _statsModel = statsModel;

            _pressMap = new Dictionary<VirtualKeyCodes, bool>() {
                { VirtualKeyCodes.VK_DOWN, false },
                { VirtualKeyCodes.VK_RIGHT, false },
                { VirtualKeyCodes.VK_LEFT, false },
                { VirtualKeyCodes.X_KEY, false },
            };
        }

        /// <summary>
        /// Processes the key presses and moving/rotating the shape
        /// </summary>
        public void InputProcess()
        {
            foreach (KeyValuePair<VirtualKeyCodes, bool> entry in _pressMap)
            {
                if (Utils.GetKeyState((int)entry.Key) < 0 && !_pressMap[entry.Key])
                {
                    if (entry.Key != VirtualKeyCodes.X_KEY)
                        _grid.MoveCube(entry.Key);
                    else
                        _grid.RotateCube();

                    _display.UpdateFrame();
                    _pressMap[entry.Key] = true;
                }
                else if (Utils.GetKeyState((int)entry.Key) >= 0 && _pressMap[entry.Key])
                {
                    _pressMap[entry.Key] = false;
                }
            }
            if (FallDelay < DateTime.Now.Ticks)
            {
                _grid.MoveCube(VirtualKeyCodes.VK_DOWN);
                _display.UpdateFrame();
                FallDelay = DateTime.Now.Ticks + 10000 * _statsModel.DelayLevel;
            }
        }
    }
}
