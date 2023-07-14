using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Extensions
{
    public static class TypeExtension
    {
        public static bool IsJsonPirmitiveType(this Type t)
        {
            if (t.IsValueType)
            {
                return true;
            }

            switch (t.Name)
            {
                case "Int32":
                case "Int64":
                case "Byte[]":
                case "Single":
                case "Double":
                case "Decimal":
                case "String":
                case "Boolean":
                case "Enum":
                case "Byte":
                case "Char":
                case "Int16":
                    return true;
                default:
                    break;
            }

            return false;
        }

#if !SILVERLIGHT
        public static string GetTypeName(this System.Type type)
        {
            return $"{type.FullName}, {type.Assembly.GetName().Name}";
        }
#endif
    }

}
