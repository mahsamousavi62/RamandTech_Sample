using Microsoft.Extensions.DependencyInjection;
using RamandTech.MessageBroker.Rabbitmq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTech.Dapper
{
    public static class ServiceRegistration
    {
        public static void AddRabbit(this IServiceCollection services)
        {
            services.AddTransient<IRabbitMQProducer, RabbitMQProducer>();
           
        }
    }
}
