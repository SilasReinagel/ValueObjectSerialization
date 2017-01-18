using System.Linq;
using System.Runtime.Serialization;

namespace ValueObjectSerialization
{
    public sealed class ReflectionSerializable : ISerializable
    {
        private readonly object _obj;

        public ReflectionSerializable(object obj)
        {
            _obj = obj;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            new PublicReadableProperties(_obj)
                .ToList()
                .ForEach(x => info.AddValue(x.Name, x.GetValue(_obj)));
        }

        public SerializationInfo SerializationInfo
        {
            get
            {
                var info = new SerializationInfo(_obj.GetType(), new FormatterConverter());
                GetObjectData(info, new StreamingContext());
                return info;
            }
        }
    }
}
