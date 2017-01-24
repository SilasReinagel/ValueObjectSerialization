using System.Reflection;

namespace ValueObjectSerialization
{
    public sealed class PublicPropertyWriter
    {
        private readonly object _obj;
        private readonly string _propertyName;
        private readonly object _value;

        public PublicPropertyWriter(object obj, string propertyName, object value)
        {
            _obj = obj;
            _propertyName = propertyName;
            _value = value;
        }

        public void Write()
        {
            var currentType = _obj.GetType();
            while (currentType != null)
            {
                var prop = currentType.GetProperty(_propertyName, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(_obj, _value);
                    return;
                }
                currentType = currentType.BaseType;
            }
        }
    }
}
