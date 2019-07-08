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
    public class AdviceController : Controller
    {
        private IContentRepository content;

        public AdviceController(IContentRepository repository)
        {
            content = repository;
        }




        // Удаление совета
        [HttpPost]
        public ActionResult DeleteAdvice(int adviceId) {
            content.DeleteAdvice(adviceId);
            return RedirectToAction("PersonalPage", "Profile");
        }

        // Создание совета

        public PartialViewResult CreateAdvice()
        {
            List<string> cate = new List<string>();

            foreach (var it in content.Categories)
            {
                cate.Add(it.Name);
            }
            SelectList categories = new SelectList(cate, cate[0]);
            ViewBag.Categories = categories;

            return PartialView();
        }


        [HttpPost]
        public ActionResult CreateAdvice(Advice advice, HttpPostedFileBase image, String category)
        {
            advice.UserId = CurrentUser.Id;
            if (image != null)
            {
                advice.PhotoType = image.ContentType;
                advice.Photo = new byte[image.ContentLength];
                image.InputStream.Read(advice.Photo, 0, image.ContentLength);
            }
            if (category != null)
            {
                AdviceCategory adviceCategory = content.Categories.FirstOrDefault(p => p.Name.Equals(category));
                advice.Category = adviceCategory;
            }
            content.SaveAdvice(advice);
            return RedirectToAction("PersonalPage","Profile");
        }

        //метод генерации опций к совету
        public ActionResult Options(Advice advice)
        {
            Console.WriteLine(advice.User.UserName);
            Console.WriteLine(CurrentUser.UserName);
            if (advice.User.UserName == CurrentUser.UserName)
            {

                ViewBag.Current = true;
            }
            else
            {
                ViewBag.Current = false;
            }

            return PartialView(advice);
        }



        //подгрузка фото
        public FileContentResult GetImage(int advicesID)
        {
            Advice adv = content.Advices.FirstOrDefault(p => p.AdviceId == advicesID);
            if (adv != null)
            {
                return File(adv.Photo, adv.PhotoType);
            }
            else
            {
                return null;
            }
        }
        //просмотр совета
        public ActionResult Open(int adviceId, string userName) {
            if(userName != null)
            {
                ViewBag.AdviceOptions = false;
                ViewBag.User = content.Users.FirstOrDefault(u => u.UserName == userName);
            }
            else
            {
                ViewBag.User = CurrentUser;
                ViewBag.AdviceOptions = true;
            }
            Advice advice = content.Advices.FirstOrDefault(a=>a.AdviceId == adviceId);
            
            return View(advice);
        }

       // система для "лайков" 
        public PartialViewResult Like(string adviceid,bool start = true)
        {
            int adviceIdInt = Int32.Parse(adviceid);
            Advice advice = content.Advices.FirstOrDefault(a=>a.AdviceId == adviceIdInt);
            if (start == false)
            {
                Like like = content.Likes.FirstOrDefault(p => p.AdviceId == adviceIdInt && p.UserId.Equals(this.CurrentUser.Id));

                if (like != null)
                {
                    ViewBag.AdviceId = adviceid;
                    ViewBag.Show = true;
                    ViewBag.Count = advice.Likes.Count();
                    return PartialView();
                }
                else {
                    ViewBag.AdviceId = adviceid;
                    ViewBag.Show = false;
                    ViewBag.Count = advice.Likes.Count();
                    return PartialView();
                }
            }
            else {
                Like like = content.Likes.FirstOrDefault(p => p.AdviceId == adviceIdInt && p.UserId.Equals(CurrentUser.Id));
                if (like != null)
                {
                    content.DeleteLikes(like);
                    ViewBag.AdviceId = adviceid;
                    ViewBag.Show = false;
                    ViewBag.Count = advice.Likes.Count();
                    return PartialView();
                }
                else {
                    Like saveLike = new Like { AdviceId = adviceIdInt, UserId = CurrentUser.Id };
                    content.SaveLike(saveLike);
                    ViewBag.AdviceId = adviceid;
                    ViewBag.Show = true;
                    ViewBag.Count = advice.Likes.Count();
                    return PartialView();
                }
            }
   
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