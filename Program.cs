using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Core;
using TetrisGame.GameLogic;

namespace TetrisGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            var services = AppServices.Configure();
            ITetris t = services.GetRequiredService<ITetris>();
            t.Run();
        }
    }
}
