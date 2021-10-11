using System;
using Microsoft.OpenApi.Extensions;

namespace Common.Core.Util
{
    public class StringValueAttribute : System.Attribute
    {
        public string Value { get; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }

    public static class Util
    {
        public static string GetStringValue(this Enum obj)
        {
            return obj.GetAttributeOfType<StringValueAttribute>().Value;
        }
    }
}