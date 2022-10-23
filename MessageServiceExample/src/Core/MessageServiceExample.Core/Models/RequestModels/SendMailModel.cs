using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Models.RequestModels
{
    public class SendMailModel
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
