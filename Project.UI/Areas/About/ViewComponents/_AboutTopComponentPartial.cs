using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.About.ViewComponents
{
    public class _AboutTopComponentPartial : ViewComponent
    {
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
