using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Tetris.Game
{
    class Grid
    {
        private int[,] gridTable { get; set; }

        private Cube currentCube { get; set; }

        public Grid()
        {
            this.gridTable = new int[20, 10];
            this.currentCube = new Cube();
            IntergrateCube(this.currentCube);
        }

        public void IntergrateCube(Cube cube)
        {
            for (int row = 0; row < cube.GetCube().GetLength(0); row++)
            {
                for (int column = 0; column < cube.GetCube().GetLength(1); column++)
                {
                    if (cube.GetCube()[row, column] != 0)
                        this.gridTable[row, column + gridTable.GetLength(1) / 2] = cube.GetCube()[row, column];
                }
            }
        }

        public bool UpdateGame()
        {
            if (isGameFinished())
                return false;
            return true;
        }

        public bool isGameFinished()
        {
            for (int column = 0; column < this.gridTable.GetLength(1); column++)
            {
                if (this.gridTable[0, column] == 1)
                    return true;
            }

            return false;
        }

        public int[,] GetGrid() => this.gridTable;

    }
}
