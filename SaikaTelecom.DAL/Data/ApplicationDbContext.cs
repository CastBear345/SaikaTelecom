using SaikaTelecom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SaikaTelecom.DAL.Configurations;

namespace SaikaTelecom.DAL.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Sale> Sales => Set<Sale>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SaleConfiguration());
    }
}
