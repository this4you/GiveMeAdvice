using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Users.Models;

namespace Users.Entity
{
    public class Friend
    {
        public int Id { get; set; }


        public string FriendId { get; set; }

        [InverseProperty("Friends")]
        [ForeignKey("FriendId")]
        public virtual AppUser FriendUser { get; set; }


        public string UserWhoId { get; set; }

        [InverseProperty("ImFriend")]
        [ForeignKey("UserWhoId")]
        public virtual AppUser UserWho { get; set; }




        public DateTime Data { get; set; }
    }
}