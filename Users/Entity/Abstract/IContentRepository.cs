using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Entity.Abstract
{
    public interface IContentRepository
    {
        IQueryable<Advice> Advices { get; }
        IQueryable<AppUser> Users { get; }
        IQueryable<AdviceCategory> Categories { get; }
        IQueryable<Like> Likes { get; }
        IQueryable<Request> Requests { get;}
        IQueryable<Friend> Friends { get; }
        IQueryable<Comment> Comments { get; }
        IQueryable<AdditionalComment> AdditionalComments { get; }

        void SaveComment(Comment comment);
        void SaveAddComment(AdditionalComment additionalComment);
        void SaveRequest(Request request);
        void DeleteRequest(int requestId);
        void SaveAdvice(Advice advice);
        void SaveLike(Like like);
        void SaveUser(AppUser user);
        void DeleteLikes(Like like);
        void DeleteAdvice(int adviceId);
        void SaveFriend(Friend friend);

       
    }
}
