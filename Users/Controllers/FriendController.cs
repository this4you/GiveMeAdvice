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
    public class FriendController : Controller
    {
        // Дополнительные методы для получение данных о пользователе и связи с бд
        private IContentRepository content;

        public FriendController(IContentRepository repository)
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
        //Подтверждение добаления в друзья

        public ActionResult FriendRequest(string userId, string url = "")
        {
            Console.WriteLine(userId);
            // создание двух записей друзей 
            Friend friendWho = new Friend
            {
                Data = DateTime.Now,
                FriendId = userId,
                UserWhoId = CurrentUser.Id
            };
            Friend friend = new Friend
            {
                Data = DateTime.Now,
                FriendId = CurrentUser.Id,
                UserWhoId = userId
            };
            content.SaveFriend(friendWho);
            content.SaveFriend(friend);
            //----------------------------

            //удаление запроса 
            Request req = content.Requests.FirstOrDefault(x => x.UserId == userId && x.UserId2 == CurrentUser.Id);
            content.DeleteRequest(req.RequestId);
            if (url.Length != 0) {
                return Redirect(url);
            }
            return PartialView("FriendRequest");
           

        }

    }
}