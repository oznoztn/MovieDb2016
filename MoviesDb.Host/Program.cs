using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Common.Core;
using MoviesDB.Business.IoC;
using MoviesDB.Business.Services.Services;

namespace MoviesDb.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = WindowsIdentity.GetCurrent();
            // appcmd.exe set site "Default Web Site" -+bindings.[protocol='net.tcp'],bindingsInformation='808:*']

            //GenericPrincipal principal = new GenericPrincipal(
            //    new GenericIdentity("Ozan"), new string[] { "Administrators", "MovieDbAdmin" });
            //Thread.CurrentPrincipal = principal;

            Console.WriteLine("Servisler başlatılıyor...");
            Console.WriteLine("");

            ObjectBase.Container = MEFLoader.Init();

            var hostActorService = new ServiceHost(typeof(ActorService));
            var hostCommentService = new ServiceHost(typeof(CommentService));
            var hostDirectorService = new ServiceHost(typeof(DirectorService));
            var hostGenreService = new ServiceHost(typeof(GenreService));
            var hostMovieService = new ServiceHost(typeof(MovieService));
            var hostNewsCategoryService = new ServiceHost(typeof(NewsCategoryService));
            var hostNewsService = new ServiceHost(typeof(NewsService));
            var hostRatingService = new ServiceHost(typeof(RatingService));
            var hostRoleService = new ServiceHost(typeof(RoleService));
            var hostUserListEntryService = new ServiceHost(typeof(UserListRecordService));
            var hostUserListService = new ServiceHost(typeof(UserListService));
            var hostUserService = new ServiceHost(typeof(UserService));
            var hostWatchlistService = new ServiceHost(typeof(WatchlistRecordService));
            var hostBlogPostService = new ServiceHost(typeof(BlogPostService));
            var hostNewsMappingService = new ServiceHost(typeof(NewsMappingService));
            var hostBlogPostCommentService = new ServiceHost(typeof(BlogPostCommentService));
            var hostLanguageService = new ServiceHost(typeof(LanguageService));
            var hostReviewService = new ServiceHost(typeof(ReviewService));
            var hostPollService = new ServiceHost(typeof(PollService));
            var hostPollVoteService = new ServiceHost(typeof(PollVoteService));
            var hostPollVotingRecordService = new ServiceHost(typeof(PollVotingRecordService));
            var hostStateService = new ServiceHost(typeof(StateService));
            var hostCountryService = new ServiceHost(typeof(CountryService));
            var hostCountyService = new ServiceHost(typeof(CountyService));

            StartService(hostNewsMappingService, hostNewsMappingService.Description.Name);
            StartService(hostBlogPostService, hostBlogPostService.Description.Name);
            StartService(hostReviewService, hostReviewService.Description.Name);
            StartService(hostPollVotingRecordService, hostPollVotingRecordService.Description.Name);
            StartService(hostPollService, hostPollService.Description.Name);
            StartService(hostPollVoteService, hostPollVoteService.Description.Name);
            StartService(hostBlogPostCommentService, hostBlogPostCommentService.Description.Name);
            StartService(hostActorService, hostActorService.Description.Name);
            StartService(hostCommentService, hostCommentService.Description.Name);
            StartService(hostDirectorService, hostDirectorService.Description.Name);
            StartService(hostGenreService, hostGenreService.Description.Name);
            StartService(hostMovieService, hostMovieService.Description.Name);
            StartService(hostLanguageService, hostLanguageService.Description.Name);
            StartService(hostNewsCategoryService, hostNewsCategoryService.Description.Name);
            StartService(hostNewsService, hostNewsService.Description.Name);
            StartService(hostRatingService, hostRatingService.Description.Name);
            StartService(hostRoleService, hostRoleService.Description.Name);
            StartService(hostUserListEntryService, hostUserListEntryService.Description.Name);
            StartService(hostUserListService, hostUserListService.Description.Name);
            StartService(hostUserService, hostUserService.Description.Name);
            StartService(hostWatchlistService, hostWatchlistService.Description.Name);
            StartService(hostStateService, hostStateService.Description.Name);
            StartService(hostCountryService, hostCountryService.Description.Name);
            StartService(hostCountyService, hostCountyService.Description.Name);

            Console.WriteLine("");
            Console.WriteLine("Çıkış için [ENTER]'a basın.");
            Console.ReadLine();

            StopService(hostNewsMappingService, hostNewsMappingService.Description.Name);
            StopService(hostBlogPostService, hostBlogPostService.Description.Name);
            StopService(hostReviewService, hostReviewService.Description.Name);
            StopService(hostPollVotingRecordService, hostPollVotingRecordService.Description.Name);
            StopService(hostPollService, hostPollService.Description.Name);
            StopService(hostPollVoteService, hostPollVoteService.Description.Name);
            StopService(hostBlogPostCommentService, hostBlogPostCommentService.Description.Name);
            StopService(hostActorService, hostActorService.Description.Name);
            StopService(hostCommentService, hostCommentService.Description.Name);
            StopService(hostDirectorService, hostDirectorService.Description.Name);
            StopService(hostGenreService, hostGenreService.Description.Name);
            StopService(hostMovieService, hostMovieService.Description.Name);
            StopService(hostLanguageService, hostLanguageService.Description.Name);
            StopService(hostNewsCategoryService, hostNewsCategoryService.Description.Name);
            StopService(hostNewsService, hostNewsService.Description.Name);
            StopService(hostRatingService, hostRatingService.Description.Name);
            StopService(hostRoleService, hostRoleService.Description.Name);
            StopService(hostUserListEntryService, hostUserListEntryService.Description.Name);
            StopService(hostUserListService, hostUserListService.Description.Name);
            StopService(hostUserService, hostUserService.Description.Name);
            StopService(hostWatchlistService, hostWatchlistService.Description.Name);
            StopService(hostStateService, hostStateService.Description.Name);
            StopService(hostCountryService, hostCountryService.Description.Name);
            StopService(hostCountyService, hostCountyService.Description.Name);

        }

        static void StartService(ServiceHost host, string description)
        {
            host.Open();
            Console.WriteLine("{0} servisi başlatıldı", description);

            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format("Dinlenen end-point bilgileri:"));
                Console.WriteLine(string.Format("Address : {0}", endpoint.Address.Uri));
                Console.WriteLine(string.Format("Binding : {0}", endpoint.Binding.Name));
                Console.WriteLine(string.Format("Contract: {0}", endpoint.Contract.Name));
            }

            Console.WriteLine();
        }

        static void StopService(ServiceHost host, string description)
        {
            host.Close();
            Console.WriteLine("{0} isimli servis durduruldu.", description);
        }
    }

}
