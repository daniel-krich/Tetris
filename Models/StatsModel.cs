using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Models
{
    public class StatsModel : IStatsModel
    {
        public int Score { get; set; }

        public int Level
        {
            get
            {
                if (Score >= 2000)
                    return 5;
                else if (Score >= 1000)
                    return 4;
                else if (Score >= 300)
                    return 3;
                else if (Score >= 100)
                    return 2;
                return 1;
            }
        }

        public int DelayLevel
        {
            get
            {
                switch (this.Level)
                {
                    case 1:
                        return 500;
                    case 2:
                        return 400;
                    case 3:
                        return 250;
                    case 4:
                        return 150;
                    case 5:
                        return 100;
                    default:
                        return 500;
                }
            }
        }

        public ConsoleColor LevelColor
        {
            get
            {
                switch (this.Level)
                {
                    case 1:
                        return ConsoleColor.Green;
                    case 2:
                        return ConsoleColor.DarkCyan;
                    case 3:
                        return ConsoleColor.DarkYellow;
                    case 4:
                        return ConsoleColor.DarkMagenta;
                    case 5:
                        return ConsoleColor.Red;
                    default:
                        return ConsoleColor.Green;
                }
            }
        }

        public string LevelName
        {
            get
            {
                switch (this.Level)
                {
                    case 1:
                        return "Newbie Green";
                    case 2:
                        return "Azure sky";
                    case 3:
                        return "Mid Orange";
                    case 4:
                        return "Crazy Magenta";
                    case 5:
                        return "Hellish red";
                    default:
                        return "Newbie Green";
                }
            }
        }

        public StatsModel()
        {

        }
    }
}
