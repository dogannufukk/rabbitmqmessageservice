using MessageServiceExample.Core.Configuration.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Configuration.Concrete
{
    public class MailConfiguration : IMailConfiguration
    {
        public IConfiguration Configuration { get; }


        public MailConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Host => Configuration.GetSection("MailConfiguration:Host").Value;

        public int Port => Convert.ToInt32(Configuration.GetSection("MailConfiguration:Port").Value);

        public string User => Configuration.GetSection("MailConfiguration:User").Value;

        public string Password => Configuration.GetSection("MailConfiguration:Password").Value;

        public bool SSL => Convert.ToBoolean(Configuration.GetSection("MailConfiguration:SSL").Value);


    }
}
