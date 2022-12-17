using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQRabbitCmdLet
{
    internal class SendQueueResult
    {
        public string QueueName { get; set; }
        public string Message { get; set; }
        public string MQHost { get; set; } = "localhost";

        public string SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = MQHost };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string _message = Message;
                var body = Encoding.UTF8.GetBytes(_message);

                channel.BasicPublish(exchange: "",
                                     routingKey: QueueName,
                                     basicProperties: null,
                                     body: body);
                string result = String.Format(" [x] Sent {0}", _message);

                return result;
            }
        }
    }
}
