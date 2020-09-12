using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Extensions
{
    public static class IEnumerableExtension
    {
        // This extension is used to set all attributes of selected category in dropdown menu 

        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, short selectedValue)
        {
            
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Name"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())
                   };
        }
    }
}
