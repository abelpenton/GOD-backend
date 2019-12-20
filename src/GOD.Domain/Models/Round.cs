using System.ComponentModel.DataAnnotations;
using backend.src.GOD.Domain.Core;
namespace backend.src.GOD.Domain.Models
{
    public class Round : Entity<int>
    {

        public int GameId { get; set; }

        public Game Game { get; set; }

        public Move Player1Move { get; set; }

        public Move Player2Move { get; set; }

        [StringLength(200)]
        public string PlayerRoundWinnerName { get; set; }
    }
}
