using Microsoft.Extensions.DependencyInjection;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Game;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Player;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Round;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;
using backend.src.GOD.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace backend.src.GOD.DataAccess.Extensions
{
    /// <summary>
    ///     Contians the functionalities to add the services 
    ///     that are implemented in the DataAcces layer.
    /// </summary>
    /// <remarks>
    ///     When a service for the DI will be used, this won't
    ///     be neccessary.
    /// </remarks>
    public static class ServiceCollectionDataAccessExtension
    {
        public static void AddDataAccessServices(this IServiceCollection service)
        {
            service.AddScoped<DbContext, GODDataContext>();
            service.AddScoped<IPlayerRepository, PlayerRepository>();
            service.AddScoped<IGameRepository, GameRepository>();
            service.AddScoped<IRoundRepository, RoundRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
