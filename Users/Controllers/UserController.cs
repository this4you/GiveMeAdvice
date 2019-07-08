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
    public class UserController : Controller
    {
        private IContentRepository content;

        public UserController(IContentRepository repository)
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
        




        //редактирование пользователя == Добавление информации о пользователе "О себе"
        [HttpPost]
        public PartialViewResult Edit(AppUser user)
        {
            AppUser userUp = CurrentUser;
            userUp.Description = user.Description;
            content.SaveUser(userUp);
            return PartialView("AboutUser", CurrentUser);
        }

        public ActionResult Alerts() {
            ICollection<Request> requests = CurrentUser.UsersTo;
            return View(requests);
        }

        public ActionResult FriendRequest(string userId, bool start = false)
        {
            ViewBag.Already = false;
            ViewBag.Start = start;
            ViewBag.UseId = userId;
            if (CurrentUser.UsersWho.FirstOrDefault(u => u.UserId2 == userId) != null)
            {
                ViewBag.Already = true;
                return PartialView();
            }
            else {
                AppUser userTo = content.Users.FirstOrDefault(u => u.Id == userId);
                if (start)
                {
                    return PartialView();
                }
                else {
                    Request request = new Request { Data = DateTime.Now, UserId = CurrentUser.Id, UserId2 = userTo.Id };
                    ViewBag.Already = true;
                    content.SaveRequest(request);
                    return PartialView();
                }
            }
    
        }

        public ActionResult ShowPeople() {
            IQueryable<AppUser> users = UserManager.Users.Where(u=>u.UserName != CurrentUser.UserName && u.UserName != "Admin");
            ViewBag.Current = CurrentUser;
            return View(users);
        }

        public PartialViewResult UserHeader() {
            return PartialView("UserHeader",CurrentUser);
        }

        // GET: User
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated) {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        public ActionResult ShowAdvice(string category) {

            ShowAdvices showAdvices = new ShowAdvices
            {   
                Advices = category==null? content.Advices.Where(p => p.User.Id.Equals(CurrentUser.Id)): 
                content.Advices.Where(p => p.User.Id.Equals(CurrentUser.Id) && p.Category.Name.Equals(category))
            };
            return View(showAdvices);
        }

        public ActionResult CreateAdvice() {  ///DDDDDD
            List<string> cate = new List<string>();

            foreach (var it in content.Categories) {
                cate.Add(it.Name);
            }
            SelectList categories = new SelectList(cate,cate[0]);
            ViewBag.Categories = categories;
            ViewBag.User = CurrentUser;
            return View();
        }

        [HttpPost]
        public ActionResult CreateAdvice(Advice advice, HttpPostedFileBase image, String category) {
            if (image != null)
            {
                advice.PhotoType = image.ContentType;
                advice.Photo = new byte[image.ContentLength];
                image.InputStream.Read(advice.Photo, 0, image.ContentLength);
            }
            if (category != null) {
                AdviceCategory adviceCategory = content.Categories.FirstOrDefault(p=>p.Name.Equals(category));
                advice.Category = adviceCategory;
            }
            content.SaveAdvice(advice);
            return RedirectToAction("ShowAdvice");
        }

        public FileContentResult GetImage(int advicesID) {
            Advice adv = content.Advices.FirstOrDefault(p => p.AdviceId == advicesID);
            if (adv != null) {
                return File(adv.Photo, adv.PhotoType);
            } else {
                return null;
            }
        }

        //  Получение Аватарку пользователя
        public FileContentResult GetAvatar(string userId) {
            if(userId != null)
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
            else { 
            if (CurrentUser.Photo != null && CurrentUser.PhotoType != null)
            {
                return File(CurrentUser.Photo, CurrentUser.PhotoType);
            }
            else {
                return null;
            }
            }

        }
    }
}