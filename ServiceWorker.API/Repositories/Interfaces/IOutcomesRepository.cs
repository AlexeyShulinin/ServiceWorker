using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServiceWorker.API.Models;

namespace ServiceWorker.API.Repositories.Interfaces;

public interface IOutcomesRepository
{
    Task<List<Outcome>> OutcomesAsync(int userId, CancellationToken cancellationToken);
}