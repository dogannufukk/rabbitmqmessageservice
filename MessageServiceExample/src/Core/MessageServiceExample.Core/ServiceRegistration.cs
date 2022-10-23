using MessageServiceExample.Core.Configuration.Abstract;
using MessageServiceExample.Core.Configuration.Concrete;
using MessageServiceExample.Core.Helper.Abstract;
using MessageServiceExample.Core.Helper.Concrete;
using MessageServiceExample.Core.Services.Abstract;
using MessageServiceExample.Core.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core
{
    public static class ServiceRegistration
    {
        public static void AddCoreServiceResolver(this IServiceCollection services)
        {
            services.AddScoped<IRabbitMqService, RabbitMqService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IObjectConvertFormat, ObjectConvertFormat>();
            services.AddScoped<IMailConfiguration, MailConfiguration>();
            services.AddScoped<IRabbitMqConfiguration, RabbitMqConfiguration>();
            services.AddScoped<IConsumerService, ConsumerService>();
        }
    }
}
