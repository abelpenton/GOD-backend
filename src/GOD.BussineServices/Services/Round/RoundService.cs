using backend.src.GOD.BussineServices.Core;
using backend.src.GOD.DataAccess.Repositories.Core;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Round;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;
using backend.src.GOD.Domain.Models;

namespace backend.src.GOD.BussineServices.Services.Round
{
    public class RoundService : BaseBussinesService<Domain.Models.Round, int>, IRoundService
    {
        private IRoundRepository Repository { get; set; }

        public RoundService(IRoundRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            Repository = repository;
        }
    }
}
