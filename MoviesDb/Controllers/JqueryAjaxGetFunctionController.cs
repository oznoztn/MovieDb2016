using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using Core.Common.Contracts;
using MoviesDB.Client.Contracts;

namespace MoviesDb.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JqueryAjaxGetFunctionController : Controller
    {
        private IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public JqueryAjaxGetFunctionController(IProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        public string First()
        {
            return "Greetings from MVC Controller!";
        }

        public string Second(string name, string lastName)
        {
            return (string.Format("Hi, {0} {1}!", name, lastName));
        }

        public JsonResult ListCountries()
        {
            var countries = _proxyFactory.CreateProxy<ICountryService>().GetAll();

            // projection
            return Json(countries.Select(c => new CountryJSON()
            {
                Id = c.Id, Name = c.Name
            }), JsonRequestBehavior.AllowGet);
        }
    }

    public class CountryJSON
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}