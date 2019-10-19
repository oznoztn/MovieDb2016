using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Common.Contracts;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NewsController : ViewControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public NewsController(INewsService newsService, IProxyFactory proxyFactory)
        {            
            _newsService = newsService;
            _proxyFactory = proxyFactory;
        }

        [HttpGet]
        public ActionResult Index()
        {
            RunAndReleaseProxy(_proxyFactory.CreateProxy<INewsCategoryService>(), proxy =>
            {
                proxy.GetAll();
            });

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsCreationData incomingNews)
        {
            
            return View();
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
                return null;
            }
        }

        [HttpPost]
        public ActionResult Edit(NewsCreationData updatedNews)
        {
            return null;
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

        [HttpPost]
        public ActionResult Delete(int newsId)
        {
            return View();
        }
    }
}