using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
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
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DirectorController : ViewControllerBase
    {
        private IDirectorService _directorService;
        private IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public DirectorController(IDirectorService directorService, IProxyFactory proxyFactory)
        {
            _directorService = directorService;
            _proxyFactory = proxyFactory;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            // base.RegisterServices(disposableServices); // Ne işe yaradığını unuttum. Tekrar öğrenilecek.
            disposableServices.Add(_directorService);
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
        public ActionResult Create(Director director, HttpPostedFileBase file)
        {
            //// image to binary
            //byte[] buffer = new byte[file.InputStream.Length];
            //file.InputStream.Read(buffer, 0, (int) file.InputStream.Length);
            //file.InputStream.Close();
            
            director.Validate();

            if (!director.ValidationErrors.Any())
            {
                //_directorService.Update(director);

                TempData["Status"] = CrudNotification.Success;
                return View();
            }
            else
            {
                ViewBag.ValidationErrors = director.ValidationErrors.ToList();
                TempData["Status"] = CrudNotification.ValidationError;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("Select");
            }
            else
            {
                var director = _directorService.Get((int) id);

                return View(director);
            }
        }

        [HttpPost]
        public ActionResult Edit(Director directorUpdated)
        {
            _directorService.Update(directorUpdated);

            //directorUpdated.Validate();

            //if (!directorUpdated.ValidationErrors.Any())
            //{
            //    _directorService.Update(directorUpdated);
            //}
            
            return View();
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

        [HttpGet]
        public ActionResult Details(int id)
        {
            var directorDetails = _directorService.GetDetails(id);

            if (directorDetails.Id == 0)
                return Content("There's no director for given Id");

            return View(directorDetails);
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Directors_Movie_Read([DataSourceRequest]DataSourceRequest request)
        {
            DirectorData[] directors = null;
            int directorCount = _directorService.TotalCount();

            RunAndReleaseProxy<IDirectorService>(_proxyFactory.CreateProxy<IDirectorService>(), proxy =>
            {
                //directors = proxy.GetByPage(request.Page, request.PageSize);
                
                directorCount = proxy.TotalCount();
            });

            return Json(new DataSourceResult
            {
                Data = directors,
                Total = directorCount
            });
        }

        //public ActionResult Director_Read([DataSourceRequest]DataSourceRequest request)
        //{
        //    Director[] directors = null;
        //    int directorCount = _directorService.GetAll().Count(); //////////////////////////////

        //    RunAndReleaseProxy<IDirectorService>(_proxyFactory.CreateProxy<IDirectorService>(), proxy =>
        //    {
        //        directors = proxy.GetByPage(request.Page, request.PageSize);
        //        directorCount = proxy.TotalCount();
        //    });

        //    return Json(new DataSourceResult
        //    {
        //        Data = directors,
        //        Total = directorCount
        //    });
        //}

        public ActionResult Director_Read([DataSourceRequest]DataSourceRequest request, string searchTerm)
        {
            if (searchTerm.IsNullOrWhiteSpace())
            {
                Director[] directors = _directorService.GetByPage(request.Page, request.PageSize);
                int movieCount = _directorService.TotalCount();

                return Json(new DataSourceResult
                {
                    Data = directors,
                    Total = movieCount
                });
            }
            else
            {
                Director[] directors = _directorService.FindByName(searchTerm).Select(data => new Director(){FullName = data.Name, Id = data.Id }).ToArray();
                int movieCount = directors.Count();

                return Json(new DataSourceResult
                {
                    Data = directors,
                    Total = movieCount
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Director_Update([DataSourceRequest] DataSourceRequest request, Director model)
        {
            //throw new Exception("selam");
            model.Validate();

            if (!model.ValidationErrors.Any())
            {
                _directorService.Update(model);
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                // NOTFINISHED
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Director_Delete([DataSourceRequest] DataSourceRequest request, Director model)
        {
            _directorService.Delete(model.Id);
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        public JsonResult GetCascadeCountries()
        {
            Country[] countries = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<ICountryService>(), proxy => { countries = proxy.GetAll(); });

            return Json(countries.Select(c => new {Name = c.Name, Id = c.Id }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeStates(int? country)
        {
            State[] countryStates = null;

            if (country != null)
            {
                RunAndReleaseProxy(_proxyFactory.CreateProxy<IStateService>(), proxy =>
                {
                    countryStates = proxy.GetStatesByCountry((int)country);
                });
            }
            return Json(countryStates.Select(st => new { Name = st.Name, Id = st.Id }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadeCounties(int? state)
        {
            County[] stateCounties = null;

            if (state != null)
            {
                RunAndReleaseProxy(_proxyFactory.CreateProxy<ICountyService>(), proxy => { stateCounties = proxy.GetCountiesByState((int)state); });
            }
            return Json(stateCounties.Select(c => new { Name = c.Name, Id = c.Id }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Director_Search(string text)
        {
            if (text.IsNullOrWhiteSpace())
                return null;

            DirectorData[] directorDatas = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IDirectorService>(), proxy =>
            {
                directorDatas = proxy.FindByName(text);
            });

            return Json(directorDatas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string Statistics_TopXDirectors(int top)
        {
            if (top > 50)
                top = 10;

            var jsonData = _directorService.Statistics_TopXDirectors(top);

            return jsonData;
        }
    }
}