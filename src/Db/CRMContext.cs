using Microsoft.EntityFrameworkCore;
using CarShowRoom.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarShowRoom.Db
{
    public class CRMContext : IdentityDbContext<ApplicationUser>
    {
        public CRMContext(DbContextOptions<CRMContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarModel> CarModels { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<CarColor> CarColors { get; set; }

        public DbSet<PartType> PartTypes { get; set; }

        public DbSet<Depot> Depots{ get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<TestDrive> TestDrives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Depot>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Partner>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<PartType>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<PartType>().Property(x => x.Article).IsRequired();
            modelBuilder.Entity<PartType>().Property(x => x.Price).IsRequired();

            modelBuilder.Entity<Service>().Property(x=>x.Name).IsRequired();
            modelBuilder.Entity<Service>().Property(x=>x.Price).IsRequired();
                    
            modelBuilder.Entity<Vendor>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<CarColor>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<CarModel>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<CarModel>().HasOne(x => x.Vendor).WithMany().HasForeignKey(x=>x.VendorId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<Client>().Property(x => x.LastName).IsRequired();
            modelBuilder.Entity<Client>().Property(x => x.Phone).IsRequired();

            modelBuilder.Entity<Car>().Property(x => x.VIN).IsRequired();
            modelBuilder.Entity<Car>().Property(x => x.Year).IsRequired();
            modelBuilder.Entity<Car>().Property(x => x.Price).IsRequired();
            modelBuilder.Entity<Car>().HasOne(x => x.Color).WithMany().HasForeignKey(x => x.ColorId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            modelBuilder.Entity<Car>().HasOne(x => x.CarModel).WithMany().HasForeignKey(x => x.CarModelId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            modelBuilder.Entity<Car>().HasOne(x => x.Partner).WithMany().HasForeignKey(x => x.PartnerId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            modelBuilder.Entity<Car>().HasOne(x => x.Depot).WithMany().HasForeignKey(x => x.DepotId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Car>().HasOne(x => x.Client).WithMany(x=>x.Cars).HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().HasOne(x => x.Car).WithMany(x => x.Orders).HasForeignKey(x => x.CarId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<Order>().HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<Order>().HasMany(x => x.Parts).WithOne(x=>x.Order).OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<PartOrderItem>().HasOne(x => x.PartType).WithMany().OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<Order>().HasMany(x => x.Services).WithOne(x => x.Order).OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<ServiceOrderItem>().HasOne(x => x.Service).WithMany().OnDelete(DeleteBehavior.Cascade).IsRequired();

            modelBuilder.Entity<Bill>().HasOne(x => x.Order).WithMany(x => x.Bills).HasForeignKey(x=>x.OrderId).OnDelete(DeleteBehavior.Cascade).IsRequired();

            modelBuilder.Entity<TestDrive>().HasOne(x => x.Car).WithMany().OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.CarId).IsRequired();
            modelBuilder.Entity<TestDrive>().HasOne(x => x.Client).WithMany().OnDelete(DeleteBehavior.Cascade).HasForeignKey(x => x.ClientId).IsRequired();
        }

    }
}