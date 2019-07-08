using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Entity.Abstract;

namespace Users.Controllers
{
    public class NavController : Controller
    {
        private IContentRepository content;

        public NavController(IContentRepository repository)
        {
            content = repository;
        }
        public PartialViewResult Menu(string category) {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = content.Categories.Select(x=>x.Name).OrderBy(x=>x);
            return PartialView(categories);
        }
    }
} 
    