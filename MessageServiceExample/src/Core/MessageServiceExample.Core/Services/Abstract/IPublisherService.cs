using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Services.Abstract
{
    public interface IPublisherService
    {
        void Enqueue<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new();
    }
}
