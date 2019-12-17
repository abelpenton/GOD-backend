using backend.src.GOD.DataAccess.Context;
using backend.src.GOD.DataAccess.Repositories.Core;
namespace backend.src.GOD.DataAccess.Repositories.GODRepositories.Player
{
    public class PlayerRepository : BaseRepository<Domain.Models.Player, int>, IPlayerRepository
    {
        public PlayerRepository(GODContext dbContext) : base(dbContext)
        {
        }
    }
}
