using Microsoft.EntityFrameworkCore;
using CarShowRoom.Models;

namespace CarShowRoom.Db
{
    public class CRMContext : DbContext
    {
        public CRMContext(DbContextOptions<CRMContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
    }
}