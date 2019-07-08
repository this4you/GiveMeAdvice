using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Controllers.AbstractControllers;
using Users.Entity.Abstract;

namespace Users.Controllers
{
    [Authorize]
    public class NewsLineController : AbstractController
    {
        public NewsLineController(IContentRepository repository) : base(repository)
        {
        }

        //
        public ActionResult ShowNewsLine()
        {
             
            return View(CurrentUser);
        }
    }
}