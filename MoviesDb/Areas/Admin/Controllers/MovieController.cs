using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using MoviesDb.Areas.Admin.Constants;
using MoviesDb.Common;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MovieController : ViewControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public MovieController(IMovieService movieService, IProxyFactory proxyFactory)
        {
            _movieService = movieService;
            _proxyFactory = proxyFactory;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_movieService);
        }

        [HttpGet]
        public ActionResult Deneme()
        {
            ViewBag.Dict = new Dictionary<int, string>();
            ((Dictionary<int, string>)ViewBag.Dict).Add(1, "Admin");
            ((Dictionary<int, string>)ViewBag.Dict).Add(2, "Moderator");

            return View();
        }

        [HttpPost]
        public ActionResult Deneme(object[] values)
        {
            return View(values);
        }

        [HttpGet]
        public ActionResult Index()
        {
            Language[] languages = null;
            Genre[] genres = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<ILanguageService>(), proxy =>
            {
                languages = proxy.GetAll();
            });

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IGenreService>(), proxy =>
            {
                genres = proxy.GetAll();
            });

            ViewBag.Languages = languages;
            ViewBag.Genres = genres;

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            Language[] languages = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<ILanguageService>(),
                proxy =>
                {
                    languages = proxy.GetAll();
                });

            List<SelectListItem> languageList = languages.Select(lang => new SelectListItem() { Text = lang.Name, Value = lang.Id.ToString() }).ToList();

            ViewBag.Languages = languageList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(MovieCreationData incomingMovie, HttpPostedFileBase image)
        {
            incomingMovie.Validate();

            if (!incomingMovie.ValidationErrors.Any())
            {
                //_movieService.UpdateSec(incomingMovie);
                TempData["Status"] = CrudNotification.Success;
                return View("Create");
            }
            else
            {
                ViewBag.ValidationErrors = incomingMovie.ValidationErrors.ToList();
                TempData["Status"] = CrudNotification.ValidationError;
                return View();
            }           
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == 0)
                return View("Select");
            try
            {
                MovieCreationData movie = _movieService.GetSec(id.Value);

                List<SelectListItem> languages = null;

                RunAndReleaseProxy(_proxyFactory.CreateProxy<ILanguageService>(), proxy =>
                {
                    languages =
                        proxy.GetAll()
                            .Select(t => new SelectListItem() {Text = t.Name, Value = t.Id.ToString()})
                            .ToList();
                });
                ViewBag.Languages = languages;

                return View(movie);
            }
            catch (FaultException<UniformException> e)
            {
                return Content(string.Format("{0} {1}", e.Detail.Message, e.Detail.Reason));
            }
            catch (FaultException<CommunicationException> e)
            {
                return Content(string.Format("(CE) {0} / {1}", e.Message, e.Reason));
            }
            catch (FaultException e)
            {
                return Content(string.Format("(DEF F.E.) {0} / {1}", e.Message, e.Reason));
            }
        }

        [HttpPost]
        public ActionResult Edit(MovieCreationData updatedMovie)
        {
            updatedMovie.Validate();

            if (!updatedMovie.ValidationErrors.Any())
            {
                var updetedMovie = _movieService.UpdateSec(updatedMovie);
                return View(updatedMovie);
            }
            else
            {
                ViewBag.ValidationErrors = updatedMovie.ValidationErrors.ToList();
                TempData["Status"] = CrudNotification.ValidationError;
                return View(updatedMovie);
            }
        }

        [HttpGet]
        public ActionResult Statistics()
        {
            ViewBag.jsonMovieGenre = _movieService.Statistics_MoviesByGenre();
            ViewBag.jsonMovieYear = _movieService.Statistics_MoviesByYear();
            
            return View();
        }

        [HttpPost]
        public ActionResult Statistics(object something)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var movieDetails = new MovieDetailsData();

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IMovieService>(), proxy =>
            {
                movieDetails = proxy.GetDetails(id);
            });

            return View(movieDetails);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult Movie_Read([DataSourceRequest]DataSourceRequest request, string searchTerm)
        {
            if (searchTerm.IsEmpty())
            {
                MovieData[] movies = _movieService.GetByPage(request.Page, request.PageSize);
                int movieCount = _movieService.TotalCount();

                return Json(new DataSourceResult
                {
                    Data = movies,
                    Total = movieCount
                });
            }
            else
            {
                MovieData[] movies = _movieService.SearchMovie(searchTerm);
                int movieCount = movies.Count();

                return Json(new DataSourceResult
                {
                    Data = movies,
                    Total = movieCount
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Movie_Update([DataSourceRequest] DataSourceRequest request, MovieData model)
        {
            model.Validate();

            if (!model.ValidationErrors.Any())
            {
                _movieService.UpdateSimple(model);
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                // NOTFINISHED
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Movie_Delete([DataSourceRequest] DataSourceRequest request, Movie model)
        {
            _movieService.Delete(model.Id);
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Director_Read(string text)
        {
            DirectorData[] directorDatas = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IDirectorService>(), proxy =>
            {
                directorDatas = proxy.DirectorsForDropdownList();
            });

            return Json(directorDatas.Select(t => new { Name = t.Name, Id = t.Id}), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Language_Read(string text)
        {
            Language[] directorDatas = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<ILanguageService>(), proxy =>
            {
                directorDatas = proxy.GetAll();
            });

            return Json(directorDatas.Select(t => new { Name = t.Name, Id = t.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Country_Read(string text)
        {
            Country[] directorDatas = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<ICountryService>(), proxy =>
            {
                directorDatas = proxy.GetAll();
            });

            return Json(directorDatas.Select(t => new { Name = t.Name, Id = t.Id }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Actor_Read(string text)
        {
            ActorData[] actors = null;

            if (!string.IsNullOrEmpty(text))
            {
                RunAndReleaseProxy(_proxyFactory.CreateProxy<IActorService>(), proxy =>
                {
                    actors = proxy.FindByName(text).ToArray();
                });

                return Json(actors, JsonRequestBehavior.AllowGet);
            }

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IActorService>(), proxy =>
            {
                actors = proxy.ActorsForDropdownList();
                //actors = proxy.GetAll().Select(t => new ActorData(){Name = t.FullName, Id = t.Id}).ToArray();
            });

            return Json(actors, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Genre_Read(string text)
        {
            Genre[] genres = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IGenreService>(), proxy => { genres = proxy.GetAll(); });

            return Json(genres, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SubGenre_Read(string text)
        {
            Genre[] genres = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IGenreService>(), proxy => { genres = proxy.GetAllSubs(); });

            return Json(genres, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Movie_Search(string text)
        {
            if (text.IsNullOrWhiteSpace())
                return null;

            MovieData[] movieDatas = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IMovieService>(), proxy =>
            {
                movieDatas = proxy.SearchMovie(text);
            });

            return Json(movieDatas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Movie_SearchIMDB(string text)
        {
            if (text.IsNullOrWhiteSpace())
                return null;

            MovieData movie = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IMovieService>(), proxy =>
            {
                movie = proxy.GetMovieByImdbId(text);
            });

            return Json(movie, JsonRequestBehavior.AllowGet);
        }
	}
}