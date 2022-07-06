using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace AsyncMessagesUtil
{
    public static class AsyncMessaging
    {
        public static bool Publish(ConnectionFactory factory,string queue,string message)
        {

            
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

            }


            return true;
        }

    }
}
