using System;
using System.Collections;

namespace ObjectComparer.Helper
{
    public class TypeHelper
    {
        public static bool IsAssignableFrom(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type);
        }

        public static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive;
        }

        public static bool IsValueType(Type type)
        {
            return type.IsValueType;
        }

        public static bool IsEnumerableType(Type type)
        {
            return (typeof(IEnumerable).IsAssignableFrom(type));
        }
    }
}
