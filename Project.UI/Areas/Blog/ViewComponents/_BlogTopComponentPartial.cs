using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Blog.ViewComponents
{
    public class _BlogTopComponentPartial : ViewComponent
    {
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
