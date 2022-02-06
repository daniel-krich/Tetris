using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TetrisGame.Core;
using TetrisGame.ILogic;

namespace TetrisGame
{
    public static class Startup
    {
        public static void Start()
        {
            var services = AppServices.Configure();
            ITetris t = services.GetRequiredService<ITetris>();
            t.Run(20, 10);

            Wait();
        }

        public static void Wait()
        {
            while (true)
                Thread.Sleep(1000);
        }
    }
}
