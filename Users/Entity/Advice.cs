using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Models;

namespace Users.Entity
{
    public class Advice
    {
       
        [HiddenInput(DisplayValue = false)]
        public int AdviceId { get; set; }

        public string Headline { get; set; }
        public string Discription { get; set; }
        public string Text { get; set; }
        public DateTime Data { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual   AppUser User { get; set; }

        public int CatgId { get; set; }

        [ForeignKey("CatgId")]
        public virtual  AdviceCategory Category { get; set; }

        public virtual ICollection<Like> Likes { get; }

        public virtual ICollection<Comment> Comments { get; }

        public Advice()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }

    }
}