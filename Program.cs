using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Core;
using TetrisGame.Logic;
using TetrisGame.ILogic;
using System.Threading;

namespace TetrisGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Startup.Start();
        }
    }
}
