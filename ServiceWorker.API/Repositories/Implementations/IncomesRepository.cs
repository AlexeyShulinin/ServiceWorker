using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ServiceWorker.API.Models;
using ServiceWorker.API.Repositories.Interfaces;

namespace ServiceWorker.API.Repositories.Implementations;

public class IncomesRepository(DbContext dbContext) : IIncomesRepository
{
    public Task<List<Income>> IncomesAsync(int userId, CancellationToken cancellationToken = default)
        => Task.FromResult(dbContext.Incomes.Where(x => x.UserId == userId).ToList());
}