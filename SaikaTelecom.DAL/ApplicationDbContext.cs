using Microsoft.EntityFrameworkCore;
using SaikaTelecom.Domain.Entities;

namespace SaikaTelecom.DAL;

public class ApplicationDbContext : DbContext
{

    public DbSet<User> Users => Set<User>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
