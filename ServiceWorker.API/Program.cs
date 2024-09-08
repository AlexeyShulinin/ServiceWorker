using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceWorker.API.Repositories;
using ServiceWorker.API.Responses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "ServiceWorker.API";
    config.Title = "ServiceWorker.API v1";
    config.Version = "v1";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
        configurePolicy: policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "ServiceWorker.API";
        config.Path = string.Empty;
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/users", (UnitOfWork unitOfWork, CancellationToken cancellationToken) 
    => unitOfWork.UsersRepository.UsersQuery()
        .Select(x => new UserBalanceResponse(
            x.Id, 
            x.FirstName, 
            x.LastName, 
            x.Incomes.Sum(i => i.Amount) - x.Outcomes.Sum(o => o.Amount))));

app.MapGet("users/{userId}/incomes", (int userId, UnitOfWork unitOfWork, CancellationToken cancellationToken) 
    => unitOfWork.IncomesRepository.IncomesAsync(userId, cancellationToken));

app.MapGet("users/{userId}/outcomes", (int userId, UnitOfWork unitOfWork, CancellationToken cancellationToken) 
    => unitOfWork.OutcomesRepository.OutcomesAsync(userId, cancellationToken));

app.MapGet("users/{userId}/transactions", async (int userId, UnitOfWork unitOfWork, CancellationToken cancellationToken)
    =>
{
    var incomes = (await unitOfWork.IncomesRepository.IncomesAsync(userId, cancellationToken))
        .Select(x => new UserTransactionResponse(x.UserId, x.Amount, x.Date));
    var outcomes = (await unitOfWork.OutcomesRepository.OutcomesAsync(userId, cancellationToken))
        .Select(x => new UserTransactionResponse(x.UserId, -x.Amount, x.Date));
    
    return incomes.Concat(outcomes).OrderByDescending(x => x.Date);
});

app.Run();
