using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Entity;
using Users.Entity.Abstract;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers.AbstractControllers
{
    public class AbstractController : Controller
    {
        // Дополнительные методы для получение данных о пользователе и связи с бд
        protected IContentRepository content;

        public AbstractController(IContentRepository repository)
        {
            content = repository;
        }
        protected AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        protected AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        //-------------------------- Конец дополнительных методов
    }
}