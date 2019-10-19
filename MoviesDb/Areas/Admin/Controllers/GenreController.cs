using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Common.Contracts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Ajax.Utilities;
using MoviesDb.Areas.Admin.Constants;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GenreController : ViewControllerBase
    {
        public IGenreService _genreService;
        public IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public GenreController(IGenreService genreService, IProxyFactory proxyFactory)
        {
            _genreService = genreService;
            _proxyFactory = proxyFactory;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            // base.RegisterServices(disposableServices); // Ne işe yaradığını unuttum. Tekrar öğrenilecek.
            disposableServices.Add(_genreService);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            genre.Validate();
            
            if (!genre.ValidationErrors.Any())
            {
                var genreUpdated = _genreService.Update(genre);

                TempData["Status"] = CrudNotification.Success;
                TempData["Id"] = genreUpdated.Id;

                return View();
            }
            else
            {
                ViewBag.ValidationErrors = genre.ValidationErrors.ToList();
                TempData["Status"] = CrudNotification.ValidationError;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Create");
            }
            else
            {
                var genre = new Genre();
                genre = _genreService.Get((int)id);

                return View(genre);
            }
        }

        [HttpPost]
        public ActionResult Edit(Genre genreUpdated)
        {
            genreUpdated.Validate();

            if (!genreUpdated.ValidationErrors.Any())
            {
                _genreService.Update(genreUpdated);
                return View();
            }
            else
            {
                ViewBag.ValidationErrors = genreUpdated.ValidationErrors.ToList();
                TempData["Status"] = CrudNotification.ValidationError;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Statistics()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Statistics(object something)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            _genreService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Genre_Read([DataSourceRequest]DataSourceRequest request, string searchTerm)
        {
            if (searchTerm.IsNullOrWhiteSpace())
            {
                Genre[] genres = _genreService.GetByPage(request.Page, request.PageSize);
                int genreCount = _genreService.TotalCount();

                return Json(new DataSourceResult
                {
                    Data = genres,
                    Total = genreCount
                });
            }
            else
            {
                Genre[] genres = _genreService.FindByName(searchTerm);
                int genreCount = genres.Count();

                return Json(new DataSourceResult
                {
                    Data = genres,
                    Total = genreCount
                });
            }
        }

        public ActionResult SubGenre_Read([DataSourceRequest]DataSourceRequest request)
        {
            Genre[] genres = _genreService.GetByPage(request.Page, request.PageSize);
            int genreCount = _genreService.TotalCount();

            return Json(new DataSourceResult
            {
                Data = genres,
                Total = genreCount
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genre_Update([DataSourceRequest] DataSourceRequest request, Genre model)
        {
            //throw new Exception("selam");
            model.Validate();

            if (!model.ValidationErrors.Any())
            {
                _genreService.Update(model);
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                // NOTFINISHED
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genre_Delete([DataSourceRequest] DataSourceRequest request, Genre model)
        {
            _genreService.Delete(model.Id);
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
	}
}