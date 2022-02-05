using TetrisGame.Enums;

namespace TetrisGame.ILogic
{
    public interface ICube
    {
        CubeType CubeType { get; set; }
        CellType[,] CurrentCube { get; set; }
        void RecreateCube();
        bool Move(VirtualKeyCodes key, bool cloneMove = false);
        void Rotate();
    }
}