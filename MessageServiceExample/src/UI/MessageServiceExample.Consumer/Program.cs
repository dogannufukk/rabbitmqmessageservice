
using MessageServiceExample.Core.Configuration.Abstract;
using MessageServiceExample.Core.Configuration.Concrete;
using MessageServiceExample.Core.Helper.Abstract;
using MessageServiceExample.Core.Helper.Concrete;
using MessageServiceExample.Core.Services.Abstract;
using MessageServiceExample.Core.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine($"RabbitMQ - Consumer servis başlatıldı. Başlama Zamanı: {DateTime.Now}", ConsoleColor.Red);

var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("settings.json");
var configuration = builder.Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<IRabbitMqService, RabbitMqService>()
    .AddSingleton<IMessageService, MessageService>()
    .AddSingleton<IPublisherService, PublisherService>()
    .AddSingleton<IObjectConvertFormat, ObjectConvertFormat>()
    .AddSingleton<IMailConfiguration, MailConfiguration>()
    .AddSingleton<IRabbitMqConfiguration, RabbitMqConfiguration>()
    .AddSingleton<IConsumerService,ConsumerService>()
    .BuildServiceProvider();

var consumerService = serviceProvider.GetService<IConsumerService>();
await consumerService.Start();

Console.ReadLine();
