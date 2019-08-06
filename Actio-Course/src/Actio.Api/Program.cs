using System.Threading.Tasks;

using Actio.Common.Events;
using Actio.Common.Services;

using MessageBus.Rabbit;

namespace Actio.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await ServiceHost.Create<Startup>(args)
                .UseQueueImplementation<RabbitQueueMessageBus>()
                .SubscribeToEvent<ActivityCreated>()
                .Build()
                .Run();
        }
    }
}
