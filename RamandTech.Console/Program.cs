using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RamandTech.MessageBroker;
using RamandTech.MessageBroker.Rabbitmq;


IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();


var builder = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<IRabbitMQProducer, RabbitMQProducer>()
    .BuildServiceProvider();


//consumer

var factory = new ConnectionFactory { HostName = configuration["RabbitMQ:HostName"] };

IConnection? connection = null;
try
{

    connection = factory.CreateConnection();

}
catch (BrokerUnreachableException ex)
{
    Console.WriteLine($"RabbitMQ connection failed , An error occurred: {ex.Message}");

    Environment.Exit(0);

}

Console.WriteLine("RabbitMQ connection started...");


using var channel = connection.CreateModel();


Console.WriteLine("Start Consuming...");

try
{

    var consumer = new EventingBasicConsumer(channel);

    channel.BasicConsume(queue: configuration["RabbitMQ:ConsumeQueue"],
                         autoAck: true,
                         consumer: consumer);


    var rabbitMq = builder.GetRequiredService<IRabbitMQProducer>();

    var queueArgs = new Dictionary<string, object> { { "x-message-ttl", 10000 } };

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($" [x] Received {message} from queue");

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        rabbitMq.SendUserMessage(message, configuration["RabbitMQ:ProdeuceQueue"], queueArgs);
        Console.WriteLine($" [x] Send  {message} To another queue with TTL=10s");

        Console.ResetColor();
    };

}
catch (Exception)
{

    throw;
}

Console.ReadLine();

