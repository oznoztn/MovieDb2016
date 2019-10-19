using System.ComponentModel.Composition;
using System.ServiceModel;
using System.Web.Script.Serialization;
using MoviesDB.Business.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Data.Contracts;

namespace MoviesDB.Business.Services.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserService : ServiceBase, IUserService
    {
        [Import] 
        IUserRepository _userRepository;

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }
        
        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public User GetByTwitterId(string twitterId)
        {
            return _userRepository.GetByTwitterId(twitterId);
        }

        public User GetByFacebookId(string facebookId)
        {
            return _userRepository.GetByFacebookId(facebookId);
        }

        public User GetByInstagramId(string instagramId)
        {
            return _userRepository.GetByInstagramId(instagramId);
        }

        public User GetByGoogleId(string googleId)
        {
            return _userRepository.GetByGoogleId(googleId);
        }

        public User Update(User entity)
        {
            return _userRepository.Update(entity);
        }

        public void Delete(int entityId)
        {
            _userRepository.Remove(entityId);
        }

        public User[] GetByPage(int page, int pageSize)
        {
            return _userRepository.GetByPage(page, pageSize);
        }

        public int TotalCount()
        {
            return _userRepository.TotalCount();
        }

        public string Statistics_RatedMoviesByGenre(int userId)
        {
            var stats = _userRepository.Statistics_RatedMoviesByGenre(userId);
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(stats);
            return jsonString;
        }

        public string Statistics_RatedMoviesByYear(int userId)
        {
            var stats = _userRepository.Statistics_RatedMoviesByYear(userId);
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(stats);
            return jsonString;
        }

        public string Statistics_RatingDistrubition(int userId)
        {
            var stats = _userRepository.Statistics_RatingDistrubition(userId);
            var serializer = new JavaScriptSerializer();
            var jsonString = serializer.Serialize(stats);
            return jsonString;
        }

        public string Statistics_Top10Directors(int userId)
        {
            var serializer = new JavaScriptSerializer();
            var stats = _userRepository.Statistics_Top10Directors(userId);
            var jsonString = serializer.Serialize(stats);
            return jsonString;
        }

        public string Statistics_Top10Actors(int userId)
        {
            var serializer = new JavaScriptSerializer();
            var stats = _userRepository.Statistics_Top10Actor(userId);
            var jsonString = serializer.Serialize(stats);
            return jsonString;
        }
    }
}
