using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
//TODO: complete
namespace MQRabbitCmdLet
{
    public class SendExchangeResult
    {
        public string ExchangeName { get; set; }
        public string Message { get; set; }
        public string MQHost { get; set; } = "localhost";
        public string RoutingKey { get; set; } = "";
        public string ExType { get; set; } = "fanout";
        public IDictionary<string, object> Arguments { get; set; } = null;
        public Dictionary<string, object> BasicProperties { get; set; } = null;

        public string SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = MQHost };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExType);

                var _message = Message;
                var body = Encoding.UTF8.GetBytes(_message);
                channel.BasicPublish(exchange: ExchangeName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);

                string result = String.Format(" [x] Sent {0}", _message);

                return result;
            }

        }
    }
}

