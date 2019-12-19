using System;
namespace backend.src.GOD.Api.Models
{
    public class RoundDto
    {
        public int Id {get; set;}
        public int GameId { get; set; }

        public int Player1Move { get; set; }

        public int Player2Move { get; set; }

        public string PlayerRoundWinnerName { get; set; }
    }
}
