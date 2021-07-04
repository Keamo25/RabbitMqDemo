using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "parentQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var ReceivedName = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Hello {ReceivedName}, I am your father!”");

                };
                channel.BasicConsume(queue: "parentQueue",
                                     autoAck: true,
                                     consumer: consumer);

            }
        }
    }
}
