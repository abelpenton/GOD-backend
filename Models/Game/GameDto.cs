using System;
using System.Collections.Generic;

namespace backend.src.GOD.Api.Models
{
    public class GameDto
    {

        public int Id {get; set; }
        public string PlayerGameWinnerName { get; set; }

        public bool EndGame { get; set; }

        public IEnumerable<RoundDto> Rounds { get; set; }
    }
}
