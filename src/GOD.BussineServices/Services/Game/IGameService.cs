using System.Collections.Generic;
using System.Threading.Tasks;
using backend.src.GOD.BussineServices.Core;
namespace backend.src.GOD.BussineServices.Services.Game
{
    public interface IGameService : IBaseBussineService<Domain.Models.Game, int>
    {
        Task<List<Domain.Models.Game>> Statistics(string playerName);
    }
}
