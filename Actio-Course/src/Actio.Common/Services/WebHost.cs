using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Actio.Common.Services
{
    // TODO: perhaps move it elsewhere, so common lib do not reference AspNetCore.Hosting?
    
    public class WebHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public WebHost(IWebHost webHost)
        {
            _webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
        }

        public Task Run()
        {
            throw new System.NotImplementedException();
        }
    }
}