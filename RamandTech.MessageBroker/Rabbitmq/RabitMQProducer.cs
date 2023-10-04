namespace RamandTech.MessageBroker.Rabbitmq;

using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

public class RabbitMQProducer : IRabbitMQProducer
{
    private readonly IConfiguration Configuration;

    public string? Exchange { get; }
    public string? HostName { get; }

    public RabbitMQProducer(IConfiguration configuration)
    {
        Configuration = configuration;

        Exchange = Configuration["RabbitMq:Exchange"];
        HostName = Configuration["RabbitMq:HostName"];

    }

    public void SendUserMessage(string message, string queue, Dictionary<string, object>? args)
    {
        var factory = new ConnectionFactory
        {
            HostName = HostName
        };

        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(Exchange, "direct");

        channel.QueueDeclare(queue, true, false, false, args);
        channel.QueueBind(queue, Exchange, queue);

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(Exchange, routingKey: queue, body: body);
    }



}

