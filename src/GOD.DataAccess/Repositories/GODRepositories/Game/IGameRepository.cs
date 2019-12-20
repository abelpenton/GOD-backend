using System.Collections.Generic;
using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Repositories.Core;
namespace backend.src.GOD.DataAccess.Repositories.GODRepositories.Game
{
    public interface IGameRepository : IBaseRepository<Domain.Models.Game, int>
    {
        Task<IEnumerable<Domain.Models.Game>> GetStatisticPlayer(string playerName);
    }
}
