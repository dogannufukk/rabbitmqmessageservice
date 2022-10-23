using MessageServiceExample.Core.Consts;
using MessageServiceExample.Core.Helper.Abstract;
using MessageServiceExample.Core.Services.Abstract;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageServiceExample.Core.Models.RequestModels;

namespace MessageServiceExample.Core.Services.Concrete
{
    public class ConsumerService : IConsumerService
    {
        private SemaphoreSlim _semaphore;
        // eventler - olaylar
        public event EventHandler<SendMailModel> MessageReceived;
        public event EventHandler<bool> MessageProcessed;

        private EventingBasicConsumer _consumer;
        private IModel _channel;
        private IConnection _connection;

        private readonly IRabbitMqService _rabbitMQServices;
        private readonly IObjectConvertFormat _objectConvertFormat;
        private readonly IMessageService _messageService;

        public ConsumerService(
            IRabbitMqService rabbitMQServices,
            IMessageService messageService,
            IObjectConvertFormat objectConvertFormat
            )
        {
            _rabbitMQServices = rabbitMQServices;
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _objectConvertFormat = objectConvertFormat;
        }

        public async Task Start()
        {
            try
            {
                _semaphore = new SemaphoreSlim(RabbitMqConsts.ParallelThreadsCount);
                _connection = _rabbitMQServices.GetConnection();
                _channel = _rabbitMQServices.GetModel(_connection);
                _channel.QueueDeclare(queue: RabbitMqConsts.RabbitMqConstsList.QueueNameEmail.ToString(),
                                     durable: true,
                                    exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                _channel.BasicQos(0, RabbitMqConsts.ParallelThreadsCount, false);
                _consumer = new EventingBasicConsumer(_channel);
                _consumer.Received += Consumer_Received;
                await Task.FromResult(_channel.BasicConsume(queue: RabbitMqConsts.RabbitMqConstsList.QueueNameEmail.ToString(),
                                           autoAck: false,
                                           /* autoAck: bir mesajı aldıktan sonra bunu anladığına       
                                              dair(acknowledgment) kuyruğa bildirimde bulunur ya da timeout gibi vakalar oluştuğunda 
                                              mesajı geri çevirmek(Discard) veya yeniden kuyruğa aldırmak(Re-Queue) için dönüşler yapar*/
                                           consumer: _consumer));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message.ToString());
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            try
            {
                _semaphore.Wait();
                SendMailModel message = _objectConvertFormat.JsonToObject<SendMailModel>(Encoding.UTF8.GetString(ea.Body.ToArray()));
                MessageReceived?.Invoke(this, message);
                // E-Posta akışını başlatma yeri
                Task.Run(() =>
                {
                    try
                    {
                        var task = _messageService.SendMailAsync(message);
                        task.Wait();
                        var result = task.Result;
                        MessageProcessed?.Invoke(this, result);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.InnerException.Message.ToString());
                    }
                    finally
                    {
                        // Teslimat Onayı
                        _channel.BasicAck(ea.DeliveryTag, false);
                        // akışı - thread'i serbest bırakıyoruz ek thread alabiliriz.
                        _semaphore.Release();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message.ToString());
            }
        }



        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            _channel.Dispose();
            //_connection.Dispose();
            _semaphore.Dispose();
        }

    }
}
