namespace TetrisGame.ILogic
{
    public interface ITetris
    {
        int Rows { get; set; }
        int Columns { get; set; }
        void Run();
    }
}