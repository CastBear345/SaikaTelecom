namespace SaikaTelecom.DAL.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Lead> Leads => Set<Lead>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new StatusInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LeadConfiguration());
    }
}
