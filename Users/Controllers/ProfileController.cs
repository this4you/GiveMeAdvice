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
    public class ProfileController : Controller
    {
    
        // Дополнительные методы для получение данных о пользователе и связи с бд
        private IContentRepository content;

        public ProfileController(IContentRepository repository)
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
        


            // Добавление информации о себе 
        public PartialViewResult AddAbout()
        {
            return PartialView(CurrentUser);
        }


            //Отображение главной страницы
        public ActionResult PersonalPage()
        {
            return View(CurrentUser);
        }

        //Отображение ленты
        public ActionResult ShowLineAdvice(int amount = 10)
        {
            List<Advice> advices = CurrentUser.Advices.OrderByDescending(p => p.Data).Take(10).ToList();
            for(int i = 0; i < CurrentUser.ImFriend.Count; i++)
            {
                advices.AddRange(CurrentUser.ImFriend.ToList()[i].FriendUser.Advices.OrderByDescending(p => p.Data).Take(10));
            }
            if (amount >= advices.Count())
                ViewBag.Amount = 0;
            else
                ViewBag.Amount = amount;

            return PartialView("ShowLineAdvice",advices.OrderByDescending(p => p.Data).Take(amount));
        }

        //Отображение советов на главной странице
        public ActionResult ShowAdvices(string category, string userName = null, int amount = 5) {
            if (userName != null)
            {
                AppUser user = content.Users.FirstOrDefault(u => u.UserName == userName);
                IEnumerable<Advice> advices = category == null ? user.Advices.AsQueryable().OrderByDescending(p => p.Data) :
                   user.Advices.OrderByDescending(p => p.Data).Where(p => p.Category.Name.Equals(category));
                ViewBag.Category = category;
                ViewBag.UserProfile = user;
                if (amount >= advices.Count())
                    ViewBag.Amount = 0;
                else
                    ViewBag.Amount = amount;
                return PartialView("ShowAdvices", advices.Take(amount));
            }
            else
            {
                ViewBag.UserProfile = null;
                IEnumerable<Advice> advices = category == null ? CurrentUser.Advices.AsQueryable().OrderByDescending(p => p.Data) :
                   CurrentUser.Advices.OrderByDescending(p => p.Data).Where(p => p.Category.Name.Equals(category));
                ViewBag.Category = category;
                if (amount >= advices.Count())
                    ViewBag.Amount = 0;
                else
                    ViewBag.Amount = amount;
                return PartialView("ShowAdvices", advices.Take(amount));
            }
        }

        //Отображение категорий советов
        public PartialViewResult ShowCategory(string category, string userName)
        {
            if (userName != null)
            {
                AppUser user = content.Users.FirstOrDefault(u => u.UserName == userName);
                CategoryModel model = new CategoryModel
                {
                    Categories = content.Categories.Select(x => x.Name).OrderBy(x => x),
                    User = user
                };
                ViewBag.SelectedCategory = category;
                return PartialView(model);

            }
            else { 
            CategoryModel model = new CategoryModel
            {
                Categories = content.Categories.Select(x => x.Name).OrderBy(x => x),
                User = CurrentUser
            };
            ViewBag.SelectedCategory = category;
          
            
            return PartialView(model);
        }
        }

        // отображение информации о пользователе

        public ActionResult AboutUser(string userName) {
            if(userName!= null)
            {
                AppUser user = content.Users.FirstOrDefault(u => u.UserName == userName);
                return PartialView("AboutUser", user);
            }
            return PartialView("AboutUser",CurrentUser);
        }

    }
}