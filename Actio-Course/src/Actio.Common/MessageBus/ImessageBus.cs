using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;

namespace Actio.Common.MessageBus
{
    //Abstraction for serving queues <- In training course using RabbitMQ, but then I will try re-implement it with our ActiveMQ
    public interface ImessageBus
    {
        Task WithCommandHandlerAsync<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;

        Task WithEventHandlerAsync<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;

        Task PublishAsync<TMessage>(TMessage msg);
    }
}