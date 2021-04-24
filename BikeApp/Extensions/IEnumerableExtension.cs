using BikeApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.Extensions
{
    public class IEnumerableExtension
    {
        public IEnumerable<SelectListItem> CSelectListItem(IEnumerable<Make> Items)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem item = new SelectListItem()
            {
                Text = "--Select--",
                Value = "0"
            };
            list.Add(item);
            foreach (Make make in Items)
            {
                item = new SelectListItem()
                {
                    Text = item.GetType().GetProperty("Name").GetValue(item,null).ToString(),// make.Name,
                    Value = item.GetType().GetProperty("Id").GetValue(item, null).ToString()//make.Id.ToString()

                };
                list.Add(item);
            }
            return list;
        }
    }
}
