using backend.src.GOD.DataAccess.Context;
using backend.src.GOD.DataAccess.Repositories.Core;
namespace backend.src.GOD.DataAccess.Repositories.GODRepositories.Game
{
    public class GameRepository : BaseRepository<Domain.Models.Game, int>, IGameRepository
    {
        public GameRepository(GODContext dbContext) : base(dbContext)
        {
        }
    }
}
