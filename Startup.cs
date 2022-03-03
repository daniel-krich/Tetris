using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TetrisGame.Core;
using TetrisGame.Contracts;

namespace TetrisGame
{
    public static class Startup
    {
        public static void Start()
        {
            ITetris tetris = AppServices.ServiceProvider.GetRequiredService<ITetris>();
            tetris.Run(20, 10);

            Wait();
        }

        public static void Wait()
        {
            while (true)
                Thread.Sleep(1000);
        }
    }
}
