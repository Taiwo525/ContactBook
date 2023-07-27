using Microsoft.EntityFrameworkCore;
using STAYCATION.Models;

namespace STAYCATION.Data
{
    public class StayDbContext : DbContext
    {
        public StayDbContext(DbContextOptions<StayDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Customers> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Customers>().HasData(
               //new Customers() { Id = 1, Name = "Taiwo Babatunde", Email = "ayothdav52@gmail.com", PasssWord = "Dav52@53", RepeatPassword = "Dav52@53" });
        }
    }
}
