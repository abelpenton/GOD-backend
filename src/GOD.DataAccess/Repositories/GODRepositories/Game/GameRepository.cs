using System.Collections.Generic;
using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Context;
using backend.src.GOD.DataAccess.Repositories.Core;
using backend.src.GOD.Domain.Models;
using Dapper;

namespace backend.src.GOD.DataAccess.Repositories.GODRepositories.Game
{
    public class GameRepository : BaseRepository<Domain.Models.Game, int>, IGameRepository
    {
        public GameRepository(GODDataContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Domain.Models.Game>> GetStatisticPlayer(string playerName)
        {
            var connection = OpenConnection();

            var sql = $@"SELECT * FROM Games WHERE PlayerGameWinnerName = @PN";

            try
            {
                using (var query = connection.QueryAsync<Domain.Models.Game>(sql, new { PN = playerName }))
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
