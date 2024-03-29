using CashFlowTracker.Application.Handlers;
using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Infra.Data;
using CashFlowTracker.Infra.Data.Repository;
using CashFlowTracker.Infra.RabbitAdapter;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(typeof(RabbitMQConsumer<>)); 
builder.Services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();

builder.Services.AddDbContext<SqlServerContext>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StartConsolidationCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTransactionCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateTransactionCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteTransactionCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTransactionCommandHandler).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SqlServerContext>();
        context.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        // Log the error
    }
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
