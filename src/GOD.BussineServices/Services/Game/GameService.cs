using backend.src.GOD.BussineServices.Core;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Game;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace backend.src.GOD.BussineServices.Services.Game
{
    public class GameService : BaseBussinesService<Domain.Models.Game, int>, IGameService
    {
        private IGameRepository Repository { get; set; }

        private ILogger logger { get; set; } 

        public GameService(IGameRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            Repository = repository;

        }
    }
}
