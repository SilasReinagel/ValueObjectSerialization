using System;

namespace ValueObjectSerialization
{
    public interface IConverter
    {
        object Convert(object obj, Type toType);
    }
}
