using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Configuration.Abstract
{
    public interface IMailConfiguration
    {
        public string Host { get;  }
        public int Port { get; }
        public string User { get; }
        public string Password { get; }
        public bool SSL { get;}
    }
}
