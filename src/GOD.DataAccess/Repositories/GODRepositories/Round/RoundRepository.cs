using backend.src.GOD.DataAccess.Context;
using backend.src.GOD.DataAccess.Repositories.Core;
namespace backend.src.GOD.DataAccess.Repositories.GODRepositories.Round
{
    public class RoundRepository : BaseRepository<Domain.Models.Round, int>, IRoundRepository
    {
        public RoundRepository(GODContext dbContext) : base(dbContext)
        {
        }
    }
}
