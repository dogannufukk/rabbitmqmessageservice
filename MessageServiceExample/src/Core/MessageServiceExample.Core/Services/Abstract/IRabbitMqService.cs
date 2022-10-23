using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Services.Abstract
{
    public interface IRabbitMqService
    {
        IConnection GetConnection();
        IModel GetModel(IConnection connection);
    }
}
