using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame.Tetris.Game
{
    class Stats
    {
        private int score;

        public Stats()
        {
            this.score = 0;
        }

        public void AddScore(int score)
        {
            this.score += score;
        }

        public int GetScore() => this.score;
    }
}
