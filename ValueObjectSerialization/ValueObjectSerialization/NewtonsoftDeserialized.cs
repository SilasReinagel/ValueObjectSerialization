using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ValueObjectSerialization
{
    public sealed class NewtonsoftDeserialized<T> : IFactory<T>
    {
        private readonly string _json;

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
            private readonly Deserialized<T> _deserialized;

            // Newtonsoft deserialization constructor
            public NewtonsoftSerializable(SerializationInfo info, StreamingContext context)
                : this (new Deserialized<T>(info, new NewtonsoftConverter())) { }

            private NewtonsoftSerializable(Deserialized<T> deserialized)
            {
                _deserialized = deserialized;
            }

            public T Create()
            {
                return _deserialized.Create();
            }

            // Must implement ISerializable in order for Newtonsoft to use the correct constructor. Not to be used.
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                throw new NotImplementedException();
            }
        }

        private class NewtonsoftConverter : IConverter
        {
            public object Convert(object obj, Type toType)
            {
                if (!(obj is JToken))
                    throw new InvalidOperationException($"Can only convert Newtonsoft JToken objects. Supplied type: {toType}");
                return ((JToken)obj).ToObject(toType);
            }
        }
    }
}
