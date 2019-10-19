using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Core.Common.Contracts;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MoviesDb;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CountryController : ViewControllerBase
    {
        ICountryService _countryService;
        IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public CountryController(ICountryService countryService, IProxyFactory proxyFactory)
        {
            _countryService = countryService;
            _proxyFactory = proxyFactory;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_countryService);
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
        public ActionResult Create(Country country)
        {

            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Country countryUpdated)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Statistics()
        {
            ViewBag.jsonStats = _countryService.Statistics();

            return View();
        }

        public ActionResult Country_Read([DataSourceRequest]DataSourceRequest request, string searchTerm)
        {
            if (searchTerm.IsEmpty())
            {
                Country[] countries = _countryService.GetByPage(request.Page, request.PageSize);
                int countryCount = _countryService.TotalCount();

                return Json(new DataSourceResult
                {
                    Data = countries,
                    Total = countryCount
                });
            }
            else
            {
                Country[] countries = _countryService.SearchCountry(searchTerm);
                int countryCount = countries.Count();

                return Json(new DataSourceResult
                {
                    Data = countries,
                    Total = countryCount
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Country_Update([DataSourceRequest] DataSourceRequest request, Country model)
        {
            model.Validate();

            if (!model.ValidationErrors.Any())
            {
                _countryService.Update(model);
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                // NOTFINISHED
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }
    }
}