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

        public CellType[,] CurrentMatrix { get; set; }

        private IStatsModel _statsModel;

        public Cube(IStatsModel statsModel)
        {
            _statsModel = statsModel;
        }

        public void RecreateCube(IGrid grid)
        {
            var (CubeMatrix, CubeMartixType) = RandomCube(grid);
            CurrentMatrix = CubeMatrix;
            CubeType = CubeMartixType;
        }

        /// <summary>
        /// Generates a random shaped cube
        /// </summary>
        /// <returns>Tuple of a new 2D array where only the shape exists, and current shape type</returns>
        private (CellType[,], CubeType) RandomCube(IGrid grid)
        {
            CellType[,] randCube = new CellType[grid.CurrentGrid.GetLength(0), grid.CurrentGrid.GetLength(1)];
            CubeType randType = 0;
            switch (Utils.Random.Next(1, 8))
            {
                case 1: // l_shape
                    randType = CubeType.l_shape;
                    randCube[0, 0 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[1, 0 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[2, 0 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[3, 0 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    break;
                case 2: // J_shape
                    randType = CubeType.J_shape;
                    randCube[0, 3 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[1, 3 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 3 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 2 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    break;
                case 3: // L_shape
                    randType = CubeType.L_shape;
                    randCube[0, 0 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[1, 0 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[2, 0 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[2, 1 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    break;
                case 4: // O_shape
                    randType = CubeType.O_shape;
                    randCube[1, 1 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[1, 2 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 1 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 2 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    break;
                case 5: // Z_shape
                    randType = CubeType.Z_shape;
                    randCube[1, 1 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[1, 2 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[2, 2 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    randCube[2, 3 + grid.CurrentGrid.GetLength(1) / 2] = CellType.PlayerCell;
                    break;
                case 6: // T_shape
                    randType = CubeType.T_shape;
                    randCube[2, 1 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[3, 0 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[3, 1 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[3, 2 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    break;
                case 7: // S_shape
                    randType = CubeType.S_shape;
                    randCube[1, 3 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[1, 2 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 2 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    randCube[2, 1 + grid.CurrentGrid.GetLength(1) / 2 - 2] = CellType.PlayerCell;
                    break;
                default:
                    break;
            }

            return (randCube, randType);
        }

        
    }
}
