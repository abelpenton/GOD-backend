﻿using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Repositories.Core;
namespace backend.src.GOD.DataAccess.Repositories.GODRepositories.Player
{
    public interface IPlayerRepository : IBaseRepository<Domain.Models.Player, int>
    {
           Task<Domain.Models.Player> FilterPlayerByNumber(int playerNumer);
    }
}
