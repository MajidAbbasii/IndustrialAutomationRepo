using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Commons.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this System.Enum enumValue)
        {
            if (enumValue == null) return string.Empty;
            Type enumType = enumValue.GetType();
            string fieldName = Enum.GetName(enumType, enumValue);

            FieldInfo enumField = enumType.GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
            object[] descAttributes = enumField.GetCustomAttributes(typeof(DescriptionAttribute), false);

            string displayText = fieldName;
            if (descAttributes.Length > 0)
            {
                displayText = ((DescriptionAttribute)descAttributes[0]).Description;
            }
            return displayText;
        }

        public static int ToInt32(this System.Enum enumValue)
        {
            if (enumValue == null) return 0;
            return System.Convert.ToInt32(enumValue);
        }


    }
}
