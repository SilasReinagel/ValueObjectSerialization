using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ValueObjectSerialization
{
    public class NewtonsoftDeserialized<T> : IFactory<T>
    {
        private readonly string _json;

        public NewtonsoftDeserialized(byte[] bytes)
            : this(Encoding.UTF8.GetString(bytes)) { }

        public NewtonsoftDeserialized(string json)
        {
            _json = json;
        }

        public T Create()
        {
            return JsonConvert.DeserializeObject<NewtonsoftSerializable<T>>(_json).Create();
        }

        private class NewtonsoftSerializable<T> : IFactory<T>, ISerializable
        {
            private readonly SerializationInfo _info;

            public NewtonsoftSerializable(SerializationInfo info, StreamingContext context)
            {
                _info = info;
            }

            public T Create()
            {
                var obj = new SerializationFactory<T>().Create();
                foreach (var entry in _info)
                    if (IsBasicType(entry.Value))
                        SetDeclaringObjectPropertyValueIfExists(obj, entry.Name, (JValue)entry.Value);
                    else
                        SetDeclaringObjectPropertyValueIfExists(obj, entry.Name,
                            CreateInnerObj((JObject)entry.Value, obj.GetType().GetProperty(entry.Name).PropertyType));
                return obj;
            }

            private bool IsBasicType(object value)
            {
                if (!(value is JValue))
                    return false;
                var type = ((JValue)value).Type;
                return type.Equals(JTokenType.Boolean)
                       || type.Equals(JTokenType.Bytes)
                       || type.Equals(JTokenType.Array)
                       || type.Equals(JTokenType.Date)
                       || type.Equals(JTokenType.Guid)
                       || type.Equals(JTokenType.Float)
                       || type.Equals(JTokenType.Null)
                       || type.Equals(JTokenType.String)
                       || type.Equals(JTokenType.Integer)
                       || type.Equals(JTokenType.Uri);
            }

            private object CreateInnerObj(object value, Type objType)
            {
                if (value is JObject && objType != null)
                    return ((JObject)value).ToObject(objType);
                return null;
            }

            private void SetDeclaringObjectPropertyValueIfExists(object obj, string propertyName, object value)
            {
                var currentType = obj.GetType();
                while (currentType != null)
                {
                    var prop = currentType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null && prop.CanWrite)
                    {
                        if (value.GetType() == prop.PropertyType)
                            prop.SetValue(obj, value);
                        if (value is JValue)
                            prop.SetValue(obj, Convert.ChangeType(((JValue)value).Value, prop.PropertyType, CultureInfo.InvariantCulture));
                        return;
                    }
                    currentType = currentType.BaseType;
                }
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
