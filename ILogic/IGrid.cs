using TetrisGame.Enums;
using TetrisGame.Logic;

namespace TetrisGame.ILogic
{
    public interface IGrid
    {
        ICube CurrentCube { get; }
        CellType[,] CurrentGrid { get; }

        void ConvertAllPlayerCellToFixed();
        void CreateGridTable(int rows, int column);
        void CreateNewPlayerCube();
        bool isGameFinished();
        bool isMergeCubeWithGridSafe();
        void MergeCubeWithGrid();
        bool MoveCube(VirtualKeyCodes key, bool cloneMove = false);
        void RotateCube();
        bool UpdateGame();
    }
}