using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models
{
    public class UsersModel
    {
        public AppUser CurrentUser { get; set; }
        public AppUser MainUser { get; set; }
    }
    

    public class ShowUsersModel
    {
        public IEnumerable<AppUser> Users { get; set; }
        public AppUser CurrentUser { get; set; }
    }
}