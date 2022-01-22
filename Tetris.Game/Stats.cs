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

        /// <summary>
        /// Add score to the stats
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(int score)
        {
            this.score += score;
        }

        /// <summary>
        /// Get score
        /// </summary>
        /// <returns></returns>
        public int GetScore() => this.score;
    }
}
