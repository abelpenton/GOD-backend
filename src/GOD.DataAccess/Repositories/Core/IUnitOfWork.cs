using System;
using System.Threading.Tasks;

namespace backend.src.GOD.DataAccess.Repositories.Core
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves changes to the underlying store asynchronously
        /// </summary>
        /// <returns>The number of afected objects</returns>
        Task<int> SaveChangesAsync();
    }
}
