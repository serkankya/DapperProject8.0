using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.OurServices.ViewComponents
{
	public class _OurServicesTopComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
