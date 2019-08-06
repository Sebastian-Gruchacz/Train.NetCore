using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.MessageBus;

namespace Actio.Services.Identity.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly ImessageBus _busClient;

        public CreateActivityHandler(ImessageBus busClient)
        {
            _busClient = busClient;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Creating activity: {command.Name}");

            await _busClient.PublishAsync(
                new ActivityCreated(command.Id,
                    command.UserId,
                    command.Category,
                    command.Name,
                    command.Description,
                    DateTime.UtcNow));
        }
    }
}
