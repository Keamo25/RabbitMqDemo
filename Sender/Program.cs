using System;
using System;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your name below:");
            string message = Console.ReadLine();
            try
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

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "parentQueue",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($"Hello my name is, {message}");
                }
            }
            catch (Exception ex)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
