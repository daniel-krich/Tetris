using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Core;
using TetrisGame.Enums;
using TetrisGame.ILogic;
using TetrisGame.Models;

namespace TetrisGame.Logic
{

    public class Cube : ICube
    {

        public CubeType CubeType { get; set; }

        public CellType[,] CurrentCube { get; set; }

        private ITetris _tetris;
        private IGrid _grid;
        private IStatsModel _statsModel;

        public Cube(ITetris tetris, IGrid grid, IStatsModel statsModel)
        {
            _tetris = tetris;
            _grid = grid;
            _statsModel = statsModel;
        }

        public void RecreateCube()
        {
            var (CubeMatrix, CubeMartixType) = RandomCube();
            CurrentCube = CubeMatrix;
            CubeType = CubeMartixType;
        }

        /// <summary>
        /// Generates a random shaped cube
        /// </summary>
        /// <returns>Tuple of a new 2D array where only the shape exists, and current shape type</returns>
        private (CellType[,], CubeType) RandomCube()
        {
            CellType[,] randCube = new CellType[_tetris.Rows, _tetris.Columns];
            CubeType randType = 0;
            switch (Utils.random.Next(1, 8))
            {
                case 1: // l_shape
                    randType = CubeType.l_shape;
                    randCube[0, 0 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[1, 0 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[2, 0 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[3, 0 + _tetris.Columns / 2] = CellType.PlayerCell;
                    break;
                case 2: // J_shape
                    randType = CubeType.J_shape;
                    randCube[0, 3 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[1, 3 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 3 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 2 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    break;
                case 3: // L_shape
                    randType = CubeType.L_shape;
                    randCube[0, 0 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[1, 0 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[2, 0 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[2, 1 + _tetris.Columns / 2] = CellType.PlayerCell;
                    break;
                case 4: // O_shape
                    randType = CubeType.O_shape;
                    randCube[1, 1 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[1, 2 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 1 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 2 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    break;
                case 5: // Z_shape
                    randType = CubeType.Z_shape;
                    randCube[1, 1 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[1, 2 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[2, 2 + _tetris.Columns / 2] = CellType.PlayerCell;
                    randCube[2, 3 + _tetris.Columns / 2] = CellType.PlayerCell;
                    break;
                case 6: // T_shape
                    randType = CubeType.T_shape;
                    randCube[2, 1 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[3, 0 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[3, 1 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[3, 2 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    break;
                case 7: // S_shape
                    randType = CubeType.S_shape;
                    randCube[1, 3 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[1, 2 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 2 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 1 + _tetris.Columns / 2 - 2] = CellType.PlayerCell;
                    break;
                default:
                    break;
            }

            return (randCube, randType);
        }

        /// <summary>
        /// Rotates the shape 180 degrees each call.
        /// </summary>
        public void Rotate()
        {
            CellType[,] localClone = CurrentCube.Clone() as CellType[,];
            int maxRowVal = 0;
            int minRowVal = 0;
            for (int row = 0; row < CurrentCube.GetLength(0); row++)
            {
                for (int column = 0; column < CurrentCube.GetLength(1); column++)
                {
                    if (CurrentCube[row, column] == CellType.PlayerCell)
                    {
                        if (minRowVal == 0)
                            minRowVal = row;
                        maxRowVal = row;
                    }
                }
            }



            for (int row = maxRowVal, offset = 0; row >= minRowVal; row--, offset++)
            {
                for (int column = 0; column < CurrentCube.GetLength(1); column++)
                {
                    if (minRowVal + offset - (maxRowVal - minRowVal) >= 0 &&
                        maxRowVal - offset + (maxRowVal - minRowVal) < CurrentCube.GetLength(0))
                    {
                        CellType swap = CurrentCube[maxRowVal - offset + (maxRowVal - minRowVal), column];
                        CurrentCube[maxRowVal - offset + (maxRowVal - minRowVal), column] = CurrentCube[minRowVal + offset - (maxRowVal - minRowVal), column];
                        CurrentCube[minRowVal + offset - (maxRowVal - minRowVal), column] = swap;
                    }
                }
            }



            if (_grid.isMergeSafe(this))
                _grid.Merge(this);
            else
                CurrentCube = localClone;
        }


        /// <summary>
        /// Moves the shape Left/Right/Down on the grid.
        /// </summary>
        /// <param name="key">GameKeys enum (Down, Left, Right, X)</param>
        /// <param name="cloneMove">used to predict if the next move is a dead end</param>
        /// <returns>false if can't move that direction, or true otherwise.</returns>
        public bool Move(VirtualKeyCodes key, bool cloneMove = false)
        {
            CellType[,] localClone = CurrentCube.Clone() as CellType[,];
            switch (key)
            {
                case VirtualKeyCodes.VK_DOWN:
                    for (int row = CurrentCube.GetLength(0) - 1; row >= 0; row--)
                    {
                        for (int column = 0; column < CurrentCube.GetLength(1); column++)
                        {
                            if (CurrentCube[row, column] == CellType.PlayerCell)
                            {
                                if (row < CurrentCube.GetLength(0) - 1)
                                {
                                    CurrentCube[row, column] = CellType.None;
                                    CurrentCube[row + 1, column] = CellType.PlayerCell;
                                }
                                else
                                {
                                    CurrentCube = localClone;
                                    _statsModel.Score += 5;
                                    _grid.ConvertAllPlayerCellToFixed();
                                    _grid.CreateNewPlayerCube();
                                    return false;
                                }
                            }
                        }
                    }
                    if (_grid.isMergeSafe(this) && !cloneMove)
                    {
                        _statsModel.Score += 1;
                        _grid.Merge(this);
                        Move(key, !cloneMove);
                        return true;
                    }
                    else if (_grid.isMergeSafe(this) && cloneMove)
                    {
                        CurrentCube = localClone;
                        return false;
                    }
                    else
                    {
                        CurrentCube = localClone;
                        _statsModel.Score += 5;
                        _grid.ConvertAllPlayerCellToFixed();
                        _grid.CreateNewPlayerCube();
                        return false;
                    }

                case VirtualKeyCodes.VK_LEFT:
                    for (int row = 0; row < CurrentCube.GetLength(0); row++)
                    {
                        for (int column = 0; column < CurrentCube.GetLength(1); column++)
                        {
                            if (CurrentCube[row, column] == CellType.PlayerCell)
                            {
                                if (column > 0)
                                {
                                    CurrentCube[row, column] = CellType.None;
                                    CurrentCube[row, column - 1] = CellType.PlayerCell;
                                }
                                else
                                {
                                    CurrentCube = localClone;
                                    return false;
                                }
                            }
                        }
                    }
                    if (_grid.isMergeSafe(this))
                    {
                        _grid.Merge(this);
                        return true;
                    }
                    else
                    {
                        CurrentCube = localClone;
                        return false;
                    }

                case VirtualKeyCodes.VK_RIGHT:
                    for (int row = 0; row < CurrentCube.GetLength(0); row++)
                    {
                        for (int column = CurrentCube.GetLength(1) - 1; column >= 0; column--)
                        {
                            if (CurrentCube[row, column] == CellType.PlayerCell)
                            {
                                if (column < CurrentCube.GetLength(1) - 1)
                                {
                                    CurrentCube[row, column] = CellType.None;
                                    CurrentCube[row, column + 1] = CellType.PlayerCell;
                                }
                                else
                                {
                                    CurrentCube = localClone;
                                    return false;
                                }
                            }
                        }
                    }
                    if (_grid.isMergeSafe(this))
                    {
                        _grid.Merge(this);
                        return true;
                    }
                    else
                    {
                        CurrentCube = localClone;
                        return false;
                    }

                default:
                    return false;
            }
        }
    }
}
