using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ValueObjectSerialization
{
    public static class New<T>
    {
        public static readonly Func<T> Instance = CreateInstance();

        private static Func<T> CreateInstance()
        {
            var t = typeof(T);
            if (t == typeof(string))
                return Expression.Lambda<Func<T>>(Expression.Constant(string.Empty)).Compile();
            if (HasDefaultConstructor(t))
                return Expression.Lambda<Func<T>>(Expression.New(t)).Compile();
            return () => (T)FormatterServices.GetUninitializedObject(t);
        }

        private static bool HasDefaultConstructor(Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
