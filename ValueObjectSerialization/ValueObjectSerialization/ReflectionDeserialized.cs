using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ValueObjectSerialization
{
    public sealed class ReflectionDeserialized<T> : IFactory<T>
    {
        private readonly SerializationInfo _info;

        public ReflectionDeserialized(ReflectionSerializable serializable) 
            : this (serializable.SerializationInfo) { }

        public ReflectionDeserialized(SerializationInfo info)
        {
            _info = info;
        }

        public T Create()
        {
            var props = _info.ObjectType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var overlapping = new List<SerializationEntry>();
            foreach (var entry in _info)
                if (props.Any(x => entry.Name.Equals(x.Name)))
                    overlapping.Add(entry);

            var obj = New<T>.Instance();
            overlapping.ForEach(x => SetDeclaringObjectPropertyValue(obj, x.Name, x.Value));
            return obj;
        }

        private static void SetDeclaringObjectPropertyValue(object obj, string propertyName, object value)
        {
            var inner = obj.GetType();
            while (inner != null)
            {
                var propToSet = inner.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (propToSet != null && propToSet.CanWrite)
                {
                    propToSet.SetValue(obj, value, null);
                    return;
                }

                inner = inner.BaseType;
            }
        }
    }
}
