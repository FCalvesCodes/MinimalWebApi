using Microsoft.EntityFrameworkCore;
using MinimalWebApi.Models;

namespace MinimalWebApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Title).IsRequired();
                entity.Property(p => p.PublishedAt).IsRequired();
            });
        }
    }
}
