using GameStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
