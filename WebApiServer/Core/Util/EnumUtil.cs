using System;
using System.Reflection;

namespace Common.Core.Util
{
    public static class EnumUtil
    {
        public interface IValueAttribute<out T>
        {
            T Value { get; }
        }

        private static TValue ToValue<T, TValue, TAttr>(this T e)
            where T : Enum
            where TAttr : Attribute, IValueAttribute<TValue>
        {
            var attr = typeof(T).GetMember(e.ToString())[0].GetCustomAttribute<TAttr>();
            if (attr is IValueAttribute<TValue> valueAttr)
            {
                return valueAttr.Value;
            }

            return default;
        }
        
        public static string ToStringValue<T>(this T e)
            where T : Enum
        {
            return e.ToValue<T, string, StringValueAttribute>();
        }
        
    }
    
    public class StringValueAttribute : Attribute, EnumUtil.IValueAttribute<string>
    {
        public string Value { get; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }
}