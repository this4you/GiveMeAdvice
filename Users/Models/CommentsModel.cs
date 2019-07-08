using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Entity;

namespace Users.Models
{
    public class CommentsModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<AdditionalComment> additionals { get; set; }
    }
}