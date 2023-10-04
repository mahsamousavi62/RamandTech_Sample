namespace RamandTech.MessageBroker.Rabbitmq;

public interface IRabbitMQProducer
{
    void SendUserMessage(string message,string queue , Dictionary<string, object>? args); 
 } 

