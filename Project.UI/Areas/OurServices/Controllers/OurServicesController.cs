using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.OurServices.Controllers
{
    [Area("OurServices")]
    public class OurServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
