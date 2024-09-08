using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ServiceWorker.API.Models;

namespace ServiceWorker.API.Repositories.Interfaces;

public interface IUsersRepository
{
    IQueryable<User> UsersQuery();
    Task<User> UserByIdAsync(int userId, CancellationToken cancellationToken = default);
}