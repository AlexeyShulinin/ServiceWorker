using System;
using Bogus;
using ServiceWorker.API.Models;

namespace ServiceWorker.API.Repositories;

public sealed class UserFaker : Faker<User>
{
    private int userId = 1;
    public UserFaker()
    {
        RuleFor(u => u.Id, _ => userId++);
        RuleFor(u => u.FirstName, f => f.Name.FirstName());
        RuleFor(u => u.LastName, f => f.Name.LastName());
        RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
        RuleFor(u => u.Age, f => f.Random.Number(1, 100));
    }
}

public sealed class IncomeFaker : Faker<Income>
{
    public IncomeFaker(int userId)
    {
        RuleFor(u => u.Id, f => f.UniqueIndex);
        RuleFor(u => u.UserId, f => userId);
        RuleFor(u => u.Date, f => f.Date.Between(new DateTime(2020, 1, 1), DateTime.Today));
        RuleFor(u => u.Amount, f => f.Finance.Amount());
    }
}

public sealed class OutcomeFaker : Faker<Outcome>
{
    public OutcomeFaker(int userId)
    {
        RuleFor(u => u.Id, f => f.UniqueIndex);
        RuleFor(u => u.UserId, f => userId);
        RuleFor(u => u.Date, f => f.Date.Between(new DateTime(2020, 1, 1), DateTime.Today));
        RuleFor(u => u.Amount, f => f.Finance.Amount());
    }
}