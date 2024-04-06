namespace SaikaTelecom.DAL.Interceptors;

public class DateInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

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
