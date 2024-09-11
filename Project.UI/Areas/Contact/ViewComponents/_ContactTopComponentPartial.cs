using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Contact.ViewComponents
{
    public class _ContactTopComponentPartial : ViewComponent
    {
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
