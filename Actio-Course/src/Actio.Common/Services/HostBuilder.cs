using Actio.Common.MessageBus;
using Microsoft.AspNetCore.Hosting;

namespace Actio.Common.Services
{
    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;

        private ImessageBus _bus;

        public HostBuilder(IWebHost webHost)
        {
            this._webHost = webHost;
            
        }

        public BusBuilder UseQueueImplementation<TMsgBus>() where TMsgBus : ImessageBus
        {
            this._bus = (ImessageBus)_webHost.Services.GetService(typeof(TMsgBus));

            return new BusBuilder(_webHost, _bus);
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }
}