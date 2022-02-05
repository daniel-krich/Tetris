using System;

namespace TetrisGame.Models
{
    public interface IStatsModel
    {
        int DelayLevel { get; }
        int Level { get; }
        ConsoleColor LevelColor { get; }
        string LevelName { get; }
        int Score { get; set; }
    }
}