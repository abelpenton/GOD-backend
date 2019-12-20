using System;
using backend.src.GOD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.src.GOD.DataAccess.Context
{
    public class GODDataContext : DbContext
    {
        public GODDataContext(DbContextOptions<GODDataContext> dbContext) : base(dbContext)
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Round> Rounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Round>()
                .HasOne(r => r.Game)
                .WithMany(g => g.Rounds)
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
              .HasIndex(g => g.PlayerGameWinnerName);
        }
    }
}
