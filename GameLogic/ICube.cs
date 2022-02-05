using TetrisGame.Enums;

namespace TetrisGame.GameLogic
{
    public interface ICube
    {
        CellType[,] cube { get; set; }
        CubeType type { get; set; }

        int GetActiveBlocks();
        CellType[,] GetCube();
        bool Move(VirtualKeyCodes key, bool cloneMove = false);
        (CellType[,], CubeType) RandomCube();
        void Rotate();
    }
}