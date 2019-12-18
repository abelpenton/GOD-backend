using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Context;
using backend.src.GOD.DataAccess.Repositories.Core;
using Dapper;

namespace backend.src.GOD.DataAccess.Repositories.GODRepositories.Player
{
    public class PlayerRepository : BaseRepository<Domain.Models.Player, int>, IPlayerRepository
    {
        public PlayerRepository(GODContext dbContext) : base(dbContext)
        {
        }

        public Task<Domain.Models.Player> FilterPlayerByNumber(int playerNumer)
        {
            //use Dapper here
            return this.SingleOrDefaultAsync(p => p.PlayerNumber == playerNumer);
        }
    }
}
