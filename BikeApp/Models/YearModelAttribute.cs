using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApp.Models
{
    public class YearModelAttribute:RangeAttribute
    {
        public YearModelAttribute(int startYear):base(startYear,DateTime.Now.Year)
        {

        }
    }
}
