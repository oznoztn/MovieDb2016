using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Data.Contracts
{
    public interface IUserRepository : IDataRepository<User>
    {
        StatsMovieGenre[] Statistics_RatedMoviesByGenre(int userId);
        StatsMovieYear[] Statistics_RatedMoviesByYear(int userId);
        StatsRatingDistribution[] Statistics_RatingDistrubition(int userId);
        StatsMovieGenre[] Statistics_Top10Directors(int userId);
        StatsMovieGenre[] Statistics_Top10Actor(int userId);


        User GetByEmail(string email);
        User GetByUsername(string username);
        User GetByTwitterId(string twitterId);
        User GetByFacebookId(string facebookId);
        User GetByInstagramId(string instagramId);
        User GetByGoogleId(string googleId);

        User[] GetByPage(int page, int pageSize);
        int TotalCount();
    }
}
