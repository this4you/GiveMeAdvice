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
    public class NotificationController : Controller
    {
        // Дополнительные методы для получение данных о пользователе и связи с бд
        private IContentRepository content;

        public NotificationController(IContentRepository repository)
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

          //отображение уведомлений в боксе
        public ActionResult ShowNotifications()
        {
            IEnumerable<Request> notifications = CurrentUser.UsersTo.OrderByDescending(p => p.Data).Take(3);
            return PartialView(notifications);
        }

        //отображение страницы уведомлений "Показать больше"

        public ActionResult NotificationsPage()
        {
            return View();
        }
        
        //отображение запросов друзей
        
        public ActionResult NoFriends()
        {
            IEnumerable<Request> notifications = CurrentUser.UsersTo.OrderByDescending(p => p.Data);

            return PartialView(notifications);
        }
    }
}