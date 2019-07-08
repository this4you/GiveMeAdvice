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

namespace Users.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        // Дополнительные методы для получение данных о пользователе и связи с бд
        private IContentRepository content;

        public PeopleController(IContentRepository repository)
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

        // Метод отображения пользователей в системе
        public ActionResult ShowUsers()
        {
            IQueryable<AppUser> users = UserManager.Users.Where(u => u.UserName != CurrentUser.UserName
            && u.UserName != "Admin");
            ShowUsersModel model = new ShowUsersModel
            {
                CurrentUser = CurrentUser,
                Users = users
            };
            return View(model);
        }

        // Получение фото юзера
        public FileContentResult GetPhoto(string userId)
        {
            AppUser user = content.Users.FirstOrDefault(u => u.Id == userId);
            if (user.Photo != null && user.PhotoType != null)
            {
                return File(user.Photo, user.PhotoType);
            }
            else
            {
                return null;
            }

        }
        
        //Отправка запроса на добавление в друзья
        public ActionResult FriendRequest(string userId)
        {
            Request request = new Request
            {
                Data = DateTime.Now,
                UserId = CurrentUser.Id,
                UserId2 = userId
            };

            content.SaveRequest(request);

            return PartialView("FriendSent");

        }
    }
}