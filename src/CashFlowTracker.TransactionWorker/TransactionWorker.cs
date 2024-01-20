using CashFlowTracker.Application.Interfaces.Repository;
using CashFlowTracker.Application.Messages;
using CashFlowTracker.Domain.Entities;
using CashFlowTracker.Infra.RabbitAdapter;

namespace CashFlowTracker.Workers;
public class TransactionWorker : BackgroundService
{
    private readonly ILogger<TransactionWorker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly RabbitMQConsumer<TransactionCreatedMessage> _consumer;

    public TransactionWorker(ILogger<TransactionWorker> logger, IServiceScopeFactory serviceScopeFactory, RabbitMQConsumer<TransactionCreatedMessage> consumer)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _consumer = consumer;

        // Configuração do processamento da mensagem
        _consumer.ProcessMessageAsync = ProcessMessageAsync;
        _consumer.ProcessErrorAsync = ProcessErrorAsync;
    }

    private async Task ProcessMessageAsync(BaseMessage message)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var transactionRepository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();

            try
            {
                switch (message)
                {
                    case TransactionCreatedMessage createdMessage:
                        await ProcessCreateTransaction(createdMessage, transactionRepository);
                        break;
                    case TransactionUpdatedMessage updatedMessage:
                        await ProcessUpdateTransaction(updatedMessage, transactionRepository);
                        break;
                    case TransactionDeletedMessage deletedMessage:
                        await ProcessDeleteTransaction(deletedMessage.Id, transactionRepository);
                        break;
                        // Adicione mais cases conforme necessário
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar a mensagem");
            }
        }
    }

    public async Task<Task> ProcessCreateTransaction(TransactionCreatedMessage message, ITransactionRepository transactionRepository)
    {
        try
        {
            var transaction = new Transaction
            {
                AccountId = message.AccountId,
                Amount = message.Amount,
                Type = message.Type,
                TransactionDate = message.TransactionDate,
                Description = message.Description,
            };

            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveChangesAsync();

            _logger.LogInformation("Transação criada com sucesso: {TransactionId}", transaction.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a mensagem");
        }
        return Task.CompletedTask;
    }

    public async Task<Task> ProcessUpdateTransaction(TransactionUpdatedMessage message, ITransactionRepository transactionRepository)
    {
        try
        {
            var transaction = new Transaction
            {
                AccountId = message.AccountId,
                Amount = message.Amount,
                Type = message.Type,
                TransactionDate = message.TransactionDate,
                Description = message.Description,
            };

            transactionRepository.Update(transaction);
            await transactionRepository.SaveChangesAsync();

            _logger.LogInformation("Transação atualizada com sucesso: {TransactionId}", transaction.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a mensagem");
        }
        return Task.CompletedTask;
    }

    public async Task<Task> ProcessDeleteTransaction(Guid deletedId, ITransactionRepository transactionRepository)
    {
        try
        {
            var transaction = new Transaction
            {
                Id = deletedId
            };

            transactionRepository.Update(transaction);
            await transactionRepository.SaveChangesAsync();

            _logger.LogInformation("Transação excluída com sucesso: {TransactionId}", transaction.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a mensagem");
        }
        return Task.CompletedTask;
    }

    public async Task<Task> ProcessGetTransaction(Guid getId, ITransactionRepository transactionRepository)
    {
        try
        {
            var transaction = new Transaction
            {
                Id = getId
            };

            transactionRepository.FindAllAsync(BaseMessage => BaseMessage.Id == getId);
            await transactionRepository.SaveChangesAsync();

            _logger.LogInformation("Transação obtido com sucesso: {TransactionId}", transaction.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar a mensagem");
        }
        return Task.CompletedTask;
    }

    private Task ProcessErrorAsync(Exception exception)
    {
        _logger.LogError(exception, "Erro ao consumir a mensagem");
        return Task.CompletedTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TransactionWorker iniciado às: {time}", DateTimeOffset.Now);

        // Iniciar o processamento de mensagens
        await _consumer.StartProcessingAsync(stoppingToken);

        // Manter o worker em execução
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken); // Aguardar algum tempo (exemplo: 1 segundo)
        }

        _logger.LogInformation("TransactionWorker finalizado às: {time}", DateTimeOffset.Now);
    }
}
