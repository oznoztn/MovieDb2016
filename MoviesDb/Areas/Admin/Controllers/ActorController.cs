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
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ActorController : ViewControllerBase
    {
        public IActorService _actorService;
        public IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public ActorController(IActorService actorService, IProxyFactory proxyFactory)
        {
            _actorService = actorService;
            _proxyFactory = proxyFactory;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_actorService);
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
        public ActionResult Create(ActorCreationData actor)
        {
            actor.Validate();
            
            if (!actor.ValidationErrors.Any())
            {
                RunAndReleaseProxy(_proxyFactory.CreateProxy<IActorService>(), proxy => proxy.Add(actor));

                TempData["Status"] = CrudNotification.Success;
                TempData["Id"] = actor.Id;

                return View();
            }
            else
            {
                ViewBag.ValidationErrors = actor.ValidationErrors.ToList();
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
                ActorCreationData actor = _actorService.GetSec((int) id);
                return View(actor);
            }
        }

        [HttpPost]
        public ActionResult Edit(ActorCreationData actorUpdated)
        {
            actorUpdated.Validate();

            if (!actorUpdated.ValidationErrors.Any())
            {
                _actorService.UpdateSec(actorUpdated);
                return View(actorUpdated);
            }
            else
            {
                ViewBag.ValidationErrors = actorUpdated.ValidationErrors.ToList();
                TempData["Status"] = CrudNotification.ValidationError;
                return View(actorUpdated);
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

        [HttpGet]
        public ActionResult Details(int id)
        {
            // film yoksa yani dönen film null ise başka bir sayfaya yönlendirilebilir
            var actorDetails = _actorService.GetDetails(id);

            if (actorDetails.Id == 0)
                return Content("There's no movie for given Id");
            
            return View(actorDetails);
        }

        public ActionResult Delete(int id)
        {
            _actorService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Actor_Read([DataSourceRequest]DataSourceRequest request, string searchTerm)
        {
            if (searchTerm.IsNullOrWhiteSpace())
            {
                Actor[] actors = _actorService.GetByPage(request.Page, request.PageSize);
                int movieCount = _actorService.TotalCount();

                return Json(new DataSourceResult
                {
                    Data = actors,
                    Total = movieCount
                });
            }
            else
            {
                Actor[] actors = _actorService.FindByName(searchTerm).Select(data => new Actor(){FullName =  data.Name, Id = data.Id}).ToArray();
                int movieCount = actors.Count();

                return Json(new DataSourceResult
                {
                    Data = actors,
                    Total = movieCount
                });
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Actor_Update([DataSourceRequest] DataSourceRequest request, Actor model)
        {
            //throw new Exception("selam");
            model.Validate();

            if (!model.ValidationErrors.Any())
            {
                _actorService.Update(model);
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                // NOTFINISHED
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Actor_Delete([DataSourceRequest] DataSourceRequest request, Actor model)
        {
            _actorService.Delete(model.Id);
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult GetCascadeCountries()
        {
            Country[] countries = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<ICountryService>(), proxy => { countries = proxy.GetAll(); });

            return Json(countries.Select(c => new { Name = c.Name, Id = c.Id }), JsonRequestBehavior.AllowGet);
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

        public ActionResult Actor_Search(string text)
        {
            if (text.IsNullOrWhiteSpace())
                return null;

            ActorData[] actorDatas = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IActorService>(), proxy =>
            {
                actorDatas = proxy.FindByName(text);
            });

            return Json(actorDatas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string Statistics_TopXActors(int top)
        {
            if (top > 50)
                top = 10;

            var jsonData = _actorService.Statistics_TopXActors(top);

            return jsonData;
        }

	}
}