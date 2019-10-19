using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesDB.Client.Contracts;

namespace MoviesDb.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Controller
    {
        [ImportingConstructor]
        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }
	}
}