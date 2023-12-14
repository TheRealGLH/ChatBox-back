using Microsoft.Extensions.Hosting;

public class ConsumerHostedService : BackgroundService
{
    private readonly IRabbitMqConsumerService _consumerService;

    public ConsumerHostedService(IRabbitMqConsumerService consumerService)
    {
        _consumerService = consumerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumerService.ReadMessgaes();
    }
}