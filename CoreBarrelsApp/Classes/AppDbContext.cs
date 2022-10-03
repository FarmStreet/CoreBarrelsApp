using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<CoreBarrel> CoreBarrel { get; set; }
        public DbSet<GroundCoreEntity> GroundCore { get; set; }
        public DbSet<GroundPoreEntity> GroundPore { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDb.db");
        }
    }
}
