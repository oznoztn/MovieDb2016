using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;
using MoviesDB.Business.Entities;
using MoviesDB.Business.Entities.DTOs;
using MoviesDB.Business.Services.Services;
using MoviesDB.Client.Proxies;
using MoviesDB.Data;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var movieRepository = new MovieRepository();            
            var stats = movieRepository.Statistics_MoviesByGenre();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string json = jsSerializer.Serialize(stats);
            Console.WriteLine(json);

            using (var entityContext = new MovieDbContext())
            {
                //// genel - yıllara göre filmler
                //var movies = (from m in entityContext.MovieSet 
                //              group m by new {m.Year} into grp
                //              select new StatsMovieYear()
                //              {
                //                  Count = grp.Count(),
                //                  Year = grp.Key.Year
                //              }).ToList();

                //// genel - genre'ye göre filmler
                //var moviex = (from g in entityContext.GenreSet 
                //              join gm in entityContext.MovieGenreMappingSet on g.Id equals gm.GenreId
                //              group g by new {g.Name} into grp select new StatsMovieGenre()
                //              {
                //                  Count = grp.Count(),
                //                  Genre = grp.Key.Name
                //              }).ToList();

                //// ratin distrubition kaç filme kaç puan
                //var userId = 1;
                //var moviep = (from u in entityContext.UserSet where u.Id == userId
                //              join r in entityContext.RatingSet on u.Id equals r.UserId
                //              group r by new {r.Rate} into grp select new StatsRatingDistribution
                //              {
                //                  Rate = grp.Key.Rate,
                //                  Count = grp.Count()
                //              }).ToList();

                //// ratin distrubition yıllara göre film sayısı
                //var rtndstbyYear = (from u in entityContext.UserSet where u.Id == userId
                //                    join r in entityContext.RatingSet on u.Id equals r.UserId
                //                    join m in entityContext.MovieSet on r.MovieId equals m.Id
                //                    group m by new {m.Year} into grp select new StatsMovieYear()
                //                    {
                //                        Count = grp.Count(),
                //                        Year = grp.Key.Year
                //                    }).ToList();

                //// ratin distrubition hangi genre'ye ne kadar oy
                //var rtngGenreDist = (from u in entityContext.UserSet where u.Id == userId
                //                     join r in entityContext.RatingSet on u.Id equals r.UserId
                //                     join gm in entityContext.MovieGenreMappingSet on r.MovieId equals gm.MovieId
                //                     join g in entityContext.GenreSet on gm.GenreId equals g.Id
                //                     group g by new {g.Name} into grp select new StatsMovieGenre()
                //                     {
                //                         Genre = grp.Key.Name,
                //                         Count = grp.Count()
                //                     }).ToList();

            }

            // Seed_246XML_MovieInfo_Actors();
            // Seeder.SeedActorsTop1000();

            //var xdocList = new List<XDocument>();

            //foreach (string file in Directory.EnumerateFiles(@"C:\seed\xml\", "*.xml"))
            //{
            //    string contents = File.ReadAllText(file, Encoding.UTF8);
            //    xdocList.Add(XDocument.Parse(contents));
            //}

            //var countries = xdocList.Select(xml =>
            //    xml.Element("root").Element("movie").Attributes().Where(attr => attr.Name == "country")).ToList();

            //var clist = new List<string>();
            //foreach (var ccc in countries)
            //{
            //    foreach (var c in ccc)
            //    {
            //        clist.Add(c.Value);
            //    }
            //}
            //var distinctCountries = clist.Distinct();

            //var files = XDocument.Load(@"C:\seed\XML\tt1454029.xml");

            //var a = files.Element("root").Element("movie").Attributes().Where(t => t.Name == "country");

        }

        public static void Run<T>(T proxy, Action<T> codeAction)
        {
            codeAction.Invoke(proxy);
            Console.WriteLine("ok");
        }


        /// <summary>
        /// Bu metot 246 filmde oynayan aktörlerin isimlerini alıyor
        /// Bu isimlerde veritabanında aktör var ise
        /// Bu aktörler ile ActorMovieMap tablosunu seed'liyor.
        /// Aktör yok ise bir şey yapmıyor
        /// DUPLICATE SORUNU YOK
        /// </summary>
        private static void Seed_246XML_MovieInfo_Actors()
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
                            context.MovieActorMappingSet.Add(new MovieActorMapping()
                            {
                                ActorId = possibleNullActor.Id,
                                MovieId = movie.Id,
                            });
                            context.SaveChanges();
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

        private static void Fix_TOP1000_Actors()
        {
            var silinecekOlanlar = new List<string>();

            var top1000 = XDocument.Load(@"C:\seed\New_\1000actors.xml");

            var a = top1000.Element("Top1000").Elements("Actor").ToList();

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
                foreach (var item in silinecekOlanlar)
                {
                    if (item == actor.Value) // bozuk isimlileri sil
                    {
                        actor.Parent.RemoveAll();
                        counter++;
                    }
                    if (actor.Value.Split().Count() < 2) // tek isimli aktörleri sil
                    {
                        actor.Parent.RemoveAll();
                    }
                }
            }
            top1000.Save(@"c:\seed\New_\yeniTop100.xml");
            
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

            //xml.Save(@"c:\seed\new_\aynilarsilindi.xml");


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
            var imdbLinks = File.ReadAllLines(@"C:\seed\im_temp.txt");

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

        public static imdbitem GetInfoByTitle(string Title)
        {
            string url = "http://www.imdb.com/title/tt0133093";
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            req.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; WIndows NT 9.0; en-US))";
            string source;
            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                source = reader.ReadToEnd();
            }
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(source);
            XDocument xdoc = XDocument.Parse(doc.DocumentNode.InnerHtml, LoadOptions.None);
            imdbitem i = new imdbitem();
            i.rating = xdoc.Descendants("rating").Select(x => x.Value).FirstOrDefault();
            i.rating_count = xdoc.Descendants("rating_count").Select(x => x.Value).FirstOrDefault();
            i.year = xdoc.Descendants("year").Select(x => x.Value).FirstOrDefault();
            i.rated = xdoc.Descendants("rated").Select(x => x.Value).FirstOrDefault();
            i.title = xdoc.Descendants("title").Select(x => x.Value).FirstOrDefault();
            i.imdb_url = xdoc.Descendants("imdb_url").Select(x => x.Value).FirstOrDefault();
            i.plot_simple = xdoc.Descendants("plot_simple").Select(x => x.Value).FirstOrDefault();
            i.type = xdoc.Descendants("type").Select(x => x.Value).FirstOrDefault();
            i.poster = xdoc.Descendants("poster").Select(x => x.Value).FirstOrDefault();
            i.imdb_id = xdoc.Descendants("imdb_id").Select(x => x.Value).FirstOrDefault();
            i.also_known_as = xdoc.Descendants("also_known_as").Select(x => x.Value).FirstOrDefault();
            i.language = xdoc.Descendants("language").Select(x => x.Value).FirstOrDefault();
            i.country = xdoc.Descendants("country").Select(x => x.Value).FirstOrDefault();
            i.release_date = xdoc.Descendants("release_date").Select(x => x.Value).FirstOrDefault();
            i.filming_locations = xdoc.Descendants("filming_locations").Select(x => x.Value).FirstOrDefault();
            i.runtime = xdoc.Descendants("runtime").Select(x => x.Value).FirstOrDefault();
            i.directors = xdoc.Descendants("directors").Descendants("item").Select(x => x.Value).ToList();
            i.writers = xdoc.Descendants("writers").Descendants("item").Select(x => x.Value).ToList();
            i.actors = xdoc.Descendants("actors").Descendants("item").Select(x => x.Value).ToList();
            i.genres = xdoc.Descendants("genres").Descendants("item").Select(x => x.Value).ToList();
            return i;
        }

        public class imdbitem
        {
            public string rating { get; set; }
            public string rating_count { get; set; }
            public string year { get; set; }
            public string rated { get; set; }
            public string title { get; set; }
            public string imdb_url { get; set; }
            public string plot_simple { get; set; }
            public string type { get; set; }
            public string poster { get; set; }
            public string imdb_id { get; set; }
            public string also_known_as { get; set; }
            public string language { get; set; }
            public string country { get; set; }
            public string release_date { get; set; }
            public string filming_locations { get; set; }
            public string runtime { get; set; }
            public List<string> directors { get; set; }
            public List<string> writers { get; set; }
            public List<string> actors { get; set; }
            public List<string> genres { get; set; }
        }

        [Serializable]
        public class XMLContainer
        {
            public string ImdbId { get; set; }
            public string Plot { get; set; }
            public List<string> Actors { get; set; }
            public string Language { get; set; }
            public List<string> Countries { get; set; }
        }
    }
}