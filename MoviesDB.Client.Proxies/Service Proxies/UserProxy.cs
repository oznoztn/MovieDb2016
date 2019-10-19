using System.ComponentModel.Composition;
using Core.Common.Proxy;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDB.Client.Proxies
{
    [Export(typeof(IUserService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserProxy : ProxyBase<IUserService>, IUserService
    {
        public User Get(int id)
        {
            return Channel.Get(id);
        }

        public User GetByEmail(string email)
        {
            return Channel.GetByEmail(email);
        }

        public User GetByUsername(string username)
        {
            return Channel.GetByUsername(username);
        }

        public User GetByTwitterId(string twitterId)
        {
            return Channel.GetByTwitterId(twitterId);
        }

        public User GetByFacebookId(string facebookId)
        {
            return Channel.GetByFacebookId(facebookId);
        }

        public User GetByInstagramId(string instagramId)
        {
            return Channel.GetByInstagramId(instagramId);
        }

        public User GetByGoogleId(string googleId)
        {
            return Channel.GetByGoogleId(googleId);
        }

        public User GetByOpenId(string openId)
        {
            return Channel.GetByOpenId(openId);
        }

        public User Update(User entity)
        {
            return Channel.Update(entity);
        }

        public void Delete(int entityId)
        {
            Channel.Delete(entityId);
        }

        public User[] GetByPage(int page, int pageSize)
        {
            return Channel.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return Channel.TotalCount();
        }

        public string Statistics_RatedMoviesByGenre(int userId)
        {
            return Channel.Statistics_RatedMoviesByGenre(userId);
        }

        public string Statistics_RatedMoviesByYear(int userId)
        {
            return Channel.Statistics_RatedMoviesByYear(userId);
        }

        public string Statistics_RatingDistrubition(int userId)
        {
            return Channel.Statistics_RatingDistrubition(userId);
        }

        public string Statistics_Top10Directors(int userId)
        {
            return Channel.Statistics_Top10Directors(userId);
        }

        public string Statistics_Top10Actors(int userId)
        {
            return Channel.Statistics_Top10Actors(userId);
        }
    }
}
