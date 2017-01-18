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
            var obj = new SerializationFactory<T>().Create();
            foreach (var entry in _info)
                SetDeclaringObjectPropertyValueIfExists(obj, entry.Name, entry.Value);
            return obj;
        }

        private void SetDeclaringObjectPropertyValueIfExists(object obj, string propertyName, object value)
        {
            var currentType = obj.GetType();
            while (currentType != null)
            {
                var prop = currentType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(obj, value);
                    return;
                }
                currentType = currentType.BaseType;
            }
        }
    }
}
