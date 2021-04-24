using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.Extensions
{
    public static class ReflectionExtension
    {
        public static string GetPropertyValue<T>(this T Item,string propertyName)
        {
            return Item.GetType().GetProperty("Name").GetValue(Item, null).ToString();
        }
    }
}
