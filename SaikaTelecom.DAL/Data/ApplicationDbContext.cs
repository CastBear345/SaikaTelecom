namespace SaikaTelecom.DAL.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<User> Users => Set<User>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new DateInterceptor());
        optionsBuilder.AddInterceptors(new StatusInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new SaleConfiguration());
        modelBuilder.ApplyConfiguration(new LeadConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
