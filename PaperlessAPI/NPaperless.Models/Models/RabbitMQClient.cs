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
                endpoint = "localhost:5672";
            }
            else
            {
                endpoint = "rabbitmq:5672";
            }
        
        }

        public void CreateExchange()
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
                var model = connection.CreateModel();
                Console.WriteLine("Creating Exchange");
                // Create Exchange
                model.ExchangeDeclare("paperlessExchange", ExchangeType.Direct);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        

        //Main entry point to the RabbitMQ .NET AMQP client
    }
}
