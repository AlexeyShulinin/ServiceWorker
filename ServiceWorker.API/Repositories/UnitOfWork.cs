using ServiceWorker.API.Repositories.Implementations;
using ServiceWorker.API.Repositories.Interfaces;

namespace ServiceWorker.API.Repositories;

public class UnitOfWork
{
    private IUsersRepository usersRepository;
    private IIncomesRepository incomesRepository;
    private IOutcomesRepository outcomesRepository;

    private readonly DbContext dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IUsersRepository UsersRepository => usersRepository ??= new UsersRepository(dbContext);
    public IIncomesRepository IncomesRepository => incomesRepository ??= new IncomesRepository(dbContext);
    public IOutcomesRepository OutcomesRepository => outcomesRepository ??= new OutcomesRepository(dbContext);
}