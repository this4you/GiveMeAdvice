using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Users.Entity
{
    public class AdviceCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Advice> Advices { get; }

        public AdviceCategory()
        {
            Advices = new List<Advice>();
        }
    }
}