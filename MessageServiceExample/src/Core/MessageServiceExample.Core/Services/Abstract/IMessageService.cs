using MessageServiceExample.Core.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Services.Abstract
{
    public interface IMessageService
    {
        Task<bool> SendMailAsync(SendMailModel model);
    }
}
