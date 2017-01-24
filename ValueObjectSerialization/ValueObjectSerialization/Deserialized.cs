using System;
using System.Runtime.Serialization;

namespace ValueObjectSerialization
{
    public sealed class Deserialized<T> : IFactory<T>
    {
        private readonly SerializationInfo _info;
        private readonly IConverter _valueConverter;

        public Deserialized(SerializationInfo info) 
            : this(info, new DefaultConverter()) { }

        public Deserialized(SerializationInfo info, IConverter valueConverter)
        {
            _info = info;
            _valueConverter = valueConverter;
        }

        public T Create()
        {
            var obj = new SerializationFactory<T>().Create();
            foreach (var entry in _info)
                if (obj.GetType().GetProperty(entry.Name) != null)
                    new PublicPropertyWriter(obj, entry.Name,
                        GetValue(entry.Value, obj.GetType().GetProperty(entry.Name).PropertyType)).Write();
            return obj;
        }

        private object GetValue(object value, Type toType)
        {
            return _valueConverter.Convert(value, toType);
        }
    }
}
