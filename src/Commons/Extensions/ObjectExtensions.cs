using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object value)
        {
            return (T)value;
        }

#if !SILVERLIGHT
        public static object ChangeType(this object value, System.Type type)
        {
            if (value == null || value is System.DBNull)
            {
                return null;
            }
            return System.Convert.ChangeType(value, type);
        }

        public static T ChangeType<T>(this object value, T defaultValue = default(T))
        {
            if (value == null || value is System.DBNull)
            {
                return defaultValue;
            }
            if (value is T)
            {
                return (T)value;
            }
            var type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>))
            {
                type = type.GenericTypeArguments[0];
            }
            return (T)value.ChangeType(type);
        }
#endif
    }
}
