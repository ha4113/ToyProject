using System;
using Microsoft.OpenApi.Extensions;

namespace Common.Core.Util
{
    public static class EnumUtil
    {
        public static string GetStringValue(this Enum obj)
        {
            return obj.GetAttributeOfType<StringValueAttribute>().Value;
        }
    }
}