using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.MessageBus;
using Microsoft.AspNetCore.Hosting;

namespace Actio.Common.Services
{
    public class BusBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private readonly IMsgBus _bus;

        public BusBuilder(IWebHost webHost, IMsgBus bus)
        {
            _webHost = webHost;
            _bus = bus;
        }

        public override ServiceHost Build()
        {
            throw new System.NotImplementedException();
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_webHost.Services
                .GetService(typeof(ICommandHandler<TCommand>));

            _bus.WithCommandHandlerAsync(handler);

            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)_webHost.Services
                .GetService(typeof(IEventHandler<TEvent>));

            _bus.WithEventHandlerAsync(handler);

            return this;
        }
    }
}