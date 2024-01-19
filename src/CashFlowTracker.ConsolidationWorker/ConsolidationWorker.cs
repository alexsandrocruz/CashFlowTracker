using CashFlowTracker.Application.Interfaces;
using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Application.Messages;
using CashFlowTracker.Domain.Entities;
using CashFlowTracker.Infra.RabbitAdapter;

namespace CashFlowTracker.Workers;
public class ConsolidationWorker : BackgroundService
{
    private readonly ILogger<ConsolidationWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly RabbitMQConsumer<ConsolidationStartedMessage> _consumer;

    public ConsolidationWorker(ILogger<ConsolidationWorker> logger, IServiceScopeFactory serviceScopeFactory, RabbitMQConsumer<ConsolidationStartedMessage> consumer)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _consumer = consumer;

        // Configuração do processamento da mensagem
        _consumer.ProcessMessageAsync = ProcessMessageAsync;
        _consumer.ProcessErrorAsync = ProcessErrorAsync;
    }

    private async Task ProcessMessageAsync(ConsolidationStartedMessage message)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var consolidationService = scope.ServiceProvider.GetRequiredService<IConsolidationService>();
            var dailyBalanceRepository = scope.ServiceProvider.GetRequiredService<IDailyBalanceRepository>();

            try
            {
                var total = await consolidationService.ConsolidateTransactionsAsync(message.Date, message.AccountId);

                var consolidation = new DailyBalance
                {
                    AccountId = message.AccountId,
                    Date = message.Date,
                    ClosingBalance = total,
                    CreatedAt = DateTime.Now
                };

                await dailyBalanceRepository.AddAsync(consolidation);
                await dailyBalanceRepository.SaveChangesAsync();

                _logger.LogInformation("Consolidação realizada com sucesso: Conta {AccountId} | Data {Date}", consolidation.AccountId, consolidation.Date);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a mensagem");
            }
        }
    }

    private Task ProcessErrorAsync(Exception exception)
    {
        _logger.LogError(exception, "Erro ao consumir a mensagem");
        return Task.CompletedTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ConsolidationWorker iniciado às: {time}", DateTimeOffset.Now);

        // Iniciar o processamento de mensagens
        await _consumer.StartProcessingAsync(stoppingToken);

        // Manter o worker em execução
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken); // Aguardar algum tempo (exemplo: 1 segundo)
        }

        _logger.LogInformation("ConsolidationWorker finalizado às: {time}", DateTimeOffset.Now);
    }
}
