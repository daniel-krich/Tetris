using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Enums
{
    /// <summary>
    /// Key press codes
    /// </summary>
    public enum VirtualKeyCodes
    {
        VK_DOWN = 0x28,
        VK_LEFT = 0x25,
        VK_RIGHT = 0x27,
        X_KEY = 0x58
    }

    /// <summary>
    /// Cube type enum
    /// </summary>
    public enum CubeType
    {
        l_shape,
        J_shape,
        L_shape,
        O_shape,
        Z_shape,
        T_shape,
        S_shape,
    }

    public enum CellType
    {
        None,
        FixedCell,
        PlayerCell
    }
}
