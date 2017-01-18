using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ValueObjectSerialization
{
    public sealed class PublicReadableProperties : IEnumerable<PropertyInfo>
    {
        private readonly Type _type;

        public PublicReadableProperties(object obj) 
            : this(obj.GetType()) { }

        public PublicReadableProperties(Type type)
        {
            _type = type;
        }

        public IEnumerator<PropertyInfo> GetEnumerator()
        {
            return ((IEnumerable<PropertyInfo>)_type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
