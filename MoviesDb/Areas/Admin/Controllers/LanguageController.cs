using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Core.Common.Contracts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MoviesDb.Areas.Admin.Constants;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LanguageController : ViewControllerBase
    {
        public ILanguageService _languageService;
        public IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public LanguageController(ILanguageService languageService, IProxyFactory proxyFactory)
        {
            _languageService = languageService;
            _proxyFactory = proxyFactory;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            // base.RegisterServices(disposableServices); // Ne işe yaradığını unuttum. Tekrar öğrenilecek.
            disposableServices.Add(_languageService);
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
        public ActionResult Create(Language language)
        {
            language.Validate();
            
            if (!language.ValidationErrors.Any())
            {
                RunAndReleaseProxy(_proxyFactory.CreateProxy<ILanguageService>(), proxy => proxy.Update(language));

                TempData["Status"] = CrudNotification.Success;
                TempData["Id"] = language.Id;

                return View();
            }
            else
            {
                ViewBag.ValidationErrors = language.ValidationErrors.ToList();
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
                var language = new Language();
                language = _languageService.Get((int)id);

                return View(language);
            }
        }

        [HttpPost]
        public ActionResult Edit(Language languageUpdated)
        {
            languageUpdated.Validate();

            if (!languageUpdated.ValidationErrors.Any())
            {
                _languageService.Update(languageUpdated);
                return View();
            }
            else
            {
                ViewBag.ValidationErrors = languageUpdated.ValidationErrors.ToList();
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
            _languageService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Language_Read([DataSourceRequest]DataSourceRequest request)
        {
            Language[] languages = _languageService.GetByPage(request.Page, request.PageSize);
            int languageCount = _languageService.TotalCount();

            return Json(new DataSourceResult
            {
                Data = languages,
                Total = languageCount
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Language_Update([DataSourceRequest] DataSourceRequest request, Language model)
        {
            //throw new Exception("selam");
            model.Validate();

            if (!model.ValidationErrors.Any())
            {
                _languageService.Update(model);
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                // NOTFINISHED
                return Json(new[] { model }.ToDataSourceResult(request, ModelState));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Genre_Delete([DataSourceRequest] DataSourceRequest request, Language model)
        {
            _languageService.Delete(model.Id);
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}