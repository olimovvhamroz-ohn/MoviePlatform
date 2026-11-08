using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MoviePlatform.Interface;

namespace MoviePlatform.Data.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private static void ApplyAudit(DbContext? context)
    {
        if (context == null) return;

        var now = DateTime.UtcNow;


        foreach (var entry in context.ChangeTracker.Entries().ToList())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is IHasCreation created)
                    created.CreatedAt = now;

                if (entry.Entity is IHasModification modified)
                    modified.UpdatedAt = now;
            }

            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is IHasModification modified)
                    modified.UpdatedAt = now;
            }

            else if (entry.State == EntityState.Deleted)
            {
                if (entry.Entity is IHasDeletion deleted)
                {
                    entry.State = EntityState.Modified;
                    deleted.IsDeleted = true;

                    if (entry.Entity is IHasModification modified)
                        modified.UpdatedAt = now;
                }
            }
        }
    }
}