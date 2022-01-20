using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Tetris.Game
{
    enum CellType
    {
        None,
        FixedCell,
        PlayerCell
    }

    class Grid
    {
        private CellType[,] gridTable { get; set; }

        private Cube currentCube { get; set; }

        public Grid(int rows, int columns)
        {
            this.gridTable = new CellType[rows, columns];
            this.currentCube = new Cube(this, this.gridTable.GetLength(0), this.gridTable.GetLength(1));
            //IntergrateCube(this.currentCube);
            if(isMergeSafe(this.currentCube))
            {
                Merge(this.currentCube);
            }
        }

        public void IntergrateCube(Cube cube)
        {
            for (int row = 0; row < cube.GetCube().GetLength(0); row++)
            {
                for (int column = 0; column < cube.GetCube().GetLength(1); column++)
                {
                    if (cube.GetCube()[row, column] != CellType.None)
                    {
                        this.gridTable[row, column + gridTable.GetLength(1) / 2] = cube.GetCube()[row, column];
                    }
                }
            }
        }

        public void ClearPlayerCubeFromGrid()
        {
            for (int row = 0; row < this.gridTable.GetLength(0); row++)
            {
                for (int column = 0; column < this.gridTable.GetLength(1); column++)
                {
                    if (this.gridTable[row, column] == CellType.PlayerCell)
                        this.gridTable[row, column] = CellType.None;
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
                if (this.gridTable[0, column] == CellType.FixedCell)
                    return true;
            }

            return false;
        }

        public int GetActiveBlocks()
        {
            int activeBlocks = 0;

            foreach (CellType Cell in this.gridTable)
                if (Cell != CellType.None)
                    activeBlocks++;

            return activeBlocks;
        }

        public bool isMergeSafe(Cube cube)
        {
            for (int row = 0; row < cube.GetCube().GetLength(0); row++)
            {
                for (int column = 0; column < cube.GetCube().GetLength(1); column++)
                {
                    if (cube.GetCube()[row, column] != CellType.None && this.gridTable[row, column] == CellType.FixedCell)
                        return false;
                }
            }

            return true;
        }

        public void CreateNewPlayerCube()
        {
            this.currentCube = new Cube(this, this.gridTable.GetLength(0), this.gridTable.GetLength(1));
            if (isMergeSafe(this.currentCube))
            {
                Merge(this.currentCube);
            }
        }

        public void Merge(Cube cube)
        {
            ClearPlayerCubeFromGrid();

            if(WipeOutFullRows())
                CollapsFixedCells();

            for (int row = 0; row < cube.GetCube().GetLength(0); row++)
            {
                for (int column = 0; column < cube.GetCube().GetLength(1); column++)
                {
                    if (cube.GetCube()[row, column] != CellType.None)
                    {
                        this.gridTable[row, column] = cube.GetCube()[row, column];
                    }
                }
            }
        }

        public void ConvertAllPlayerCellToFixed()
        {
            for (int row = 0; row < this.gridTable.GetLength(0); row++)
            {
                for (int column = 0; column < this.gridTable.GetLength(1); column++)
                {
                    if (this.gridTable[row, column] == CellType.PlayerCell)
                        this.gridTable[row, column] = CellType.FixedCell;
                }
            }
        }

        public bool WipeOutFullRows()
        {
            for (int row = 0; row < this.gridTable.GetLength(0); row++)
            {
                int fullCount = 0;
                for (int column = 0; column < this.gridTable.GetLength(1); column++)
                {
                    if (this.gridTable[row, column] == CellType.FixedCell)
                        fullCount++;
                    else
                        break;
                }

                if (fullCount >= this.gridTable.GetLength(1))
                {
                    for (int column = 0; column < this.gridTable.GetLength(1); column++)
                    {
                        this.gridTable[row, column] = CellType.None;
                    }
                    return true;
                }
            }
            return false;
        }

        public void CollapsFixedCells()
        {
            for (int row = this.gridTable.GetLength(0) - 1; row >= 0; row--)
            {
                for (int column = 0; column < this.gridTable.GetLength(1); column++)
                {
                    int rowHeight = 0;
                    while (row + (rowHeight+1) < this.gridTable.GetLength(0) && this.gridTable[row + (rowHeight+1), column] == CellType.None &&
                        this.gridTable[row, column] == CellType.FixedCell)
                    {
                        rowHeight++;
                    }

                    if (rowHeight > 0)
                    {
                        this.gridTable[row, column] = CellType.None;
                        this.gridTable[row + rowHeight, column] = CellType.FixedCell;
                    }
                }
            }
        }

        public CellType[,] GetGrid() => this.gridTable;

        public Cube GetCurrentCube() => this.currentCube;

    }
}
