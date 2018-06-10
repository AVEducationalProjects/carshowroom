using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShowRoom.Extensions
{
    public static class AttributeExt
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return (TAttribute)(enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault());
        }
    }
}
