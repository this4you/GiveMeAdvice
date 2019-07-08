using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Users.Entity;

namespace Users.Models
{



    public class AppUser : IdentityUser
    {
        // Здесь будут указываться дополнительные свойства
        public byte[] Photo { get; set; }
        public string PhotoType { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Advice> Advices { get; }
        public virtual ICollection<Comment> Comments { get; }
        public virtual ICollection<Request> UsersWho { get; }
        public virtual ICollection<Request> UsersTo { get; }
        public virtual ICollection<Friend> Friends { get; }
        public virtual ICollection<Friend> ImFriend { get; }
        public virtual ICollection<AdditionalComment> AdditionalComments { get; }


        public AppUser()
        {
            Comments = new List<Comment>();
            Advices = new List<Advice>();
            UsersWho = new List<Request>();
            UsersTo = new List<Request>();
            Friends = new List<Friend>();
            ImFriend = new List<Friend>();
            AdditionalComments = new List<AdditionalComment>();
        }
    }
}