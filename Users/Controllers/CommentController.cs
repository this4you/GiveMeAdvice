using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Controllers.AbstractControllers;
using Users.Entity;
using Users.Entity.Abstract;
using Users.Models;

namespace Users.Controllers
{
    public class CommentController : AbstractController
    {
        public CommentController(IContentRepository repository) : base(repository)
        {
        }
        // метод отображения комментариев
        public ActionResult ShowComments(int adviceId)
        {
            Advice advice = content.Advices.FirstOrDefault(a => a.AdviceId == adviceId);
           
            return PartialView(advice.Comments);
        }

        // метод создания комментария
        [HttpPost]
        public ActionResult Create(string ctext, int adviceId)
        {
            Comment comment = new Comment
            {
                AdviceId = adviceId,
                Text = ctext,
                UserId = CurrentUser.Id,
                CommentDate = DateTime.Now
            };
            content.SaveComment(comment);
            return RedirectToAction("ShowComments", "Comment", new { adviceId });
        }
    }
}