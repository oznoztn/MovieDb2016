using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using Microsoft.Office.Interop.Excel;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Business.Services.Services;
using MoviesDB.Client.Proxies;
using MoviesDB.Data;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IMDBCrawlerJson();
            //Seeder.SeedActorsTop1000();
            //Seed_RatedMoviesXML_MovieInfo_Actors();
            //Seed_Empire500XML_MovieInfo_Actors();
            //Seed_Empire500XML_MovieInfo_Actors_SeedGenres();

            //var c = XmlToXMLContainer();
            //var a = c.Where(t => t.Country == "New");
        }

        public static void New()
        {

            var consts = File.ReadAllLines(@"C:\seed\a\const.txt", Encoding.Default);
            
            var xsavePath = @"C:\seed\XML2\";
            var isavePath = @"C:\seed\Images2\";
            
            using (var context = new MovieDbContext())
            {
                foreach (var cn in consts)
                {
                    if (context.MovieSet.FirstOrDefault(t => t.ImdbLink == cn) == null)
                    {
                        using (WebClient client = new WebClient())
                        {
                            var url = string.Format("http://www.omdbapi.com/?i={0}&plot=short&r=xml", cn);
                            var apiOutput = client.DownloadData(url);

                            XDocument xDocument = My_ArrayToXDocument(apiOutput);

                            Thread.Sleep(new Random().Next(1000, 3000));

                            var imageUrl = xDocument.Descendants("movie").Attributes().Single(t => t.Name == "poster").Value;
                            var imageName = xDocument.Descendants("movie").Attributes().Single(t => t.Name == "title").Value
                                .Replace(" ", "-")
                                .Replace(":", "")
                                .Replace("?", "")
                                .Replace("*", "")
                                .Replace("/", "").ToLowerInvariant();

                            client.DownloadFile(imageUrl, isavePath + imageName + ".jpg");
                            xDocument.Save(xsavePath + cn + ".xml");

                        }
                    }
                }
            }
        }

        private static void LINQSCOTTKALLEN()
        {
            var entityContext = new MovieDbContext();
            var movieRepo = new MovieRepository();
            var actorRepo = new ActorRepository(); ;
            var dirRepo = new DirectorRepository();

            // LET 
            var query =
                from m in entityContext.MovieSet
                where m.Name.ToLower() == "matrix"
                select m.Name.ToLower();

            var query2 =
                from m in entityContext.MovieSet
                let lname = m.Name.ToLower()
                where lname == "matrix"
                select lname;

            // The into keyword is to continue a query after a projection
            //  Original range variable goes out of scope

            var moviesOfSylvester =
                from m in entityContext.MovieSet
                join d in entityContext.DirectorSet on m.DirectorId equals d.Id
                where d.FullName == "Sylvester Stallone"
                select m
                    into sMovie
                    where sMovie.Year > 2000
                    select sMovie;

            // Genel olarak linq ile gruplama yaparken kullanılır
        }

        /// <summary>
        /// Bu metot 246 filmde oynayan aktörlerin isimlerini alıyor
        /// Bu isimlerde veritabanında aktör var ise
        /// Bu aktörler ile ActorMovieMap tablosunu seed'liyor.
        /// Aktör yok ise bir şey yapmıyor
        /// DUPLICATE SORUNU YOK
        /// </summary>
        private static void Seed_RatedMoviesXML_MovieInfo_Actors()
        {
            var listXMContainer = new List<XMLContainer>();
            var xdocList = new List<XDocument>();

            var xdocActors = new List<string>();

            var folderPath = @"C:\seed\xml\";

            foreach (string file in Directory.EnumerateFiles(folderPath, "*.xml"))
            {
                string contents = File.ReadAllText(file, Encoding.UTF8);
                xdocList.Add(XDocument.Parse(contents));
            }

            foreach (XDocument xDocument in xdocList)
            {
                var container = new XMLContainer();
                container.Actors = new List<string>();

                foreach (var xx in xDocument.Descendants("movie").Attributes())
                {
                    switch (xx.Name.ToString())
                    {
                        case "actors":
                            var splitted = xx.Value.Split(',');
                            foreach (var per in splitted)
                            {
                                if (per[0].ToString() == " ")
                                {
                                    xdocActors.Add(per.Remove(0, 1));
                                    container.Actors.Add(per.Remove(0, 1));
                                }
                                else
                                {
                                    xdocActors.Add(per);
                                    container.Actors.Add(per);
                                }
                            }
                            break;

                        case "plot":
                            container.Plot = xx.Value;
                            break;

                        case "language":
                            container.Language = xx.Value;
                            break;

                        case "imdbID":
                            container.ImdbId = xx.Value;
                            break;
                    }
                }
                listXMContainer.Add(container);
            }

            using (var context = new MovieDbContext())
            {
                foreach (var xmlContainer in listXMContainer)
                {
                    var movie = (from m in context.MovieSet
                                 where m.ImdbLink == xmlContainer.ImdbId
                                 select m).First();

                    movie.PlotOutline = xmlContainer.Plot;

                    foreach (var actor in xmlContainer.Actors)
                    {
                        var possibleNullActor = context.ActorSet.FirstOrDefault(t => t.FullName == actor);
                        if (possibleNullActor == null)
                        {
                            // aktör yoksa bir şey yapma
                        }
                        else
                        {
                            // aktör var
                            // peki hali hazırda map var mı?

                            MovieActorMapping possibleNullMap =
                                context.MovieActorMappingSet.FirstOrDefault(
                                    t => t.MovieId == movie.Id && t.ActorId == possibleNullActor.Id);

                            if (possibleNullMap == null) // map yok
                            {
                                context.MovieActorMappingSet.Add(new MovieActorMapping()
                                {
                                    ActorId = possibleNullActor.Id,
                                    MovieId = movie.Id,
                                });
                                context.SaveChanges();
                            }
                        }
                    }

                    string movieLanguage = xmlContainer.Language.Split(',')[0];
                    var possibleNullLanguage = context.LanguageSet.FirstOrDefault(t => t.Name == movieLanguage);

                    if (possibleNullLanguage == null)
                    {
                        var language = new Language() {Name = movieLanguage};
                        context.LanguageSet.Add(language);
                        context.SaveChanges();

                        movie.LanguageId = language.Id;
                    }
                    else
                    {
                        movie.LanguageId = possibleNullLanguage.Id;
                        context.SaveChanges();
                    }
                }

            }
        }

        /// <summary>
        /// Bir klasör dolusu XML'i List tipinde dönderiyor
        /// </summary>
        /// <returns></returns>
        public static List<XMLContainer> XmlToXMLContainer()
        {
            var listXMContainer = new List<XMLContainer>();
            var xdocList = new List<XDocument>();

            var xdocActors = new List<string>();

            var folderPath = @"C:\seed\xml2\";

            foreach (string file in Directory.EnumerateFiles(folderPath, "*.xml"))
            {
                string contents = File.ReadAllText(file, Encoding.UTF8);
                xdocList.Add(XDocument.Parse(contents));
            }

            foreach (XDocument xDocument in xdocList)
            {
                var container = new XMLContainer
                {
                    Genres = new List<string>(),
                    Actors = new List<string>(),
                    Directors = new List<string>()
                };

                foreach (var xx in xDocument.Descendants("movie").Attributes())
                {
                    if (xx.Name.ToString() == "actors")
                    {
                        var splitted = xx.Value.Split(',');
                        foreach (var per in splitted)
                        {
                            if (per[0].ToString() == " ")
                            {
                                xdocActors.Add(per.Remove(0, 1));
                                container.Actors.Add(per.Remove(0, 1));
                            }
                            else
                            {
                                xdocActors.Add(per);
                                container.Actors.Add(per);
                            }
                        }
                    }
                    else if (xx.Name.ToString() == "director")
                    {
                        var directors = xx.Value.Split(',');

                        foreach (var director in directors)
                        {
                            container.Directors.Add(director);
                        }
                    }
                    else if (xx.Name.ToString() == "genre")
                    {
                        var genres = xx.Value.Split(',');

                        foreach (var genre in genres)
                        {
                            container.Genres.Add(genre);
                        }
                    }
                    else if (xx.Name.ToString() == "year")
                    {
                        container.Year = int.Parse(xx.Value);
                    }
                    else if (xx.Name.ToString() == "country")
                    {
                        container.Country = xx.Value;
                    }
                    else if (xx.Name.ToString() == "runtime")
                    {
                        container.Runtime = int.Parse(xx.Value.Split(' ')[0]);
                    }
                    else if (xx.Name.ToString() == "title")
                    {
                        container.MovieTitle = xx.Value;
                    }

                    else if (xx.Name.ToString() == "plot")
                    {
                        container.Plot = xx.Value;
                    }
                    else if (xx.Name.ToString() == "language")
                    {
                        container.Language = xx.Value;
                    }
                    else if (xx.Name.ToString() == "imdbID")
                    {
                        container.ImdbId = xx.Value;
                    }
                }
                listXMContainer.Add(container);
            }
            return listXMContainer;
        } 

        private static void Seed_Empire500XML_MovieInfo_Actors()
        {
            var listXmlContainer = XmlToXMLContainer();

            using (var context = new MovieDbContext())
            {
                var randomDate = new RandomDateTime();
                foreach (var xmlContainer in listXmlContainer)
                {
                    var movie = (from m in context.MovieSet
                                 where m.ImdbLink == xmlContainer.ImdbId
                                 select m).FirstOrDefault();

                    if (movie == null)
                    {
                        var movieToDB = new Movie();
                        movieToDB.ImdbLink = xmlContainer.ImdbId;
                        movieToDB.Name = xmlContainer.MovieTitle;
                        movieToDB.RunningTime = xmlContainer.Runtime;
                        movieToDB.Year = xmlContainer.Year;
                        movieToDB.PlotOutline = xmlContainer.Plot;
                        movieToDB.CoverImage = xmlContainer.MovieTitle
                                                .Replace(" ", "-").Replace(":", "")
                                                .Replace("?", "").Replace("*", "")
                                                .Replace("/", "").ToLowerInvariant() + ".jpg";
                        
                        using (var ctx = new MovieDbContext())
                        {
                            bool directorMapped = false;
                            foreach (var directorName in xmlContainer.Directors)
                            {
                                var possibleNullDirector = new Director();
                                possibleNullDirector = ctx.DirectorSet.FirstOrDefault(t => t.FullName == directorName);

                                if (possibleNullDirector != null)
                                {
                                    movieToDB.DirectorId = possibleNullDirector.Id;
                                    directorMapped = true;
                                }
                                break;
                            }

                            if (!directorMapped)
                            {
                                var nameSplitted = xmlContainer.Directors[0].Split(' ');
                                string fname = "";
                                string lname = "";
                                string fullName = "";

                                switch (nameSplitted.Count())
                                {
                                    case 1:
                                        fullName = nameSplitted[0];
                                        break;
                                    case 2:
                                        fname = nameSplitted[0];
                                        lname = nameSplitted[1];
                                        fullName = string.Format("{0} {1}", nameSplitted[0], nameSplitted[1]);
                                        break;
                                    case 3:
                                        fname = nameSplitted[0];
                                        lname = nameSplitted[1] + " " + nameSplitted[2];
                                        fullName = string.Format("{0} {1} {2}", nameSplitted[0], nameSplitted[1], nameSplitted[2]);
                                        break;
                                    case 4:
                                        fname = nameSplitted[0];
                                        lname = nameSplitted[1] + " " + nameSplitted[2] + " " + nameSplitted[3];
                                        fullName = string.Format("{0} {1} {2} {3}", nameSplitted[0], nameSplitted[1], nameSplitted[2], nameSplitted[3]);
                                        break;
                                }

                                var director = new Director
                                {
                                    FirstName = fname,
                                    LastName = lname,
                                    FullName = fullName,
                                    Biography = null,
                                    CountryId = 1,
                                    StateId = 1,
                                    CountyId = 1,
                                    CreatedAt = randomDate.Next(),
                                    Gender = true,
                                    ImdbLink = null,
                                    Photo = null
                                };
                                ctx.DirectorSet.Add(director);
                                ctx.SaveChanges();

                                movieToDB.DirectorId = director.Id;
                            }
                        }

                        string movieLanguage = xmlContainer.Language.Split(',')[0];
                        var possibleNullLanguage = context.LanguageSet.FirstOrDefault(t => t.Name == movieLanguage);

                        if (possibleNullLanguage == null)
                        {
                            var language = new Language() { Name = movieLanguage };
                            context.LanguageSet.Add(language);
                            context.SaveChanges();

                            movieToDB.LanguageId = language.Id;
                        }
                        else
                        {
                            movieToDB.LanguageId = possibleNullLanguage.Id;
                        }

                        var stringCountry = xmlContainer.Country.Split(',')[0];
                        var possibleNullCountry = context.CountrySet.FirstOrDefault(t => t.Name == stringCountry);
                        if (possibleNullCountry == null)
                        {
                            var country = new Country() { Name = stringCountry };
                            
                            context.CountrySet.Add(country);
                            context.SaveChanges();

                            movieToDB.CountryId = country.Id;
                        }
                        else
                        {
                            movieToDB.CountryId = possibleNullCountry.Id;
                        }

                        context.MovieSet.Add(movieToDB);
                        context.SaveChanges();

                        foreach (var actor in xmlContainer.Actors)
                        {
                            var possibleNullActor = context.ActorSet.FirstOrDefault(t => t.FullName == actor);
                            if (possibleNullActor == null)
                            {
                                // aktör yoksa bir şey yapma
                            }
                            else
                            {
                                context.MovieActorMappingSet.Add(new MovieActorMapping()
                                {
                                    ActorId = possibleNullActor.Id,
                                    MovieId = movieToDB.Id,
                                });
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        private static void Seed_Empire500XML_MovieInfo_Actors_SeedGenres()
        {
            var listXmlContainer = XmlToXMLContainer();
            var context = new MovieDbContext();

            foreach (var xmlContainer in listXmlContainer)
            {
                var movie = context.MovieSet.Single(t => t.ImdbLink == xmlContainer.ImdbId).Id;
                foreach (var genre in xmlContainer.Genres)
                {
                    string genreModified = genre.Replace(" ", "");
                    Genre existingGenre = context.GenreSet.First(t => t.Name == genreModified);

                    var genreMap = new MovieGenreMapping(){MovieId = movie, GenreId = existingGenre.Id};
                    context.MovieGenreMappingSet.Add(genreMap);
                }
            }
            context.SaveChanges();

        }
        
        private static void Fix_TOP1000_Actors()
        {
            var silinecekOlanlar = new List<string>();
            var tekIsimliler = new List<XElement>();

            var top1000 = XDocument.Load(@"C:\seed\New_\1000actors.xml");

            List<XElement> a = top1000.Element("Top1000").Elements("Actor").ToList();

            foreach (var xElement in a)
            {
                string old = xElement.Elements().Where(element => element.Name == "Name").ToList()[0].Value;
                string neww = Regex.Replace(old, @"[^\u0000-\u007F]", string.Empty);

                if (old != neww)
                    silinecekOlanlar.Add(old);
            }

            var aaaaaaaa = top1000.Element("Top1000").Elements("Actor").Elements("Name");
            var counter = 0;
            foreach (XElement actor in top1000.Element("Top1000").Elements("Actor").Elements("Name"))
            {
                if (actor.ToString().Split(' ').Count() == 1)
                {
                    actor.Parent.RemoveAll();
                }

                else
                {
                    foreach (var item in silinecekOlanlar)
                    {
                        if (item == actor.Value) // bozuk isimlileri sil
                        {
                            actor.Parent.RemoveAll();
                            counter++;
                        }
                    }
                }
            }
            top1000.Save(@"c:\seed\New_\yeniTop1000.xml");
        }

        private static void Fix_TOP1000_MovieDb500_DuplicatesRemoving()
        {
            var context = new MovieDbContext();
            var xml = XDocument.Load(@"c:\seed\new_\yeniTop1000.xml");

            var top1000 = context.ActorSet.Select(t => t.FullName).ToList();
            var actorsInXml = xml.Element("Top1000").Elements("Actor").Elements("Name").Select(element => element.Value).ToList();

            foreach (var act in actorsInXml)
            {
                foreach (var name in top1000)
                {
                    if (act == name)
                    {
                        var parent =
                            xml.Elements("Top1000").Elements("Actor").Elements("Name").First(t => t.Value == act).Parent;

                        parent.RemoveAll();
                    }
                }
            }

            xml.Save(@"c:\seed\new_\aynilarsilindi.xml");


        }

        private static XDocument My_ArrayToXDocument(byte[] array)
        {
            using (MemoryStream oStream = new MemoryStream(array))
            using (XmlTextReader oReader = new XmlTextReader(oStream))
            {
                return XDocument.Load(oReader);
            }
        }
        
        private static void Deneme()
        {
            var xdocList = new List<XDocument>();

            var folderPath = @"C:\seed\xml\";
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.xml"))
            {
                string contents = File.ReadAllText(file, Encoding.UTF8);
                xdocList.Add(XDocument.Parse(contents));
            }

            foreach (XDocument xDocument in xdocList)
            {
                var imdbId = "";
                var xBaseDocument = new XDocument();
                var xelementMovie = new XElement("Movie");

                var descMovie = xDocument.Descendants("movie");
                var attributes = descMovie.Attributes();

                foreach (var attr in attributes)
                {
                    if (attr.Name == "imdbID")
                    {
                        imdbId = attr.Value;
                    }
                    xelementMovie.Add(new XElement(attr.Name, attr.Value));
                    Console.WriteLine(attr.Value);
                }
                xBaseDocument.Add(xelementMovie);
                xBaseDocument.Save(@"c:\seed\yeni\" + imdbId + ".xml");
            }
        }

        private static void IMDBCrawler()
        {
            var random = new Random();
            var imdbLinks = File.ReadAllLines(@"C:\seed\imdblink.txt");
            
            var xsavePath = @"C:\seed\XML\";
            var isavePath = @"C:\seed\Images\";
           
            using (WebClient webClient = new WebClient())
            {
                foreach (var link in imdbLinks)
                {
                    var url = string.Format("http://www.omdbapi.com/?i={0}&plot=short&r=xml", link);
                    var apiOutput = webClient.DownloadData(url);

                    XDocument xDocument = My_ArrayToXDocument(apiOutput);

                    Thread.Sleep(random.Next(1000, 3000));

                    var imageUrl = xDocument.Descendants("movie").Attributes().Single(t => t.Name == "poster").Value;
                    var imageName = xDocument.Descendants("movie").Attributes().Single(t => t.Name == "title").Value
                        .Replace(" ", "-")
                        .Replace(":", "")
                        .Replace("?", "")
                        .Replace("*", "")
                        .Replace("/", "").ToLowerInvariant();

                    webClient.DownloadFile(imageUrl, isavePath + imageName + ".jpg");
                    xDocument.Save(xsavePath + link + ".xml");
                }
            }
        }

        private static void IMDBCrawlerJson()
        {
            var random = new Random();
            var imdbLinks = File.ReadAllLines(@"C:\imdblink.txt");

            var xsavePath = @"C:\seed2\json\";
            var isavePath = @"C:\seed2\images\";

            using (WebClient webClient = new WebClient())
            {
                foreach (var link in imdbLinks)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    var jsonUrl = string.Format("http://www.omdbapi.com/?i={0}&plot=short&r=json", link);

                    var apiOutput = webClient.DownloadData(jsonUrl);                    

                    using (var reader = new StreamReader(new MemoryStream(apiOutput), Encoding.UTF8))
                    {
                        string material = reader.ReadToEnd();
                        File.WriteAllText(xsavePath+link+".json", material);
                    }
                }
            }
        }


        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

        [Serializable]
        public class XMLContainer
        {
            public string ImdbId { get; set; }
            public string Plot { get; set; }
            public List<string> Actors { get; set; }
            public string Language { get; set; }
            public string Country { get; set; }

            public string MovieTitle { get; set; }
            public List<string> Directors { get; set; }
            public int Runtime { get; set; }
            public List<string> Genres { get; set; }
            public int Year { get; set; }
        }

    }
}