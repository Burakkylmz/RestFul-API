

using Microsoft.EntityFrameworkCore;
using RestFul_API.Infrastructure.Entities.Concrete;


namespace RestFul_API.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
    }
}
