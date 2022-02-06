using TetrisGame.Enums;

namespace TetrisGame.ILogic
{
    public interface ICube
    {
        CubeType CubeType { get; set; }
        CellType[,] CurrentMatrix { get; set; }
        void RecreateCube(IGrid grid);
    }
}