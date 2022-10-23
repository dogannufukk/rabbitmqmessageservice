using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Configuration.Abstract
{
    public interface IRabbitMqConfiguration
    {
        string HostName { get; }
        string UserName { get; }
        string Password { get; }
    }
}
