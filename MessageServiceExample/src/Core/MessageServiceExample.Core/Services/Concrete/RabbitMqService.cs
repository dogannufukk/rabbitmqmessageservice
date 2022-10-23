using MessageServiceExample.Core.Configuration.Abstract;
using MessageServiceExample.Core.Services.Abstract;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Services.Concrete
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;
        public RabbitMqService(IRabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
        }
        public IConnection GetConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMqConfiguration.HostName,
                    UserName = _rabbitMqConfiguration.UserName,
                    Password = _rabbitMqConfiguration.Password
                };
                // Automatically connect
                factory.AutomaticRecoveryEnabled = true;
                // Retry per 10 sec. when connection failed
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);
                // Consume from queue disable
                factory.TopologyRecoveryEnabled = false;

                return factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IModel GetModel(IConnection connection)
        {
            return connection.CreateModel();
        }
    }
}
