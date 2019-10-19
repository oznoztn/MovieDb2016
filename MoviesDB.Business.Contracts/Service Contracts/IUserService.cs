﻿using System.ServiceModel;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;

namespace MoviesDB.Business.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        User Get(int id);

        [OperationContract]
        User GetByEmail(string email);

        [OperationContract]
        User GetByUsername(string username);

        [OperationContract]
        User GetByTwitterId(string twitterId);

        [OperationContract]
        User GetByFacebookId(string facebookId);

        [OperationContract]
        User GetByInstagramId(string instagramId);

        [OperationContract]
        User GetByGoogleId(string googleId);

        [OperationContract]
        User Update(User entity);

        [OperationContract]
        void Delete(int entityId);

        [OperationContract]
        User[] GetByPage(int page, int pageSize);

        [OperationContract]
        int TotalCount();

        [OperationContract]
        string Statistics_RatedMoviesByGenre(int userId);

        [OperationContract]
        string Statistics_RatedMoviesByYear(int userId);

        [OperationContract]
        string Statistics_RatingDistrubition(int userId);

        [OperationContract]
        string Statistics_Top10Directors(int userId);

        [OperationContract]
        string Statistics_Top10Actors(int userId);
    }
}