using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Vehicles.ViewComponents
{
	public class _VehicleTabSectionComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
