using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Core.Common.Contracts;

namespace MoviesDb.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JqueryAjaxPostFunctionController : Controller
    {
        private IProxyFactory _proxyFactory;

        [ImportingConstructor]
        public JqueryAjaxPostFunctionController(IProxyFactory proxyFactory)
        {
            _proxyFactory = proxyFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string data)
        {
            // sayfadan gelen bilgileri .net objesine deserialize ediyorum
            CountyEntity county = new JavaScriptSerializer().Deserialize<CountyEntity>(data);

            // ...

            return Json(county.State + " " + county.County + " OK!");
        }
    }

    class CountyEntity
    {
        // HTML tarafındaki JSON objesinde tanımlanan değişkenler ile aynı isimde olmalı.
        public string State { get; set; }
        public string County { get; set; }
    }
}