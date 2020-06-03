using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;
namespace ServiceBusSender
{
    class Program
    {
        //TODO - Move connection strings to secured file in project 
        const string ServiceBusConnectionString = "Endpoint=sb://az-devjonah.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=pxtosfZg/4Ad7hp7Lw2ihQpPShJ3D7zz+Xbfv8CCwGA=";
        const string QueueName = "devjonahtestqueue1";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int noOfMsgs = 50;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            await SendMessagesAsync(noOfMsgs);
            await queueClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int noOfMsgToSend)
        {           
          
            try
            {
                for (int i = 0; i < noOfMsgToSend; i++)
                {
                    //Create and send async msg to the queue
                    string msgBody = $"Message {i}";
                    var msg = new Message(Encoding.UTF8.GetBytes(msgBody));
                    Console.WriteLine($"Sending message: {msgBody}");
                    await queueClient.SendAsync(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now}:: Exception: {ex.Message}");
            }

        }
    }
}
