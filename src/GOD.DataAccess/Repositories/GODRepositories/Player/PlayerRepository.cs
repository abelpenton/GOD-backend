using System.Data;
using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Context;
using backend.src.GOD.DataAccess.Repositories.Core;
using Dapper;

namespace backend.src.GOD.DataAccess.Repositories.GODRepositories.Player
{
    public class PlayerRepository : BaseRepository<Domain.Models.Player, int>, IPlayerRepository
    {
        public PlayerRepository(GODDataContext dbContext) : base(dbContext)
        {
        }

        public async Task<Domain.Models.Player> FilterPlayerByNumber(int playerNumer)
        {
            var connection = OpenConnection();

            var sql = $@"SELECT TOP(1) * FROM Players WHERE PlayerNumber = @PN";

            try
            {
                using (var query = connection.QuerySingleAsync<Domain.Models.Player>(sql, new { PN = playerNumer }))
                {
                    return await query;
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
