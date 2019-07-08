using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Entity.Abstract;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        // Дополнительные методы для получение данных о пользователе и связи с бд
        private IContentRepository content;

        public UserProfileController(IContentRepository repository)
        {
            content = repository;
        }
        private AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        //-------------------------- Конец дополнительных методов
        //метод отображения профиля другого юзера 
        public ActionResult UserProfile(string name)
        {
            AppUser user = content.Users.FirstOrDefault(u => u.UserName == name); 
            return View(user);
        }
        //
    }
}