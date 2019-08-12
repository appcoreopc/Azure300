

using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

namespace Consumer
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            //string EventHubConnectionString = args[0];
            //string EventHubName = args[1];
            string EventHubConnectionString = "Endpoint=sb://sbehnspmtmt9filevalidator.servicebus.windows.net/;SharedAccessKeyName=ehpmtevtmt9validated-listen;SharedAccessKey=9MrR5pTKIRmjTUbqOZWTU719yi+Nje0e8lD/dCq0L3A=;EntityPath=ehpmtevtmt9validated";
            string EventHubName = "ehpmtevtmt9validated";

            string StorageContainerName = "logsdata";
            string StorageAccountName = "datastoreeventhub";
            string StorageAccountKey = "aU/QBn63Cn2XQZkfOZdHGjoyU73bWAIlAk+2S1+yg/NMUj2kNcdfgjCIf1uUgddleB6lMneYowaNFx4lgXh0/w==";

            string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);
            
            var eventProcessorHost = new EventProcessorHost(
                    EventHubName,
                    PartitionReceiver.DefaultConsumerGroupName,
                    EventHubConnectionString,
                    StorageConnectionString,
                    StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }

    }
    
    public class SimpleEventProcessor : IEventProcessor
    {
        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"Processor Shutting Down. Partition '{context.PartitionId}', Reason: '{reason}'.");
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"SimpleEventProcessor initialized. Partition: '{context.PartitionId}'");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine($"Error on Partition: {context.PartitionId}, Error: {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var eventData in messages)
            {
                var data = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                Console.WriteLine($"Message received. Partition: '{context.PartitionId}', Data: '{data}'");
            }

            return context.CheckpointAsync();
        }
    }
}

