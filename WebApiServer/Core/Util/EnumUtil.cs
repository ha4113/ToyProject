using System;
using System.Reflection;

namespace Common.Core.Util
{
    public static class EnumUtil
    {
        public static string GetStringValue(this Enum obj)
        {
            return obj.GetType().GetCustomAttribute<StringValueAttribute>()?.Value;
        }
    }
}