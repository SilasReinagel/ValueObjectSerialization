using System.Text;
using Newtonsoft.Json;

namespace ValueObjectSerialization
{
    public class NewtonsoftSerializedBytes : IFactory<byte[]>
    {
        private readonly object _obj;

        public NewtonsoftSerializedBytes(object obj)
        {
            _obj = obj;
        }

        public byte[] Create()
        {
            var json = JsonConvert.SerializeObject(_obj);
            return Encoding.UTF8.GetBytes(json);
        }
    }
}
