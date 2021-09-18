using System;
using Microsoft.OpenApi.Extensions;
using WebApiServer.Attribute;

namespace WebApiServer.Utility
{
    public static class Util
    {
        public static string GetStringValue(this Enum obj)
        {
            return obj.GetAttributeOfType<StringValue>().Value;
        }
    }
}