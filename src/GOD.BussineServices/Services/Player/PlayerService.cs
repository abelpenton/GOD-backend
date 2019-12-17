using System.Threading.Tasks;
using backend.src.GOD.BussineServices.Core;
using backend.src.GOD.DataAccess.Repositories.Core;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Player;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;
using backend.src.GOD.Domain.Models;

namespace backend.src.GOD.BussineServices.Services.Player
{
    public class PlayerService : BaseBussinesService<Domain.Models.Player, int>, IPlayerService
    {
        private IPlayerRepository Repository { get; set; }

        public PlayerService(IPlayerRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            Repository = repository;
        }

        public async Task<Domain.Models.Player> GetPlayerForNumber(int playerNumer)
        {
            return await this.SingleOrDefaultAsync(p => p.PlayerNumber == playerNumer);
        }
    }
}
