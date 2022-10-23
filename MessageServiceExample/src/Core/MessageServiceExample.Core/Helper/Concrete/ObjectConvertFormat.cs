using MessageServiceExample.Core.Helper.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceExample.Core.Helper.Concrete
{
    public class ObjectConvertFormat : IObjectConvertFormat
    {
        public T JsonToObject<T>(string jsonString) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string ObjectToJson<T>(T entityObject) where T : class, new()
        {
            return JsonConvert.SerializeObject(entityObject);
        }

        public T ParseObjectDataArray<T>(byte[] rawBytes) where T : class, new()
        {
            return JsonToObject<T>(Encoding.UTF8.GetString(rawBytes));
        }
    }
}
