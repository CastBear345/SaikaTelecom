namespace SaikaTelecom.DAL.Interceptor;

public class StatusInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext == null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = dbContext.ChangeTracker.Entries<Lead>()
            .Where(e => e.State == EntityState.Added)
            .ToList();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.LeadStatus).CurrentValue = LeadStatus.New;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}