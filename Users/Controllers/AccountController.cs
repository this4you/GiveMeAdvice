using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Users.Infrastructure;
using Users.Models;

namespace Users.Controllers
{
    
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

    

       
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(NewLayoutModel details)
        {
            AppUser user = await UserManager.FindAsync(details.Name, details.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Некорректное имя или пароль.");
            }
            else
            {
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
                return JavaScript("location.reload(true)");
              
            }
            
            return PartialView("Valid",details);

        }
        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            if (HttpContext.User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            return View("Registration");
        }

        [HttpPost]
        public async Task<ActionResult> Registration(NewLayoutModel model, HttpPostedFileBase Image)
        {
            if (string.IsNullOrEmpty(model.Name)) {
                ModelState.AddModelError("Name", "Please enter your Nickname");
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError("Email", "Please enter your Email");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("Password", "Please enter your Password");
            }

            if (ModelState.IsValidField("Password") && ModelState.IsValidField("Name") 
                && ModelState.IsValidField("Email") && !model.Password.Equals(model.RepeatPassword)) {
                ModelState.AddModelError("", "Пароли не совподают");
            }

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                // добавление фото 
                if (Image != null)
                {
                    user.PhotoType = Image.ContentType;
                    user.Photo = new byte[Image.ContentLength];
                    Image.InputStream.Read(user.Photo, 0, Image.ContentLength);
                }
               


                IdentityResult result =
                    await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return PartialView("Success");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return PartialView("ValidR",model);
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }



    }
}