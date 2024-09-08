namespace ServiceWorker.API.Responses;

public record UserBalanceResponse(int Id, string FirstName, string LastName, decimal? Balance)
{
    public string Currency => "$";
}