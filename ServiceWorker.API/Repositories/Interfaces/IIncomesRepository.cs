using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServiceWorker.API.Models;

namespace ServiceWorker.API.Repositories.Interfaces;

public interface IIncomesRepository
{
    Task<List<Income>> IncomesAsync(int userId, CancellationToken cancellationToken = default);
}