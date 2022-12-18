using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQRabbitCmdLet
{
    public class SendQueueResult
    {
        public string QueueName { get; set; }
        public string Message { get; set; }
        public string MQHost { get; set; } = "localhost";
        public bool Durable { get; set; } = false;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
        public IDictionary<string, object> Arguments { get; set; } = null;
        public Dictionary<string, object> BasicProperties { get; set; } = null;

        public string SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = MQHost };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName,
                                     durable: Durable,
                                     exclusive: Exclusive,
                                     autoDelete: AutoDelete,
                                     arguments: null);

                string _message = Message;
                var body = Encoding.UTF8.GetBytes(_message);

                var basicProperties = channel.CreateBasicProperties();
                basicProperties.Persistent = Durable;
                
                channel.BasicPublish(exchange: "",
                                     routingKey: QueueName,
                                     basicProperties: basicProperties,
                                     body: body);
                string result = String.Format(" [x] Sent {0}", _message);

                return result;
            }
        }
    }
}
