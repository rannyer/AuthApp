using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Shared.DTOs;

namespace UserService.Messaging;

public class RabbitMqProducer
{
    private const string QueueName = "fila-usuario-criado";
    private readonly IConfiguration _configuration;

    public RabbitMqProducer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task PublicaUsuarioCriadoAsync(
        UsuarioCriadoEventDto evento,
        CancellationToken cancellationToken = default)
    {
        var connectionUri = _configuration["RabbitMQ:Uri"]
            ?? throw new InvalidOperationException("Configure RabbitMQ:Uri com a URI do broker.");
        var factory = new ConnectionFactory { Uri = new Uri(connectionUri) };
        await using var connection = await factory.CreateConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evento));
        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: QueueName,
            body: body,
            cancellationToken: cancellationToken);
    }
}