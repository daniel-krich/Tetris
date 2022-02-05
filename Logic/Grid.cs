using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Enums;
using TetrisGame.Models;
using TetrisGame.Core;
using TetrisGame.ILogic;

namespace TetrisGame.Logic
{
    class Grid : IGrid
    {
        private CellType[,] _gridTable;

        private ICube _cube;

        private IStatsModel _statsModel;

        public CellType[,] CurrentGrid { get => _gridTable; }

        public Cube CurrentCube { get => _cube as Cube; }

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
        private void ClearPlayerCubeFromGrid()
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
            if (!isMergeSafe(_cube as Cube))
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
        /// Checks if Shape grid and the game grid can merge safely without any cell loss or overrides.
        /// </summary>
        /// <param name="cube">Cube instance</param>
        /// <returns>True if safe, False otherwise</returns>
        public bool isMergeSafe(Cube cube)
        {
            for (int row = 0; row < cube.CurrentCube.GetLength(0); row++)
            {
                for (int column = 0; column < cube.CurrentCube.GetLength(1); column++)
                {
                    if (cube.CurrentCube[row, column] != CellType.None && _gridTable[row, column] == CellType.FixedCell)
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
            if (_cube is null)
                _cube = AppServices.ServiceProvider.GetRequiredService<ICube>();

            _cube.RecreateCube();
            if (isMergeSafe(_cube as Cube))
            {
                Merge(_cube as Cube);
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

            for (int row = 0; row < cube.CurrentCube.GetLength(0); row++)
            {
                for (int column = 0; column < cube.CurrentCube.GetLength(1); column++)
                {
                    if (cube.CurrentCube[row, column] != CellType.None)
                    {
                        _gridTable[row, column] = cube.CurrentCube[row, column];
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
        private bool WipeOutFullRows()
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
        private void CollapsFixedCells()
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
    }
}
