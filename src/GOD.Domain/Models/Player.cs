using System.ComponentModel.DataAnnotations;
using backend.src.GOD.Domain.Core;
namespace backend.src.GOD.Domain.Models
{
    public class Player : Entity<int>
    {
        [Required]
        [StringLength(200)]
        public string PlayerName { get; set; }

        public int PlayerNumber { get; set; }

        public int NumberOfRoundWinner { get; set; }
    }
}
