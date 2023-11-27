using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.Models.Models
{
    public class RabbitMQClient
    {
        private string _username = "admin";
        private string _password = "password";
        private string _endpoint;
        private readonly ILogger _logger;
        public RabbitMQClient(IHostingEnvironment env, ILogger logger) {
            string environment = env.EnvironmentName;
            _logger = logger;
            if (environment == "Development")
            {
                _endpoint = "localhost";
            }
            else
            {
                _endpoint = "rabbitmq";
            }
        
        }

        public void PublishMessage(string fileName)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = _username,
                Password = _password,
                HostName = _endpoint
            };

            try
            {

                var connection = connectionFactory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(
                    queue: "paperless",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                string message = fileName;

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "paperless",
                                     basicProperties: null,
                                     body: body);
                

            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
        }
        

        //Main entry point to the RabbitMQ .NET AMQP client
    }
}
