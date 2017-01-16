using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ValueObjectSerialization
{
    public class ReflectionSerializable : ISerializable
    {
        private readonly object _obj;

        public ReflectionSerializable(object obj)
        {
            _obj = obj;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var props = _obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead)
                .ToList();
            
            props.ForEach(x => info.AddValue(x.Name, x.GetValue(_obj)));
        }

        public SerializationInfo SerializationInfo
        {
            get
            {
                var info = new SerializationInfo(_obj.GetType(), new FormatterConverter());
                new ReflectionSerializable(_obj).GetObjectData(info, new StreamingContext());
                return info;
            }
        }
    }
}
