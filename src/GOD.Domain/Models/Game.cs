using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using backend.src.GOD.Domain.Core;
namespace backend.src.GOD.Domain.Models
{
    public class Game : Entity<int>
    {
        [StringLength(200)]
        public string PlayerGameWinnerName { get; set; }

        public bool EndGame { get; set; }

        public ICollection<Round> Rounds { get; set; }

        public Game()
        {
            this.Rounds = new Collection<Round>();
        }
    }
}
