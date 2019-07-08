using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Users.Models;

namespace Users.Entity
{
    public class Request
    {
        public int RequestId { get; set; }


        public string UserId { get; set; }

        [InverseProperty("UsersWho")]
        [ForeignKey("UserId")]
        public virtual AppUser UserWho { get; set; }


        public string UserId2 { get; set; }

        [InverseProperty("UsersTo")]
        [ForeignKey("UserId2")]
        public virtual AppUser UserTo { get; set; }

       


        public DateTime Data { get; set; }


    }
}