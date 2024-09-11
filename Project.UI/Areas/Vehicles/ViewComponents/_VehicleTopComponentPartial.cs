
using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Vehicles.ViewComponents
{
	public class _VehicleTopComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
