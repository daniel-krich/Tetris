using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.Logic;
using TetrisGame.UI;
using TetrisGame.Models;
using TetrisGame.ILogic;

namespace TetrisGame.Core
{

    public static class AppServices
    {
        private static IServiceProvider _serviceProvider;
        private static readonly object _object = new object();
        public static IServiceProvider ServiceProvider
        {
            get
            {
                lock (_object)
                {
                    return _serviceProvider ?? Configure();
                }
            }
        }

        private static IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ICube, Cube>();
            services.AddSingleton<IControls, Controls>();
            services.AddSingleton<IGrid, Grid>();
            services.AddSingleton<IDisplay, Display>();
            services.AddSingleton<IStatsModel, StatsModel>();
            services.AddSingleton<ITetris, Tetris>();
            _serviceProvider = services.BuildServiceProvider();
            return _serviceProvider;
        }
    }
}
