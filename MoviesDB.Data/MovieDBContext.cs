using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using MoviesDB.Business.Entities;
using MoviesDB.Data.Mapping;
using MoviesDB.Data.Mapping.Blog;
using MoviesDB.Data.Mapping.Location;

namespace MoviesDB.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
            : base("name=MovieDb")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Actor> ActorSet { get; set; }
        public DbSet<Comment> CommentSet { get; set; }
        public DbSet<Director> DirectorSet { get; set; }
        public DbSet<Genre> GenreSet { get; set; }
        public DbSet<Language> LanguageSet { get; set; }
        public DbSet<Movie> MovieSet { get; set; }
        public DbSet<News> NewsSet { get; set; }
        public DbSet<NewsCategory> NewsCategorySet { get; set; }
        public DbSet<NewsMapping> NewsMappingSet { get; set; }
        public DbSet<Poll> PollSet { get; set; }
        public DbSet<PollVote> PollVoteSet { get; set; }
        public DbSet<PollVotingRecord> PollVotingRecordSet { get; set; }
        public DbSet<Rating> RatingSet { get; set; }
        public DbSet<Review> ReviewSet { get; set; }
        public DbSet<Role> RoleSet { get; set; }
        public DbSet<User> UserSet { get; set; }
        public DbSet<UserList> UserListSet { get; set; }
        public DbSet<UserListRecord> UserListRecordSet { get; set; }
        public DbSet<WatchlistRecord> WatchlistRecordSet { get; set; }
        public DbSet<BlogPost> BlogPostSet { get; set; }
        public DbSet<BlogPostComment> BlogPostCommentSet { get; set; }
        public DbSet<Country> CountrySet { get; set; }
        public DbSet<County> CountySet { get; set; }
        public DbSet<State> StateSet { get; set; }
        public DbSet<MovieActorMapping> MovieActorMappingSet { get; set; }
        public DbSet<MovieGenreMapping> MovieGenreMappingSet { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Configurations.Add(new ActorConfiguration());
            modelBuilder.Configurations.Add(new MovieConfiguration());
            modelBuilder.Configurations.Add(new DirectorConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new NewsCategoryConfiguration());
            modelBuilder.Configurations.Add(new NewsConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
            modelBuilder.Configurations.Add(new ReviewConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new UserListConfiguration());
            modelBuilder.Configurations.Add(new UserListRecordConfiguration());
            modelBuilder.Configurations.Add(new WatchlistRecordConfiguration());
            modelBuilder.Configurations.Add(new NewsMappingConfiguration());
            modelBuilder.Configurations.Add(new PollConfiguration());
            modelBuilder.Configurations.Add(new PollVoteConfiguration());
            modelBuilder.Configurations.Add(new PollVotingRecordConfiguration());
            modelBuilder.Configurations.Add(new BlogPostCommentConfiguration());
            modelBuilder.Configurations.Add(new BlogPostConfiguration());
            modelBuilder.Configurations.Add(new StateConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new CountyConfiguration());
            modelBuilder.Configurations.Add(new MovieGenreMappingConfiguration());
            modelBuilder.Configurations.Add(new MovieActorMappingConfiguration());

            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(type => !String.IsNullOrEmpty(type.Namespace))
            //    .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
            //        type.BaseType.GetGenericTypeDefinition() == typeof(MovieConfiguration));
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);
            //}
        }
    }
}
