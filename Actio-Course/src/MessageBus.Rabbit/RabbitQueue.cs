using System;
using System.Reflection;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.MessageBus;
using RawRabbit;

namespace MessageBus.Rabbit
{
    public class RabbitQueue : IMsgBus
    {
        private readonly IBusClient _rabbitBus;

        public RabbitQueue(IBusClient rabbitBus)
        {
            _rabbitBus = rabbitBus;
        }

        public Task WithCommandHandlerAsync<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            return _rabbitBus.SubscribeAsync<TCommand>(
                msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg => cfg
                    .FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));
        }

        public Task WithEventHandlerAsync<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            return _rabbitBus.SubscribeAsync<TEvent>(
                msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg => cfg
                    .FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));
        }

        public async Task PublishAsync<TMessage>(TMessage msg)
        {
            await _rabbitBus.PublishAsync(msg);
        }

        private static string GetQueueName<T>()
        {
            return $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
        }
    }
}
