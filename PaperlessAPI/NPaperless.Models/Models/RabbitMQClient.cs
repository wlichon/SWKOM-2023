using Microsoft.AspNetCore.Hosting;
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
        private string username = "admin";
        private string password = "password";
        private string endpoint;
        public RabbitMQClient(IHostingEnvironment env) {
            string environment = env.EnvironmentName;

            if (environment == "Development")
            {
                endpoint = "localhost";
            }
            else
            {
                endpoint = "rabbitmq";
            }
        
        }

        public void PublishMessage(string fileName)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = username,
                Password = password,
                HostName = endpoint
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
                Console.WriteLine(ex.Message);
            }
        }
        

        //Main entry point to the RabbitMQ .NET AMQP client
    }
}
