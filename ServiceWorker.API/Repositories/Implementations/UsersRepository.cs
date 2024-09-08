using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ServiceWorker.API.Models;
using ServiceWorker.API.Repositories.Interfaces;

namespace ServiceWorker.API.Repositories.Implementations;

public class UsersRepository(DbContext dbContext) : IUsersRepository
{
    public IQueryable<User> UsersQuery() => dbContext.Users.AsQueryable();

    public Task<User> UserByIdAsync(int userId, CancellationToken cancellationToken = default)
        => Task.FromResult(dbContext.Users.FirstOrDefault(x => x.Id == userId));
}