using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Vehicles.ViewComponents
{
	public class _VehicleDetailsTopComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
