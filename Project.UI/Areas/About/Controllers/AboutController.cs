using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.About.Controllers
{
    [Area("About")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
