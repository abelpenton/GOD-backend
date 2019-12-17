using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Repositories.Core;
using backend.src.GOD.Domain.Core;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;

namespace backend.src.GOD.BussineServices.Core
{
    public class BaseBussinesService<TEntity, TKey> : IBaseBussineService<TEntity, TKey> where TEntity : Entity<TKey>
    {

        /// <summary>
        /// Gets or Sets the Repository for handling the entity object
        /// </summary>
        public IBaseRepository<TEntity, TKey> Repository { get; set; }

        /// <summary>
        /// Gets or sets the UnitOfWork of the app
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        private bool disposedValue = false; // To detect redundant calls


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBussinesService{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IBaseRepository{TEntity,TKey}"/> for accessing to the
        /// functionalities of the DataAceess layer.</param>
        protected BaseBussinesService(IBaseRepository<TEntity, TKey> repository, IUnitOfWork unitOfWork)
        {
            this.Repository = repository;
            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="BaseBussinesService{TEntity, TKey}"/> class.
        /// </summary>
        ~BaseBussinesService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }


        /// <summary>
        ///     Release the allocated resources
        /// <remarks>
        ///     If the derived classes use objects that could
        ///     manage resources outside the context, override it
        ///     and dispose those objects
        /// </remarks>
        /// </summary>
        /// <param name="disposing">True for disposing the object; otherwise, false</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Repository.Dispose();
                }
                Repository = null;
                disposedValue = true;
            }
        }

        private async Task<TResult> CompleteTransactions<TResult>(Task<TResult> task)
        {
            var result = await task;
            await SaveChangesAsync();
            return result;

        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            var result = await Repository.AddAsync(obj);
            await SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> objs)
        {
            var result = await Repository.AddRangeAsync(objs);
            await SaveChangesAsync();
            return result;
        }

        public async Task<bool> ExistsAsync(TEntity obj)
        {
            var result = await Repository.ExistsAsync(obj);
            await SaveChangesAsync();
            return result;
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            var result = await Repository.ExistsAsync(id);
            await SaveChangesAsync();
            return result;
        }

        public async Task<bool> ExistsAsync(Func<TEntity, bool> filter)
        {
            var result = await Repository.ExistsAsync(filter);
            await SaveChangesAsync();
            return result;
        }

        public async Task<IQueryable<TEntity>> ReadAllAsync(Func<TEntity, bool> filter)
        {
            var result = await Repository.ReadAllAsync(filter);
            await SaveChangesAsync();
            return result;
        }

        public async Task RemoveAsync(Func<TEntity, bool> filter)
        {
            await Repository.RemoveAsync(filter);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity obj)
        {
            await Repository.RemoveAsync(obj);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(TKey id)
        {
            await Repository.RemoveAsync(id);
            await SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> objs)
        {
            await Repository.RemoveRangeAsync(objs);
            await SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return UnitOfWork.SaveChangesAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Func<TEntity, bool> filter)
        {
            var result = await Repository.SingleOrDefaultAsync(filter);
            await SaveChangesAsync();
            return result;
        }

        public async Task<TEntity> SingleOrDefaultAsync(TKey id)
        {
            var result = await Repository.SingleOrDefaultAsync(id);
            await SaveChangesAsync();
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            var result = await Repository.UpdateAsync(obj);
            await SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> obj)
        {
            var result = await Repository.UpdateRangeAsync(obj);
            await SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
