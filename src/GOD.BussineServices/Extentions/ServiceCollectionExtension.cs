using backend.src.GOD.BussineServices.Core;
using backend.src.GOD.BussineServices.Services.Game;
using backend.src.GOD.BussineServices.Services.Player;
using backend.src.GOD.BussineServices.Services.Round;
using Microsoft.Extensions.DependencyInjection;

namespace backend.src.GOD.BussineServices.Extentions
{
    public static class ServiceCollectionExtension
    {
        public static void AddBussinesServices(this IServiceCollection service)
        {
            service.AddScoped<IGameService, GameService>();
            service.AddScoped<IRoundService, RoundService>();
            service.AddScoped<IPlayerService, PlayerService>();
        }
    }
}
