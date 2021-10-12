using MassTransit;
using MT.Domain.Models;

namespace MT.Worker.Workers
{
    public class WorkerClient : IConsumer<ClientModel>
    {
        public Task Consume(ConsumeContext<ClientModel> context)
        {
            var id = context.Message.ClientId;
            var name = context.Message.Name;

            Console.WriteLine($"Receive client: {id} - {name}");
            return Task.CompletedTask;
        }
    }
}
