namespace MoviesDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        FullName = c.String(),
                        Biography = c.String(),
                        ImdbLink = c.String(),
                        CreatedAt = c.DateTime(nullable: false, storeType: "date"),
                        BirthDate = c.DateTime(storeType: "date"),
                        DeathDate = c.DateTime(storeType: "date"),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CountyId = c.Int(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .ForeignKey("dbo.County", t => t.CountyId)
                .ForeignKey("dbo.State", t => t.StateId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CountyId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Director",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        FullName = c.String(),
                        Biography = c.String(),
                        Gender = c.Boolean(nullable: false),
                        ImdbLink = c.String(),
                        Photo = c.String(),
                        CountryId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        CountyId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, storeType: "date"),
                        BirthDate = c.DateTime(storeType: "date"),
                        DeathDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.County", t => t.CountyId)
                .ForeignKey("dbo.State", t => t.StateId)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.CountryId)
                .Index(t => t.StateId)
                .Index(t => t.CountyId);
            
            CreateTable(
                "dbo.County",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DirectorId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                        Aka = c.String(),
                        Year = c.Int(nullable: false),
                        RunningTime = c.Int(nullable: false),
                        Rating = c.Single(nullable: false),
                        VoteCount = c.Int(nullable: false),
                        ImdbLink = c.String(),
                        PlotOutline = c.String(),
                        CoverImage = c.String(),
                        CreatedAt = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Director", t => t.DirectorId, cascadeDelete: true)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.DirectorId)
                .Index(t => t.LanguageId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        Text = c.String(),
                        IsConfirmed = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(),
                        Mail = c.String(),
                        Age = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        LastPasswordChangedDate = c.DateTime(),
                        LastAccountSuspensedDate = c.DateTime(),
                        LastActivityDate = c.DateTime(),
                        Signature = c.String(),
                        Location = c.String(),
                        Website = c.String(),
                        Twitter = c.String(),
                        Facebook = c.String(),
                        Google = c.String(),
                        Instagram = c.String(),
                        Avatar = c.String(),
                        DisablePrivateMessages = c.Boolean(nullable: false),
                        DisableEmailNotifications = c.Boolean(nullable: false),
                        IsActivated = c.Boolean(nullable: false),
                        IsSuspended = c.Boolean(nullable: false),
                        IsSystemAccount = c.Boolean(nullable: false),
                        IsPremium = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogPostComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogPostId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Text = c.String(),
                        Published = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPost", t => t.BlogPostId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.BlogPostId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BlogPost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        MetaKeywords = c.String(),
                        MetaDescription = c.String(),
                        MetaTitle = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Published = c.Boolean(nullable: false),
                        AllowComments = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Body = c.String(),
                        MetaKeywords = c.String(),
                        MetaDescription = c.String(),
                        MetaTitle = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PollVotingRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        VotingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PollVote", t => t.VoteId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.VoteId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PollVote",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PollId = c.Int(nullable: false),
                        VoteText = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        VoteCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Poll", t => t.PollId, cascadeDelete: true)
                .Index(t => t.PollId);
            
            CreateTable(
                "dbo.Poll",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        OnHomePage = c.Boolean(nullable: false),
                        Published = c.Boolean(nullable: false),
                        GuestsAllowedForVoting = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        DateRated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserListRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserList", t => t.ListId, cascadeDelete: true)
                .Index(t => t.ListId);
            
            CreateTable(
                "dbo.WatchlistRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieActorMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actor", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.MovieGenreMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsSubGenre = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        MovieId = c.Int(),
                        ActorId = c.Int(),
                        DirectorId = c.Int(),
                        NewsCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actor", t => t.ActorId)
                .ForeignKey("dbo.Director", t => t.DirectorId)
                .ForeignKey("dbo.News", t => t.NewsId)
                .ForeignKey("dbo.NewsCategory", t => t.NewsCategoryId)
                .ForeignKey("dbo.Movie", t => t.MovieId)
                .Index(t => t.NewsId)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId)
                .Index(t => t.DirectorId)
                .Index(t => t.NewsCategoryId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        Synopsis = c.String(),
                        FullText = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleUser",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Role", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actor", "StateId", "dbo.State");
            DropForeignKey("dbo.Actor", "CountyId", "dbo.County");
            DropForeignKey("dbo.Actor", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Movie", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Director", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Director", "StateId", "dbo.State");
            DropForeignKey("dbo.NewsMapping", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.NewsMapping", "NewsCategoryId", "dbo.NewsCategory");
            DropForeignKey("dbo.NewsMapping", "NewsId", "dbo.News");
            DropForeignKey("dbo.NewsMapping", "DirectorId", "dbo.Director");
            DropForeignKey("dbo.NewsMapping", "ActorId", "dbo.Actor");
            DropForeignKey("dbo.MovieGenreMapping", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.MovieGenreMapping", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.MovieActorMapping", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.MovieActorMapping", "ActorId", "dbo.Actor");
            DropForeignKey("dbo.Movie", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Movie", "DirectorId", "dbo.Director");
            DropForeignKey("dbo.WatchlistRecord", "UserId", "dbo.User");
            DropForeignKey("dbo.WatchlistRecord", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.UserList", "UserId", "dbo.User");
            DropForeignKey("dbo.UserListRecord", "ListId", "dbo.UserList");
            DropForeignKey("dbo.RoleUser", "User_Id", "dbo.User");
            DropForeignKey("dbo.RoleUser", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.Rating", "UserId", "dbo.User");
            DropForeignKey("dbo.Rating", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.PollVotingRecord", "UserId", "dbo.User");
            DropForeignKey("dbo.PollVotingRecord", "VoteId", "dbo.PollVote");
            DropForeignKey("dbo.PollVote", "PollId", "dbo.Poll");
            DropForeignKey("dbo.Review", "UserId", "dbo.User");
            DropForeignKey("dbo.Review", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropForeignKey("dbo.BlogPostComment", "UserId", "dbo.User");
            DropForeignKey("dbo.BlogPostComment", "BlogPostId", "dbo.BlogPost");
            DropForeignKey("dbo.Comment", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.County", "StateId", "dbo.State");
            DropForeignKey("dbo.State", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Director", "CountyId", "dbo.County");
            DropIndex("dbo.RoleUser", new[] { "User_Id" });
            DropIndex("dbo.RoleUser", new[] { "Role_Id" });
            DropIndex("dbo.NewsMapping", new[] { "NewsCategoryId" });
            DropIndex("dbo.NewsMapping", new[] { "DirectorId" });
            DropIndex("dbo.NewsMapping", new[] { "ActorId" });
            DropIndex("dbo.NewsMapping", new[] { "MovieId" });
            DropIndex("dbo.NewsMapping", new[] { "NewsId" });
            DropIndex("dbo.MovieGenreMapping", new[] { "GenreId" });
            DropIndex("dbo.MovieGenreMapping", new[] { "MovieId" });
            DropIndex("dbo.MovieActorMapping", new[] { "ActorId" });
            DropIndex("dbo.MovieActorMapping", new[] { "MovieId" });
            DropIndex("dbo.WatchlistRecord", new[] { "MovieId" });
            DropIndex("dbo.WatchlistRecord", new[] { "UserId" });
            DropIndex("dbo.UserListRecord", new[] { "ListId" });
            DropIndex("dbo.UserList", new[] { "UserId" });
            DropIndex("dbo.Rating", new[] { "MovieId" });
            DropIndex("dbo.Rating", new[] { "UserId" });
            DropIndex("dbo.PollVote", new[] { "PollId" });
            DropIndex("dbo.PollVotingRecord", new[] { "UserId" });
            DropIndex("dbo.PollVotingRecord", new[] { "VoteId" });
            DropIndex("dbo.Review", new[] { "UserId" });
            DropIndex("dbo.Review", new[] { "MovieId" });
            DropIndex("dbo.BlogPostComment", new[] { "UserId" });
            DropIndex("dbo.BlogPostComment", new[] { "BlogPostId" });
            DropIndex("dbo.Comment", new[] { "MovieId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Movie", new[] { "CountryId" });
            DropIndex("dbo.Movie", new[] { "LanguageId" });
            DropIndex("dbo.Movie", new[] { "DirectorId" });
            DropIndex("dbo.State", new[] { "CountryId" });
            DropIndex("dbo.County", new[] { "StateId" });
            DropIndex("dbo.Director", new[] { "CountyId" });
            DropIndex("dbo.Director", new[] { "StateId" });
            DropIndex("dbo.Director", new[] { "CountryId" });
            DropIndex("dbo.Actor", new[] { "CountyId" });
            DropIndex("dbo.Actor", new[] { "StateId" });
            DropIndex("dbo.Actor", new[] { "CountryId" });
            DropTable("dbo.RoleUser");
            DropTable("dbo.NewsCategory");
            DropTable("dbo.News");
            DropTable("dbo.NewsMapping");
            DropTable("dbo.Genre");
            DropTable("dbo.MovieGenreMapping");
            DropTable("dbo.MovieActorMapping");
            DropTable("dbo.Language");
            DropTable("dbo.WatchlistRecord");
            DropTable("dbo.UserListRecord");
            DropTable("dbo.UserList");
            DropTable("dbo.Role");
            DropTable("dbo.Rating");
            DropTable("dbo.Poll");
            DropTable("dbo.PollVote");
            DropTable("dbo.PollVotingRecord");
            DropTable("dbo.Review");
            DropTable("dbo.BlogPost");
            DropTable("dbo.BlogPostComment");
            DropTable("dbo.User");
            DropTable("dbo.Comment");
            DropTable("dbo.Movie");
            DropTable("dbo.State");
            DropTable("dbo.County");
            DropTable("dbo.Director");
            DropTable("dbo.Country");
            DropTable("dbo.Actor");
        }
    }
}
