using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Core.src.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecommerce.WebAPI.src.Database
{
        public class TimeStampInterceptor : SaveChangesInterceptor
    {
     public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var changedData = eventData.Context!.ChangeTracker.Entries(); // give collections of all entities experiencing the changes: Added or Updated, Deleted
            var updatedEntries = changedData.Where(entity => entity.State == EntityState.Modified);
            var addedEntries = changedData.Where(entity => entity.State == EntityState.Added);

            foreach (var e in updatedEntries)
            {
                if (e.Entity is BaseEntity entity)
                {
                    entity.UpdatedAt = DateTime.Now;
                }
            }

            foreach (var e in addedEntries)
            {
                if (e.Entity is BaseEntity entity)
                {
                    entity.UpdatedAt = DateTime.Now;
                    entity.CreatedAt = DateTime.Now;
                }
            }
            return await base.SavingChangesAsync(eventData, result);
        }

        /*
        public override  InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var changedData = eventData.Context!.ChangeTracker.Entries(); // give collections of all entities experiencing the changes: Added or Updated, Deleted
            var updatedEntries = changedData.Where(entity => entity.State == EntityState.Modified);
            var addedEntries = changedData.Where(entity => entity.State == EntityState.Added);

            foreach (var e in updatedEntries)
            {
                if (e.Entity is BaseEntity entity)
                {
                    entity.UpdatedAt = DateTime.Now;
                }
            }

            foreach (var e in addedEntries)
            {
                if (e.Entity is BaseEntity entity)
                {
                    entity.UpdatedAt = DateTime.Now;
                    entity.CreatedAt = DateTime.Now;
                }
            }
            return base.SavingChanges(eventData, result);
        }
        */
    }

}