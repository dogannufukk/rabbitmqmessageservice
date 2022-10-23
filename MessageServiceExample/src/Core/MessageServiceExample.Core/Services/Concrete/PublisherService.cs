using MessageServiceExample.Core.Consts;
using MessageServiceExample.Core.Helper.Abstract;
using MessageServiceExample.Core.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Services.Concrete
{
    public class PublisherService : IPublisherService
    {
        private readonly IRabbitMqService _rabbitMQServices;


        public PublisherService(IRabbitMqService rabbitMQServices) => _rabbitMQServices = rabbitMQServices;

        public void Enqueue<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new()
        {
            try
            {
                using (var _connection = _rabbitMQServices.GetConnection())
                using (var _channel = _rabbitMQServices.GetModel(_connection))
                {
                    _channel.QueueDeclare(
                                         queue: queueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    var properties = _channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.Expiration = RabbitMqConsts.MessagesTTL.ToString();

                    foreach (var queueDataModel in queueDataModels)
                    {
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(queueDataModel));
                        _channel.BasicPublish(exchange: "",
                                             routingKey: queueName,
                                             mandatory: false,
                                             basicProperties: properties,
                                             body: body);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message.ToString());
            }
        }
    }
}

