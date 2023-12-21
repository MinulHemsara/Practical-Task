using Microsoft.EntityFrameworkCore;
using ptask.Models;

namespace ptask.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
    }
}
