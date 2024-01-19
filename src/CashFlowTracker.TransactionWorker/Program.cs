using CashFlowTracker.Infra.Data;
using CashFlowTracker.Infra.Data.Repository;
using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Workers;
using CashFlowTracker.Application.Messages;
using CashFlowTracker.Infra.RabbitAdapter;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Configura��o do contexto do EF Core
        services.AddDbContext<SqlServerContext>();

        // Registro do TransactionRepository
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        // Registro do RabbitMQConsumer
        // Dependendo de como o RabbitMQConsumer � utilizado, talvez ele precise ser escopo tamb�m
        services.AddSingleton<RabbitMQConsumer<TransactionCreatedMessage>>();

        // Registro do Worker como Hosted Service
        services.AddHostedService<TransactionWorker>();
    })
    .Build();

await host.RunAsync();
