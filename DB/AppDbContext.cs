using Lab_4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_4.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
    }
}
