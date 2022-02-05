using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisGame.GameLogic;
using TetrisGame.UI;
using TetrisGame.Models;

namespace TetrisGame.Core
{

    public class AppServices
    {
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ServiceProvider { get => _serviceProvider; }

        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            services.AddTransient<ICube, Cube>();
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
