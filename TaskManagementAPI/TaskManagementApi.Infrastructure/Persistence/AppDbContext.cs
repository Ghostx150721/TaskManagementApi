using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Domain.Entities;

namespace TaskManagementApi.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(entity => entity.Description)
                      .HasMaxLength(500);

                entity.Property(entity => entity.Status)
                      .IsRequired();

                entity.Property(entity => entity.CreatedAt)
                      .IsRequired();
            });
        }
    }
}
