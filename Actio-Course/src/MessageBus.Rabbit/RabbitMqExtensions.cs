using System;
using System.Collections.Generic;
using System.Text;
using Actio.Common.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Instantiation;

namespace MessageBus.Rabbit
{
    public static class RabbitMqExtensions
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");

            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });

            services.AddSingleton<ImessageBus>(new RabbitQueueMessageBus(client));
        }
    }
}
