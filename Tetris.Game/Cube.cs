using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Tetris.Game
{
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

        public int[,] cube { get; set; }

        public CubeType type { get; set; }

        public Cube()
        {
            var (CubeMatrix, CubeMartixType) = RandomCube();
            this.cube = CubeMatrix;
            this.type = CubeMartixType;
        }

        public (int[,], CubeType) RandomCube()
        {
            int[,] randCube = new int[4, 4];
            CubeType randType = 0;
            switch (random.Next(1, 8))
            {
                case 1: // l_shape
                    randType = CubeType.l_shape;
                    randCube[0, 0] = 2;
                    randCube[1, 0] = 2;
                    randCube[2, 0] = 2;
                    randCube[3, 0] = 2;
                    break;
                case 2: // J_shape
                    randType = CubeType.J_shape;
                    randCube[0, 3] = 2;
                    randCube[1, 3] = 2;
                    randCube[2, 3] = 2;
                    randCube[2, 2] = 2;
                    break;
                case 3: // L_shape
                    randType = CubeType.L_shape;
                    randCube[0, 0] = 2;
                    randCube[1, 0] = 2;
                    randCube[2, 0] = 2;
                    randCube[2, 1] = 2;
                    break;
                case 4: // O_shape
                    randType = CubeType.O_shape;
                    randCube[1, 1] = 2;
                    randCube[1, 2] = 2;
                    randCube[2, 1] = 2;
                    randCube[2, 2] = 2;
                    break;
                case 5: // Z_shape
                    randType = CubeType.Z_shape;
                    randCube[1, 1] = 2;
                    randCube[1, 2] = 2;
                    randCube[2, 2] = 2;
                    randCube[2, 3] = 2;
                    break;
                case 6: // T_shape
                    randType = CubeType.T_shape;
                    randCube[2, 1] = 2;
                    randCube[3, 0] = 2;
                    randCube[3, 1] = 2;
                    randCube[3, 2] = 2;
                    break;
                case 7: // S_shape
                    randType = CubeType.S_shape;
                    randCube[1, 3] = 2;
                    randCube[1, 2] = 2;
                    randCube[2, 2] = 2;
                    randCube[2, 1] = 2;
                    break;
                default:
                    break;
            }

            return (randCube, randType);
        }

        public void Rotate()
        {

        }

        public int[,] GetCube() => this.cube;
    }
}
