using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
    public class CategoryModel
    {
       public IEnumerable<string> Categories { get; set; }
       public AppUser User { get; set; }
    }
}