using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace my8.Api.Infrastructures
{
    public static class TypeExtensions
    {
        public static T GetDefaultValue<T>()
        {
            return (T)GetDefaultValue(typeof(T));
        }
        public static object GetDefaultValue(this Type type)
        {
            return type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
