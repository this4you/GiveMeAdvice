using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Users.Entity;
using Users.Models;

namespace Users.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("name=IdentityDb") { }
        public virtual DbSet<Advice> Advices { get; set;}
        public virtual DbSet<Comment> Coments { get; set;}
        public virtual DbSet<AdviceCategory> Categories { get; set;}
        public virtual DbSet<Like> Likes { get; set;}
        public virtual DbSet<Request> Requests { get; set;}
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<AdditionalComment> AddComments { get; set; }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

      
    }

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {
    }
}

