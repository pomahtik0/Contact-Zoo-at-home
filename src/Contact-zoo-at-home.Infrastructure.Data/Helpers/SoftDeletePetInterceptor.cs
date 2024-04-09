using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Shared.Basics.Enums;

namespace Contact_zoo_at_home.Infrastructure.Data.Helpers
{
    public sealed class SoftDeletePetInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(
                    eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<Pet>> entries =
                eventData
                    .Context
                    .ChangeTracker
                    .Entries<Pet>()
                    .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<Pet> softDeletable in entries)
            {
                softDeletable.State = EntityState.Modified;
                softDeletable.Entity.CurrentPetStatus = PetStatus.Archived;
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
