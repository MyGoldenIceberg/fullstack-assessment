using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAssessment.Core.Helper
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                return string.Empty;

            Type type = value.GetType();
            string name = Enum.GetName(type, value);

            if (string.IsNullOrEmpty(name))
                return value.ToString();

            FieldInfo field = type.GetField(name);
            if (field == null)
                return value.ToString();

            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }
    }
}
