using System;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Azure_Queues
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Queue storage sample!");

            var storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=mpstorageaccount008;AccountKey=GtrJQ1ARGV59+5M8v3P8Z87UHtSTAy6JKS+zFjTYxiR61QHqukttQey3y44sOf8Vx0jZGjXc72jSfvcpC0Ml4w==;EndpointSuffix=core.windows.net";
            var queueName = "mptestqueue";

            QueueClient queueClient = new QueueClient(storageConnectionString, queueName);

            foreach (QueueMessage message1 in queueClient.ReceiveMessages(10).Value) {
                Console.WriteLine($"Message: {message1.Body}");

                queueClient.DeleteMessage(message1.MessageId, message1.PopReceipt);
            }

            queueClient.SendMessage("MP Test Message 1"); // We can also use AddMessageAsync(message), where message is CloudQueueMessage("MP Test Message 1")

            QueueMessage message2 = queueClient.ReceiveMessage();
            Console.WriteLine($"Message: {message2.Body}");
            queueClient.UpdateMessage(message2.MessageId, message2.PopReceipt, "MP Test Message 1 - Updated");
        }
    }
}
