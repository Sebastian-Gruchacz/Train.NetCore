using Actio.Common.MessageBus;
using Microsoft.AspNetCore.Hosting;

namespace Actio.Common.Services
{
    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;

        private IMsgBus _bus;

        public HostBuilder(IWebHost webHost)
        {
            this._webHost = webHost;
            
        }

        public BusBuilder UseQueueImplementation<TQueue>() where TQueue : IMsgBus
        {
            this._bus = (IMsgBus)_webHost.Services.GetService(typeof(TQueue));

            return new BusBuilder(_webHost, _bus);
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }
}