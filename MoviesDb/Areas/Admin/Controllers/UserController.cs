using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Core.Common.Contracts;
using Kendo.Mvc.UI;
using MoviesDb.Areas.Admin.Constants;
using MoviesDB.Client.Contracts;
using MoviesDB.Client.Entities;
using MoviesDB.Client.Entities.DTOs;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserController : ViewControllerBase
    {
        [Import]
        IUserService _userService;

        [Import] 
        private IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_userService);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            Role[] roles = null;

            RunAndReleaseProxy(_proxyFactory.CreateProxy<IRoleService>(),
                proxy =>
                {
                    roles = proxy.GetAll();
                });

            List<SelectListItem> rolesList = roles.Select(role => new SelectListItem()
                {
                    Text = role.Name, 
                    Value = role.Id.ToString()
                }).ToList();

            ViewBag.Roles = rolesList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(User incomingUser)
        {
            incomingUser.Validate();

            if (!incomingUser.ValidationErrors.Any())
            {
                var b = 1;
                TempData["Status"] = CrudNotification.Success;
                return View("Create");
            }
            else
            {
                ViewBag.ValidationErrors = incomingUser.ValidationErrors.ToList();
                TempData["Status"] = CrudNotification.ValidationError;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");

            User user = _userService.Get(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User updatedUser)
        {
            updatedUser.Validate();

            if (!updatedUser.ValidationErrors.Any())
            {
                //var updatedUser = _userService.UpdateSec(updatedMovie);
                return View(updatedUser);
            }
            else
            {
                ViewBag.ValidationErrors = updatedUser.ValidationErrors.ToList();
                TempData["Status"] = CrudNotification.ValidationError;
                return View(updatedUser);
            }
        }

        [HttpGet]
        public ActionResult Statistics(int id)
        {
            if (id == 0)
                return View("Index");

            return View(id);
        }

        public ActionResult User_Read([DataSourceRequest]DataSourceRequest request, string searchTerm)
        {
            if (searchTerm.IsEmpty())
            {
                User[] users = _userService.GetByPage(request.Page, request.PageSize);
                int userCount = _userService.TotalCount();

                return Json(new DataSourceResult
                {
                    Data = users,
                    Total = userCount
                });
            }
            else
            {
                return null;
                //MovieData[] movies = _userService.SearchMovie(searchTerm);
                //int movieCount = movies.Count();

                //return Json(new DataSourceResult
                //{
                //    Data = movies,
                //    Total = movieCount
                //});
            }
        }

        [HttpGet]
        public string Statistics_RatedMoviesByGenre(int id)
        {
            if (id == 0) 
                throw new Exception("Log-in error!");

            var jsonString = _userService.Statistics_RatedMoviesByGenre(id);
            return jsonString;
        }

        [HttpGet]
        public string Statistics_RatedMoviesByYear(int id)
        {
            if (id == 0) 
                throw new Exception("Log-in error!");

            var jsonString = _userService.Statistics_RatedMoviesByYear(id);

            return jsonString;
        }

        [HttpGet]
        public string Statistics_RatingDistrubition(int id)
        {
            if (id == 0) 
                throw new Exception("Log-in error!");

            var jsonString = _userService.Statistics_RatingDistrubition(id);
            return jsonString;
        }

        [HttpGet]
        public string Statistics_Top10Directors(int id)
        {
            if (id == 0)
                throw new Exception("Log-in error!");

            var jsonStr = _userService.Statistics_Top10Directors(id);
            return jsonStr;
        }

        [HttpGet]
        public string Statistics_Top10Actors(int id)
        {
            if (id == 0)
                throw new Exception("Log-in error!");

            var jsonString = _userService.Statistics_Top10Actors(id);

            return jsonString;
        }
    }
}