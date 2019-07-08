using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Users.Models;

namespace Users.Entity
{
    public class Like
    {
        public int LikeId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public int AdviceId { get; set; }

        [ForeignKey("AdviceId")]
        public Advice Advice { get; set; }


    }
}