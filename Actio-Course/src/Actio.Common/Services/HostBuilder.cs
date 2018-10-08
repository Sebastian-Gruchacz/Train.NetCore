using Actio.Common.ServiceBus;
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
            this._bus = (IMsgBus)_webHost.Services.GetService(typeof(IMsgBus));
        }

        public override ServiceHost Build()
        {
            throw new System.NotImplementedException();
        }
    }
}