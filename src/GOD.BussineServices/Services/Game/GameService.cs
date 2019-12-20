using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.src.GOD.BussineServices.Core;
using backend.src.GOD.DataAccess.Repositories.Core;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Game;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;
using backend.src.GOD.Domain.Models;
using Microsoft.Extensions.Logging;

namespace backend.src.GOD.BussineServices.Services.Game
{
    public class GameService : BaseBussinesService<Domain.Models.Game, int>, IGameService
    {
        private IGameRepository _repository;
        public GameService(IGameRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _repository = repository;
        }

        public async Task<List<Domain.Models.Game>> Statistics(string playerName)
        {
            return (await _repository.GetStatisticPlayer(playerName)).ToList();
        }
    }
}
