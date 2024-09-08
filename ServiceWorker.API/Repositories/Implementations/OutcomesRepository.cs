using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ServiceWorker.API.Models;
using ServiceWorker.API.Repositories.Interfaces;

namespace ServiceWorker.API.Repositories.Implementations;

public class OutcomesRepository(DbContext dbContext) : IOutcomesRepository
{
    public Task<List<Outcome>> OutcomesAsync(int userId, CancellationToken cancellationToken = default)
        => Task.FromResult(dbContext.Outcomes.Where(x => x.UserId == userId).ToList());
}