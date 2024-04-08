namespace SaikaTelecom.DAL.Interceptor;

public class DateInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = dbContext.ChangeTracker.Entries<Contact>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .ToList();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.LastChanged).CurrentValue = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.LastChanged).CurrentValue = DateTime.UtcNow;
            }
        }

        var sales = dbContext.ChangeTracker.Entries<Sale>()
            .Where(e => e.State == EntityState.Added)
            .ToList();
        foreach (var sale in sales)
        {
            if (sale.State == EntityState.Added)
            {
                sale.Property(x => x.DateOfSale).CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
