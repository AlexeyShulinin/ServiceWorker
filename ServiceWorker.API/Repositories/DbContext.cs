using System;
using System.Collections.Generic;
using ServiceWorker.API.Models;

namespace ServiceWorker.API.Repositories;

public class DbContext
{
    public DbContext()
    {
        OnConfigure();
    }

    public List<User> Users { get; set; } = new();
    public List<Income> Incomes { get; set; } = new();
    public List<Outcome> Outcomes { get; set; } = new();

    private void OnConfigure()
    {
        var userFaker = new UserFaker();

        Users = userFaker.Generate(10);

        foreach (var user in Users)
        {
            var incomeFaker = new IncomeFaker(user.Id);
            var outcomeFaker = new OutcomeFaker(user.Id);

            var userIncomes = incomeFaker.Generate(new Random().Next(0, 20));
            var userOutcomes = outcomeFaker.Generate(new Random().Next(0, 10));

            user.Incomes.AddRange(userIncomes);
            user.Outcomes.AddRange(userOutcomes);
            
            Incomes.AddRange(userIncomes);
            Outcomes.AddRange(userOutcomes);
        }
    }
}