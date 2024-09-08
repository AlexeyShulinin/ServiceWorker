using System;

namespace ServiceWorker.API.Models;

public class Outcome
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public decimal Amount { get; set; }
    public DateTimeOffset Date { get; set; }
    
    public string Currency => "$";
}