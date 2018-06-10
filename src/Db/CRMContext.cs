using Microsoft.EntityFrameworkCore;
using CarShowRoom.Models;

namespace CarShowRoom.Db
{
    public class CRMContext : DbContext
    {
        public CRMContext(DbContextOptions<CRMContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Vendor> Vendors { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>().Property(x=>x.Name).IsRequired();
            modelBuilder.Entity<Service>().Property(x=>x.Price).IsRequired();

            modelBuilder.Entity<Vendor>().Property(x => x.Name).IsRequired();
        }
    }
}