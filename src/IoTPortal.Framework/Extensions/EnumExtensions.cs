#nullable disable

using System.ComponentModel.DataAnnotations;

namespace IoTPortal.Framework.Extensions
{
    public static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var memberInfo = value.GetType().GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.FirstOrDefault() as T;
        }

        public static DisplayAttribute GetDisplay(this Enum value)
        {
            return value.GetAttribute<DisplayAttribute>();
        }
    }
}
