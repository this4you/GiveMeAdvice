using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Users.Models;

namespace Users.Entity
{
    public class AdditionalComment
    {
        public int AdditionalCommentId { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }


        public int CommentId { get; set; }

        [ForeignKey("CommentId")]
        public Comment MainComment { get; set; }


        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }


    }
}