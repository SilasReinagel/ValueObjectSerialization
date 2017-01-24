using Newtonsoft.Json;

namespace ValueObjectSerialization
{
    public sealed class NewtonsoftJsonString : IFactory<string>
    {
        private readonly object _obj;

        public NewtonsoftJsonString(object obj)
        {
            _obj = obj;
        }

        public string Create()
        {
            return JsonConvert.SerializeObject(_obj);
        }
    }
}
