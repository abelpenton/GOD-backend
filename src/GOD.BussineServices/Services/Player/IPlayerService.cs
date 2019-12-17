using System.Threading.Tasks;
using backend.src.GOD.BussineServices.Core;
namespace backend.src.GOD.BussineServices.Services.Player
{
    public interface IPlayerService : IBaseBussineService<Domain.Models.Player, int>
    {
        Task<Domain.Models.Player> GetPlayerForNumber(int playerNumer);
    }
}
