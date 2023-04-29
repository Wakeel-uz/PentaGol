using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PentaGol.Domain.Entities;

namespace PentaGol.Data.DatabaseConfiguration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base()
        { }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Liga> Ligas { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

    }
}
