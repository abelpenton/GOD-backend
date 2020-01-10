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
using backend.src.GOD.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GOD.ServicesUnitTest
{
    public class ServicesUnitTest
    {
        private IServiceCollection services;
        private IServiceProvider serviceProvider;
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        private readonly IRoundService _roundService;

        private List<string> PlayerNames { get; set; }

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
            _gameService = serviceProvider.GetRequiredService<IGameService>();
            _roundService = serviceProvider.GetRequiredService<IRoundService>();

            PlayerNames = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                PlayerNames.Add(GenerateName(5));
            }

        }

        //[Fact]
        public async void CreateGames()
        {
            Random r = new Random();
            var gamesNumer = 0;
            Console.WriteLine("Creating 100 games");
            Console.WriteLine();
            Console.WriteLine();
            while (gamesNumer < 100)
            {
                gamesNumer++;

                Console.WriteLine($"Start the {gamesNumer} game ...");
                Console.WriteLine();

                await CreatePlayer(1);
                await CreatePlayer(2);
                Game game;
                try
                {
                    game = await _gameService.AddAsync(new Game
                    {
                        EndGame = false,
                        PlayerGameWinnerName = ""
                    });


                    Console.WriteLine($"Game {game.Id} Created Succefully");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex}");
                    continue;
                }
               

                var numberRound = 1;
                var player1 = await _playerService.GetPlayerByNumber(1);
                var player2 = await _playerService.GetPlayerByNumber(2);

                while(true)
                {
                    Console.WriteLine($"Round {numberRound}");
                    numberRound++;
                    Round round;
                    try
                    {
                        var player1Move = (Move)r.Next(1, 4);
                        round = await _roundService.AddAsync(new Round
                        {
                            Player1Move = player1Move,
                            GameId = game.Id
                        });
                        Console.WriteLine($"{player1.PlayerName} played {player1Move}");
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error with Player 1 move: {ex}");
                        break;
                    }

                    try
                    {
                        Console.WriteLine(round.Id);
                        var player2Move = (Move)r.Next(1, 4);

                        var rnd = await _roundService.SingleOrDefaultAsync(round.Id);
                        if(rnd != null)
                        {
                            game = await _roundService.CompleteRound(rnd, player2Move);
                        }

                        Console.WriteLine($"{player2.PlayerName} played {player2Move}");
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error with Player 2 move: {ex}");
                        break;
                    }

                    if (game.EndGame)
                    {
                        Console.WriteLine($"Game {gamesNumer} winner is {game.PlayerGameWinnerName}");
                        Console.WriteLine();
                        break;
                    }
                }

            }
            Console.WriteLine("Statics for players");
            foreach (var name in PlayerNames)
            {
                try
                {
                    var result = await _gameService.Statistics(name);
                    Console.WriteLine($"{name} won {result.Count} games");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex}");
                    continue;
                }}
            Console.WriteLine("Test Finish");
        }

        private static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        private async Task CreatePlayer(int PlayerNumber)
        {
            Console.WriteLine($"Creating Player {PlayerNumber} ...");
            Random r = new Random();
            try
            {
                await _playerService.AddPlayer(new Player
                {
                    PlayerName = PlayerNames[r.Next(PlayerNames.Count)],
                    PlayerNumber = PlayerNumber,
                });

                Console.WriteLine($"Player {PlayerNumber} created succefully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
    }
}
