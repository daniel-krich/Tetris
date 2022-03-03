using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Enums;
using TetrisGame.Models;
using TetrisGame.Core;
using TetrisGame.Contracts;

namespace TetrisGame.Logic
{
    class Grid : IGrid
    {
        private CellType[,] _gridTable;

        private ICube _cube;

        private IStatsModel _statsModel;

        public CellType[,] CurrentGrid { get => _gridTable; }

        public ICube CurrentCube { get => _cube; }

        public Grid(IStatsModel statsModel, ICube cube)
        {
            _cube = cube;
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
            if (!isMergeCubeWithGridSafe())
                return false;
            return true;
        }

        /// <summary>
        /// Checks if the game finished or player lost.
        /// </summary>
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
        public bool isMergeCubeWithGridSafe()
        {
            for (int row = 0; row < CurrentCube.CurrentMatrix.GetLength(0); row++)
            {
                for (int column = 0; column < CurrentCube.CurrentMatrix.GetLength(1); column++)
                {
                    if (CurrentCube.CurrentMatrix[row, column] != CellType.None && _gridTable[row, column] == CellType.FixedCell)
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
            _cube.RecreateCube(this);
            if (isMergeCubeWithGridSafe())
            {
                MergeCubeWithGrid();
            }
        }

        /// <summary>
        /// Merges 2 grids together
        /// </summary>
        public void MergeCubeWithGrid()
        {
            ClearPlayerCubeFromGrid();

            while (WipeOutFullRows())
                CollapsFixedCells();

            for (int row = 0; row < CurrentCube.CurrentMatrix.GetLength(0); row++)
            {
                for (int column = 0; column < CurrentCube.CurrentMatrix.GetLength(1); column++)
                {
                    if (CurrentCube.CurrentMatrix[row, column] != CellType.None)
                    {
                        _gridTable[row, column] = CurrentCube.CurrentMatrix[row, column];
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

        /// <summary>
        /// Rotates the shape 180 degrees each call.
        /// </summary>
        public void RotateCube()
        {
            CellType[,] localClone = CurrentCube.CurrentMatrix.Clone() as CellType[,];
            int maxRowVal = 0;
            int minRowVal = 0;
            for (int row = 0; row < CurrentCube.CurrentMatrix.GetLength(0); row++)
            {
                for (int column = 0; column < CurrentCube.CurrentMatrix.GetLength(1); column++)
                {
                    if (CurrentCube.CurrentMatrix[row, column] == CellType.PlayerCell)
                    {
                        if (minRowVal == 0)
                            minRowVal = row;
                        maxRowVal = row;
                    }
                }
            }



            for (int row = maxRowVal, offset = 0; row >= minRowVal; row--, offset++)
            {
                for (int column = 0; column < CurrentCube.CurrentMatrix.GetLength(1); column++)
                {
                    if (minRowVal + offset - (maxRowVal - minRowVal) >= 0 &&
                        maxRowVal - offset + (maxRowVal - minRowVal) < CurrentCube.CurrentMatrix.GetLength(0))
                    {
                        CellType swap = CurrentCube.CurrentMatrix[maxRowVal - offset + (maxRowVal - minRowVal), column];
                        CurrentCube.CurrentMatrix[maxRowVal - offset + (maxRowVal - minRowVal), column] = CurrentCube.CurrentMatrix[minRowVal + offset - (maxRowVal - minRowVal), column];
                        CurrentCube.CurrentMatrix[minRowVal + offset - (maxRowVal - minRowVal), column] = swap;
                    }
                }
            }



            if (isMergeCubeWithGridSafe())
                MergeCubeWithGrid();
            else
                CurrentCube.CurrentMatrix = localClone;
        }


        /// <summary>
        /// Moves the shape Left/Right/Down on the grid.
        /// </summary>
        /// <param name="key">GameKeys enum (Down, Left, Right, X)</param>
        /// <param name="cloneMove">used to predict if the next move is a dead end</param>
        /// <returns>false if can't move that direction, or true otherwise.</returns>
        public bool MoveCube(VirtualKeyCodes key, bool cloneMove = false)
        {
            CellType[,] localClone = CurrentCube.CurrentMatrix.Clone() as CellType[,];
            switch (key)
            {
                case VirtualKeyCodes.VK_DOWN:
                    for (int row = CurrentCube.CurrentMatrix.GetLength(0) - 1; row >= 0; row--)
                    {
                        for (int column = 0; column < CurrentCube.CurrentMatrix.GetLength(1); column++)
                        {
                            if (CurrentCube.CurrentMatrix[row, column] == CellType.PlayerCell)
                            {
                                if (row < CurrentCube.CurrentMatrix.GetLength(0) - 1)
                                {
                                    CurrentCube.CurrentMatrix[row, column] = CellType.None;
                                    CurrentCube.CurrentMatrix[row + 1, column] = CellType.PlayerCell;
                                }
                                else
                                {
                                    CurrentCube.CurrentMatrix = localClone;
                                    _statsModel.Score += 5;
                                    ConvertAllPlayerCellToFixed();
                                    CreateNewPlayerCube();
                                    return false;
                                }
                            }
                        }
                    }
                    if (isMergeCubeWithGridSafe() && !cloneMove)
                    {
                        _statsModel.Score += 1;
                        MergeCubeWithGrid();
                        MoveCube(key, !cloneMove);
                        return true;
                    }
                    else if (isMergeCubeWithGridSafe() && cloneMove)
                    {
                        CurrentCube.CurrentMatrix = localClone;
                        return false;
                    }
                    else
                    {
                        CurrentCube.CurrentMatrix = localClone;
                        _statsModel.Score += 5;
                        ConvertAllPlayerCellToFixed();
                        CreateNewPlayerCube();
                        return false;
                    }

                case VirtualKeyCodes.VK_LEFT:
                    for (int row = 0; row < CurrentCube.CurrentMatrix.GetLength(0); row++)
                    {
                        for (int column = 0; column < CurrentCube.CurrentMatrix.GetLength(1); column++)
                        {
                            if (CurrentCube.CurrentMatrix[row, column] == CellType.PlayerCell)
                            {
                                if (column > 0)
                                {
                                    CurrentCube.CurrentMatrix[row, column] = CellType.None;
                                    CurrentCube.CurrentMatrix[row, column - 1] = CellType.PlayerCell;
                                }
                                else
                                {
                                    CurrentCube.CurrentMatrix = localClone;
                                    return false;
                                }
                            }
                        }
                    }
                    if (isMergeCubeWithGridSafe())
                    {
                        MergeCubeWithGrid();
                        return true;
                    }
                    else
                    {
                        CurrentCube.CurrentMatrix = localClone;
                        return false;
                    }

                case VirtualKeyCodes.VK_RIGHT:
                    for (int row = 0; row < CurrentCube.CurrentMatrix.GetLength(0); row++)
                    {
                        for (int column = CurrentCube.CurrentMatrix.GetLength(1) - 1; column >= 0; column--)
                        {
                            if (CurrentCube.CurrentMatrix[row, column] == CellType.PlayerCell)
                            {
                                if (column < CurrentCube.CurrentMatrix.GetLength(1) - 1)
                                {
                                    CurrentCube.CurrentMatrix[row, column] = CellType.None;
                                    CurrentCube.CurrentMatrix[row, column + 1] = CellType.PlayerCell;
                                }
                                else
                                {
                                    CurrentCube.CurrentMatrix = localClone;
                                    return false;
                                }
                            }
                        }
                    }
                    if (isMergeCubeWithGridSafe())
                    {
                        MergeCubeWithGrid();
                        return true;
                    }
                    else
                    {
                        CurrentCube.CurrentMatrix = localClone;
                        return false;
                    }

                default:
                    return false;
            }
        }
    }
}
