using System.Threading.Tasks;
using backend.src.GOD.BussineServices.Core;
using backend.src.GOD.Domain.Models;

namespace backend.src.GOD.BussineServices.Services.Round
{
    public interface IRoundService : IBaseBussineService<Domain.Models.Round, int>
    {
        Task<Domain.Models.Game> CompleteRound(Domain.Models.Round round, Move lastMove);
    }
}
