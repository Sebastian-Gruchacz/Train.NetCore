using System.Threading.Tasks;

using Actio.Common.Commands;
using Actio.Common.Services;

using MessageBus.Rabbit;

namespace Actio.Services.Identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await ServiceHost.Create<Startup>(args)
                .UseQueueImplementation<RabbitQueueMessageBus>()
                .SubscribeToCommand<CreateActivity>()
                .Build()
                .Run();
        }
    }
}
