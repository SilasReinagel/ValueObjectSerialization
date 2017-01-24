using System;
using System.Runtime.Serialization;

namespace ValueObjectSerialization
{
    public sealed class DefaultConverter : IConverter
    {
        private readonly IFormatterConverter _formatterConverter = new FormatterConverter();

        public object Convert(object obj, Type toType)
        {
            return _formatterConverter.Convert(obj, toType);
        }
    }
}
