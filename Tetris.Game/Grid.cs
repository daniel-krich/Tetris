using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Tetris.Game
{
    /// <summary>
    /// None = empty cell.
    /// FixedCell = occupied cell, non controllable.
    /// PlayerCell = temporary occupied cell, controllable by the player.
    /// </summary>
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

        private Stats stats { get; set; }

        public Grid(int rows, int columns, Stats stats)
        {
            this.gridTable = new CellType[rows, columns];
            this.stats = stats;

            CreateNewPlayerCube();
        }

        /// <summary>
        /// Clears all the CellType.PlayerCell from the grid
        /// </summary>
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

        /// <summary>
        /// Checks if the game finished or player lost.
        /// </summary>
        /// <returns>True if game still going, False if finished.</returns>
        public bool UpdateGame()
        {
            if (isGameFinished())
                return false;
            if (!isMergeSafe(this.currentCube))
                return false;
            return true;
        }

        /// <summary>
        /// Checks if the game finished or player lost.
        /// </summary>
        /// <returns></returns>
        public bool isGameFinished()
        {
            for (int column = 0; column < this.gridTable.GetLength(1); column++)
            {
                if (this.gridTable[0, column] == CellType.FixedCell)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Count all occupied blocks of the grid.
        /// </summary>
        /// <returns></returns>
        public int GetActiveBlocks()
        {
            int activeBlocks = 0;

            foreach (CellType Cell in this.gridTable)
                if (Cell != CellType.None)
                    activeBlocks++;

            return activeBlocks;
        }

        /// <summary>
        /// Checks if Shape grid and the game grid can merge safely without any cell loss or overrides.
        /// </summary>
        /// <param name="cube">Cube instance</param>
        /// <returns>True if safe, False otherwise</returns>
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

        /// <summary>
        /// Creates a new instance of a random player shape
        /// </summary>
        public void CreateNewPlayerCube()
        {
            this.currentCube = new Cube(this, this.gridTable.GetLength(0), this.gridTable.GetLength(1), this.stats);
            if (isMergeSafe(this.currentCube))
            {
                Merge(this.currentCube);
            }
        }

        /// <summary>
        /// Merges 2 grids together
        /// </summary>
        /// <param name="cube">Cube instance</param>
        public void Merge(Cube cube)
        {
            ClearPlayerCubeFromGrid();

            while(WipeOutFullRows())
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

        /// <summary>
        /// Takes CellType.PlayerCell and turns it into CellType.FixedCell
        /// </summary>
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


        /// <summary>
        /// Wipes all the full rows
        /// </summary>
        /// <returns>True if wiped some rows, False otherwise</returns>
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
                    this.stats.Score += this.gridTable.GetLength(1);

                    for (int column = 0; column < this.gridTable.GetLength(1); column++)
                    {
                        this.gridTable[row, column] = CellType.None;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Make all the cleared rows to collapse down and avoid ghost rows.
        /// </summary>
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

        /// <summary>
        /// Gets the grid or 2D array instance
        /// </summary>
        /// <returns>2D array instance</returns>
        public CellType[,] GetGrid() => this.gridTable;

        /// <summary>
        /// Get the current cube instance
        /// </summary>
        /// <returns>Cube instance</returns>
        public Cube GetCurrentCube() => this.currentCube;

    }
}
