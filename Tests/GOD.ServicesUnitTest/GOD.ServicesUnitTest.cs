using System;
using Xunit;
using backend.src.GOD.BussineServices.Services.Player;
using NMock;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Game;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Round;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;
using backend.src.GOD.BussineServices.Services.Game;
using backend.src.GOD.BussineServices.Services.Round;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Player;
using backend.src.GOD.DataAccess.Context;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace GOD.ServicesUnitTest
{
    public class ServicesUnitTest
    {
        private IServiceCollection services;
        private IServiceProvider serviceProvider;
        private IPlayerService _playerService;

        public static IConfiguration Configuration { get; set; }

        //private MockFactory mocks;
        private void RegisterServices ()
        {
            services = new ServiceCollection();
            services.AddScoped<DbContext, GODDataContext>();

            void MigrationAssembly(SqlServerDbContextOptionsBuilder x) => x.MigrationsAssembly("GOD.DataAccess.Migrations");
            var connectionString = "Server=localhost;Database=GODData;User ID=sa;Password=Pass@123;Connect Timeout=30;Trusted_Connection=False;MultipleActiveResultSets=true";

            services.AddDbContext<GODDataContext>(options =>
                options.UseSqlServer(connectionString, MigrationAssembly));

            services.AddDbContext<GODDataContext>();

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IRoundRepository, RoundRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IRoundService, RoundService>();

            serviceProvider = services.BuildServiceProvider();
        }


        public ServicesUnitTest()
        {
            RegisterServices();
            _playerService = serviceProvider.GetRequiredService<IPlayerService>();

        }
        [Fact]
        public async void CreateGames()
        {
            var gamesNumer = 1;
            Console.WriteLine("Creating 100 games");
            Console.WriteLine();
            Console.WriteLine();
            while (gamesNumer < 100)
            {
                Console.WriteLine($"Start the {gamesNumer} game ...");
                gamesNumer++;

            }
            //var player1 = await _playerService.GetPlayerByNumber(1);
            Console.WriteLine("Test Finish");
        }
    }
}
