using System;

namespace ServiceWorker.API.Responses;

public record UserTransactionResponse(int UserId, decimal Amount, DateTimeOffset Date)
{
    public string Currency => "$";
}