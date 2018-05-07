using System;

namespace Framework.Core
{
    public static class ObjectExtensions
    {
        private const string NULL = "null";

        public static string ToSafeString(this object @object)
        {
            return (@object == null) ? NULL : @object.ToString();
        }

        public static string ToSafeString(this IFormattable @object, string format, IFormatProvider provider = null)
        {
            if (@object == null)
                return NULL;

            return (string.IsNullOrWhiteSpace(format)) ? @object.ToString() : @object.ToString(format, provider);
        }
    }
}
