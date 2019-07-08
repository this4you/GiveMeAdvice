using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Users.Entity.Abstract;
using Users.Infrastructure;
using Users.Models;

namespace Users.Entity.Concrete
{
    public class EFContentRepository : IContentRepository
    {
        private AppIdentityDbContext context = new AppIdentityDbContext();
        public IQueryable<Advice> Advices
        {
            get { return context.Advices; }
        }

        public IQueryable<AppUser> Users {
            get { return context.Users; }
        }

        public IQueryable<AdviceCategory> Categories {
            get { return context.Categories; }
        }

        public IQueryable<Like> Likes {
            get { return context.Likes; }
        }

        public IQueryable<Request> Requests {
            get { return context.Requests;}
        }

        public IQueryable<Friend> Friends {
            get { return context.Friends; }
        }

        public IQueryable<Comment> Comments
        {
            get { return context.Coments; }
        }

        public IQueryable<AdditionalComment> AdditionalComments {
            get { return context.AddComments; }
        }

        public void DeleteAdvice(int adviceId)
        {
            Advice advice = context.Advices.Find(adviceId);
            if (advice != null) {
                context.Advices.Remove(advice);
                context.SaveChanges();
            }
        }

        public void DeleteLikes(Like like)
        {
            context.Likes.Remove(like);
            context.SaveChanges();
        }

        public void DeleteRequest(int requestId)
        {
            Request req = context.Requests.Find(requestId);
            if (req != null)
            {
                context.Requests.Remove(req);
                context.SaveChanges();
            }
        }

        public void SaveAddComment(AdditionalComment additionalComment)
        {
            context.AddComments.Add(additionalComment);
            context.SaveChanges();
        }

        public void SaveAdvice(Advice advice)
        {
            context.Advices.Add(advice);
            context.SaveChanges();
        }

        public void SaveComment(Comment comment)
        {
            context.Coments.Add(comment);
            context.SaveChanges();
        }

        public void SaveFriend(Friend friend)
        {
            context.Friends.Add(friend);
            context.SaveChanges();
        }

        public void SaveLike(Like like)
        {
            context.Likes.Add(like);
            context.SaveChanges();
        }

        public void SaveRequest(Request request)
        {
            context.Requests.Add(request);
            context.SaveChanges();
        }



        public void SaveUser(AppUser user)
        {
            AppUser eUser = context.Users.Find(user.Id);
            if (eUser != null)
            {
                eUser.Description = user.Description;
            }
            context.SaveChanges();
        }
    }
}