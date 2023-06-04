using Microsoft.EntityFrameworkCore;
using RestaurMap.Models.Db;

namespace RestaurMap.Data
{
	public class ApplicationDbContext : DbContext
    {
        public DbSet<Restaurant>Restaurant { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasKey(x => x.Id);
        }
    }
}