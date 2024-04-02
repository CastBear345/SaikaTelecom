using Microsoft.EntityFrameworkCore;
using SaikaTelecom.DAL.Configurations;
using SaikaTelecom.Domain.Entities;

namespace SaikaTelecom.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<Contact> Contacts => Set<Contact>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
    }
}
