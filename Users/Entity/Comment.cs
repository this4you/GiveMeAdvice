using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Users.Models;

namespace Users.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        

        public int AdviceId { get; set; }

        [ForeignKey("AdviceId")]
        public virtual Advice Advice { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        public virtual ICollection<AdditionalComment> AdditionalComments { get; }

        public Comment()
        {
            AdditionalComments = new List<AdditionalComment>();
        }
    }
}