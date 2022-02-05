using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Enums;
using TetrisGame.Models;
using TetrisGame.Core;

namespace TetrisGame.GameLogic
{
    class Grid : IGrid
    {
        private CellType[,] _gridTable;

        private Cube _currentCube;

        private IStatsModel _statsModel;

        public Grid(IStatsModel statsModel)
        {
            _statsModel = statsModel;
        }

        public void CreateGridTable(int rows, int column)
        {
            _gridTable = new CellType[rows, column];
            CreateNewPlayerCube();
        }

        /// <summary>
        /// Clears all the CellType.PlayerCell from the grid
        /// </summary>
        public void ClearPlayerCubeFromGrid()
        {
            for (int row = 0; row < _gridTable.GetLength(0); row++)
            {
                for (int column = 0; column < _gridTable.GetLength(1); column++)
                {
                    if (_gridTable[row, column] == CellType.PlayerCell)
                        _gridTable[row, column] = CellType.None;
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
            if (!isMergeSafe(_currentCube))
                return false;
            return true;
        }

        /// <summary>
        /// Checks if the game finished or player lost.
        /// </summary>
        /// <returns></returns>
        public bool isGameFinished()
        {
            for (int column = 0; column < _gridTable.GetLength(1); column++)
            {
                if (_gridTable[0, column] == CellType.FixedCell)
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

            foreach (CellType Cell in _gridTable)
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
                    if (cube.GetCube()[row, column] != CellType.None && _gridTable[row, column] == CellType.FixedCell)
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
            _currentCube = AppServices.ServiceProvider.GetRequiredService<ICube>() as Cube;
            if (isMergeSafe(_currentCube))
            {
                Merge(_currentCube);
            }
        }

        /// <summary>
        /// Merges 2 grids together
        /// </summary>
        /// <param name="cube">Cube instance</param>
        public void Merge(Cube cube)
        {
            ClearPlayerCubeFromGrid();

            while (WipeOutFullRows())
                CollapsFixedCells();

            for (int row = 0; row < cube.GetCube().GetLength(0); row++)
            {
                for (int column = 0; column < cube.GetCube().GetLength(1); column++)
                {
                    if (cube.GetCube()[row, column] != CellType.None)
                    {
                        _gridTable[row, column] = cube.GetCube()[row, column];
                    }
                }
            }
        }

        /// <summary>
        /// Takes CellType.PlayerCell and turns it into CellType.FixedCell
        /// </summary>
        public void ConvertAllPlayerCellToFixed()
        {
            for (int row = 0; row < _gridTable.GetLength(0); row++)
            {
                for (int column = 0; column < _gridTable.GetLength(1); column++)
                {
                    if (_gridTable[row, column] == CellType.PlayerCell)
                        _gridTable[row, column] = CellType.FixedCell;
                }
            }
        }


        /// <summary>
        /// Wipes all the full rows
        /// </summary>
        /// <returns>True if wiped some rows, False otherwise</returns>
        public bool WipeOutFullRows()
        {
            for (int row = 0; row < _gridTable.GetLength(0); row++)
            {
                int fullCount = 0;
                for (int column = 0; column < _gridTable.GetLength(1); column++)
                {
                    if (_gridTable[row, column] == CellType.FixedCell)
                        fullCount++;
                    else
                        break;
                }

                if (fullCount >= _gridTable.GetLength(1))
                {
                    _statsModel.Score += _gridTable.GetLength(1);

                    for (int column = 0; column < _gridTable.GetLength(1); column++)
                    {
                        _gridTable[row, column] = CellType.None;
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
            for (int row = _gridTable.GetLength(0) - 1; row >= 0; row--)
            {
                for (int column = 0; column < _gridTable.GetLength(1); column++)
                {
                    int rowHeight = 0;
                    while (row + (rowHeight + 1) < _gridTable.GetLength(0) && _gridTable[row + (rowHeight + 1), column] == CellType.None &&
                        _gridTable[row, column] == CellType.FixedCell)
                    {
                        rowHeight++;
                    }

                    if (rowHeight > 0)
                    {
                        _gridTable[row, column] = CellType.None;
                        _gridTable[row + rowHeight, column] = CellType.FixedCell;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the grid or 2D array instance
        /// </summary>
        /// <returns>2D array instance</returns>
        public CellType[,] GetGrid() => _gridTable;

        /// <summary>
        /// Get the current cube instance
        /// </summary>
        /// <returns>Cube instance</returns>
        public Cube GetCurrentCube() => _currentCube;

    }
}
