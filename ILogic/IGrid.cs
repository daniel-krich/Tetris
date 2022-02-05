using TetrisGame.Enums;
using TetrisGame.Logic;

namespace TetrisGame.ILogic
{
    public interface IGrid
    {
        Cube CurrentCube { get; }
        CellType[,] CurrentGrid { get; }

        void ConvertAllPlayerCellToFixed();
        void CreateGridTable(int rows, int column);
        void CreateNewPlayerCube();
        bool isGameFinished();
        bool isMergeSafe(Cube cube);
        void Merge(Cube cube);
        bool UpdateGame();
    }
}