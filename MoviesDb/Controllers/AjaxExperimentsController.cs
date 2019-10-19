using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Core.Common.Contracts;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDb.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AjaxExperimentsController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public AjaxExperimentsController(IMovieService movieService, IProxyFactory proxyFactory)
        {
            _movieService = movieService;
            _proxyFactory = proxyFactory;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetSampleData()
        {
            return Json("Greetings from MVC Controller");
        }

        // Returns json array
        [HttpPost]
        public ActionResult GetMovie_WithoutId()
        {
            Movie[] movie = { _movieService.Get(1) };

            // Projection
            return Json(movie.Select(t => new
            {
                isim = t.Name,
                imdb = t.ImdbLink,
                poster = t.CoverImage ?? "Resim yok.",
                konu = t.PlotOutline ?? "Konu tanımlanmamış.",
            }));
        }

        [HttpPost]
        public ActionResult GetMovie_WithId(int id)
        {
            try
            {
                Movie[] movie = { _movieService.Get(id) };

                return Json(movie.Select(t => new
                {
                    isim = t.Name,
                    imdb = t.ImdbLink,
                    poster = t.CoverImage ?? "Resim yok.",
                    konu = t.PlotOutline ?? "Konu tanımlanmamış.",
                }), JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpPost]
        public JsonResult GetMovies(List<int> movieIds)
        {
            var movies = movieIds.Select(id => _movieService.Get(id)).ToList();

            return Json(movies.Select(t => new
            {
                isim = t.Name,
                imdb = t.ImdbLink,
            }), JsonRequestBehavior.AllowGet);
        }
    }

    class MovieJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}