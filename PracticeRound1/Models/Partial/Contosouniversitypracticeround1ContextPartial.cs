using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PracticeRound1.Models
{
    public partial class Contosouniversitypracticeround1Context : DbContext
    {
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries();
            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues.SetValues(new { DateModified = DateTime.Now });
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}