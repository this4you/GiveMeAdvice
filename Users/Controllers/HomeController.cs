using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Users.Entity;
using Users.Entity.Abstract;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{

    public class HomeController : Controller
    {
        private IContentRepository content;

        public HomeController(IContentRepository repository) {
            content = repository;
        }


        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                return RedirectToAction("ShowNewsLine", "NewsLine");   
            }
            return View();
        }
        public string FriendRequest(string userId)
        {
            AppUser userTo = content.Users.FirstOrDefault(u => u.Id == userId);
            Request request = new Request { Data = DateTime.Now, UserId = CurrentUser.Id, UserId2 = userTo.Id };
            content.SaveRequest(request);
            return "YES";
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


    }
}