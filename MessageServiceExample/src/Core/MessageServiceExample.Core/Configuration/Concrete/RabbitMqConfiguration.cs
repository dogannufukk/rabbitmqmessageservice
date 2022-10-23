using MessageServiceExample.Core.Configuration.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Configuration.Concrete
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public IConfiguration Configuration { get; }
        public RabbitMqConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string HostName => Configuration.GetSection("RabbitMqConfig:HostName").Value;

        public string UserName => Configuration.GetSection("RabbitMqConfig:UserName").Value;

        public string Password => Configuration.GetSection("RabbitMqConfig:Password").Value;
    }
}
