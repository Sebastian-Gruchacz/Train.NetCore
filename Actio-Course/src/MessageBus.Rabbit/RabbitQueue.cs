using System;
using Actio.Common.ServiceBus;
using RawRabbit;

namespace MessageBus.Rabbit
{
    public class RabbitQueue : IMsgBus
    {
        private readonly IBusClient _rabbitMq;
    }
}
