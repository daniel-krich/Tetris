using TetrisGame.Enums;

namespace TetrisGame.GameLogic
{
    public interface IGrid
    {
        void ClearPlayerCubeFromGrid();
        void CollapsFixedCells();
        void ConvertAllPlayerCellToFixed();
        void CreateNewPlayerCube();
        int GetActiveBlocks();
        void CreateGridTable(int rows, int column);
        Cube GetCurrentCube();
        CellType[,] GetGrid();
        bool isGameFinished();
        bool isMergeSafe(Cube cube);
        void Merge(Cube cube);
        bool UpdateGame();
        bool WipeOutFullRows();
    }
}