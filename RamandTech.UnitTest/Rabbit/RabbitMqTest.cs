using RamandTech.MessageBroker.Rabbitmq;

namespace RamandTech.UnitTest.Rabbit;

public class RabbitMqTest
{
    [Fact]
    public void SendUserMessage_ShouldSendMessageToQueue()
    {

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                    { "RabbitMq:HostName", "localhost" },
                    { "RabbitMq:Exchange", "RamandTech.direct" }
            })
            .Build();


        var producer = new RabbitMQProducer(configuration);

        var args = new Dictionary<string, object>
        {
            { "x-message-ttl", 10000 }
        };

        producer.SendUserMessage("Hi", "RamandTech.Users", null);
        producer.SendUserMessage("Bye", "RamandTech.Users", null);


    }



}
