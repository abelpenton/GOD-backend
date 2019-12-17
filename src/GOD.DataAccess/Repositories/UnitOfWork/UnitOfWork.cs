using System;
using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Context;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Game;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Player;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Round;

namespace backend.src.GOD.DataAccess.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(GODContext dbContext)
        {
            DbContext = dbContext;
        }

        public GODContext DbContext { get; set; }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
