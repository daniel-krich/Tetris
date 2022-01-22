using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Tetris.Game
{
    using TetrisGame.Tetris.Controls;
    enum CubeType
    {
        l_shape,
        J_shape,
        L_shape,
        O_shape,
        Z_shape,
        T_shape,
        S_shape,
    }

    class Cube
    {
        public static Random random = new Random();

        public CellType[,] cube { get; set; }

        public CubeType type { get; set; }

        private Grid grid { get; set; }
        private Stats stats { get; set; }

        private int rows, columns;

        public Cube(Grid grid, int rows, int columns, Stats stats)
        {
            this.grid = grid;
            this.rows = rows;
            this.columns = columns;
            this.stats = stats;
            //
            var (CubeMatrix, CubeMartixType) = RandomCube();
            this.cube = CubeMatrix;
            this.type = CubeMartixType;
        }

        public (CellType[,], CubeType) RandomCube()
        {
            CellType[,] randCube = new CellType[this.rows, this.columns];
            CubeType randType = 0;
            switch (random.Next(1, 8))
            {
                case 1: // l_shape
                    randType = CubeType.l_shape;
                    randCube[0, 0] = CellType.PlayerCell;
                    randCube[1, 0] = CellType.PlayerCell;
                    randCube[2, 0] = CellType.PlayerCell;
                    randCube[3, 0] = CellType.PlayerCell;
                    break;
                case 2: // J_shape
                    randType = CubeType.J_shape;
                    randCube[0, 3] = CellType.PlayerCell;
                    randCube[1, 3] = CellType.PlayerCell;
                    randCube[2, 3] = CellType.PlayerCell;
                    randCube[2, 2] = CellType.PlayerCell;
                    break;
                case 3: // L_shape
                    randType = CubeType.L_shape;
                    randCube[0, 0] = CellType.PlayerCell;
                    randCube[1, 0] = CellType.PlayerCell;
                    randCube[2, 0] = CellType.PlayerCell;
                    randCube[2, 1] = CellType.PlayerCell;
                    break;
                case 4: // O_shape
                    randType = CubeType.O_shape;
                    randCube[1, 1] = CellType.PlayerCell;
                    randCube[1, 2] = CellType.PlayerCell;
                    randCube[2, 1] = CellType.PlayerCell;
                    randCube[2, 2] = CellType.PlayerCell;
                    break;
                case 5: // Z_shape
                    randType = CubeType.Z_shape;
                    randCube[1, 1] = CellType.PlayerCell;
                    randCube[1, 2] = CellType.PlayerCell;
                    randCube[2, 2] = CellType.PlayerCell;
                    randCube[2, 3] = CellType.PlayerCell;
                    break;
                case 6: // T_shape
                    randType = CubeType.T_shape;
                    randCube[2, 1] = CellType.PlayerCell;
                    randCube[3, 0] = CellType.PlayerCell;
                    randCube[3, 1] = CellType.PlayerCell;
                    randCube[3, 2] = CellType.PlayerCell;
                    break;
                case 7: // S_shape
                    randType = CubeType.S_shape;
                    randCube[1, 3] = CellType.PlayerCell;
                    randCube[1, 2] = CellType.PlayerCell;
                    randCube[2, 2] = CellType.PlayerCell;
                    randCube[2, 1] = CellType.PlayerCell;
                    break;
                default:
                    break;
            }

            return (randCube, randType);
        }

        public void Rotate()
        {
            CellType[,] localClone = this.cube.Clone() as CellType[,];
            int maxRowVal = 0;
            int minRowVal = 0;
            for (int row = 0; row < this.cube.GetLength(0); row++)
            {
                for (int column = 0; column < this.cube.GetLength(1); column++)
                {
                    if (this.cube[row, column] == CellType.PlayerCell)
                    {
                        if (minRowVal == 0)
                            minRowVal = row;
                        maxRowVal = row;
                    }
                }
            }

            

            for (int row = maxRowVal, offset = 0; row >= minRowVal; row--, offset++)
            {
                for (int column = 0; column < this.cube.GetLength(1); column++)
                {
                    if (minRowVal + offset - (maxRowVal - minRowVal) >= 0 &&
                        maxRowVal - offset + (maxRowVal - minRowVal) < this.cube.GetLength(0))
                    {
                        CellType swap = this.cube[maxRowVal - offset + (maxRowVal - minRowVal), column];
                        this.cube[maxRowVal - offset + (maxRowVal - minRowVal), column] = this.cube[minRowVal + offset - (maxRowVal - minRowVal), column];
                        this.cube[minRowVal + offset - (maxRowVal - minRowVal), column] = swap;
                    }
                }
            }
            


            if (this.grid.isMergeSafe(this))
                this.grid.Merge(this);
            else
                this.cube = localClone;
        }


        public bool Move(GameKeys key, bool cloneMove = false)
        {
            CellType[,] localClone = this.cube.Clone() as CellType[,];
            switch (key)
            {
                case GameKeys.Down:
                    for (int row = this.cube.GetLength(0) - 1; row >= 0; row--)
                    {
                        for (int column = 0; column < this.cube.GetLength(1); column++)
                        {
                            if (this.cube[row, column] == CellType.PlayerCell)
                            {
                                if (row < this.cube.GetLength(0) - 1)
                                {
                                    this.cube[row, column] = CellType.None;
                                    this.cube[row + 1, column] = CellType.PlayerCell;
                                }
                                else
                                {
                                    this.cube = localClone;
                                    this.stats.AddScore(5);
                                    this.grid.ConvertAllPlayerCellToFixed();
                                    this.grid.CreateNewPlayerCube();
                                    return false;
                                }
                            }
                        }
                    }
                    if (this.grid.isMergeSafe(this) && !cloneMove)
                    {
                        this.stats.AddScore(1);
                        this.grid.Merge(this);
                        Move(key, !cloneMove);
                        return true;
                    }
                    else if(this.grid.isMergeSafe(this) && cloneMove)
                    {
                        this.cube = localClone;
                        return false;
                    }
                    else
                    {
                        this.cube = localClone;
                        this.stats.AddScore(5);
                        this.grid.ConvertAllPlayerCellToFixed();
                        this.grid.CreateNewPlayerCube();
                        return false;
                    }

                case GameKeys.Left:
                    for (int row = 0; row < this.cube.GetLength(0); row++)
                    {
                        for (int column = 0; column < this.cube.GetLength(1); column++)
                        {
                            if (this.cube[row, column] == CellType.PlayerCell)
                            {
                                if (column > 0)
                                {
                                    this.cube[row, column] = CellType.None;
                                    this.cube[row, column - 1] = CellType.PlayerCell;
                                }
                                else
                                {
                                    this.cube = localClone;
                                    return false;
                                }
                            }
                        }
                    }
                    if (this.grid.isMergeSafe(this))
                    {
                        this.grid.Merge(this);
                        return true;
                    }
                    else
                    {
                        this.cube = localClone;
                        return false;
                    }

                case GameKeys.Right:
                    for (int row = 0; row < this.cube.GetLength(0); row++)
                    {
                        for (int column = this.cube.GetLength(1) - 1; column >= 0; column--)
                        {
                            if (this.cube[row, column] == CellType.PlayerCell)
                            {
                                if (column < this.cube.GetLength(1) - 1)
                                {
                                    this.cube[row, column] = CellType.None;
                                    this.cube[row, column + 1] = CellType.PlayerCell;
                                }
                                else
                                {
                                    this.cube = localClone;
                                    return false;
                                }
                            }
                        }
                    }
                    if (this.grid.isMergeSafe(this))
                    {
                        this.grid.Merge(this);
                        return true;
                    }
                    else
                    {
                        this.cube = localClone;
                        return false;
                    }
                    
                default:
                    return false;
            }
        }

        public int GetActiveBlocks()
        {
            int activeBlocks = 0;

            foreach (CellType Cell in this.cube)
                if (Cell != CellType.None)
                    activeBlocks++;

            return activeBlocks;
        }

        public CellType[,] GetCube() => this.cube;
    }
}
