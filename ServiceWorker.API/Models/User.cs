using System.Collections.Generic;

namespace ServiceWorker.API.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }

    public List<Income> Incomes { get; set; } = new();
    public List<Outcome> Outcomes { get; set; } = new();
}