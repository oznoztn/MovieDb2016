using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesDb.Areas.Admin.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : ViewControllerBase
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