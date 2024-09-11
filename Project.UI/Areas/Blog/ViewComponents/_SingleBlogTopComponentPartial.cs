using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Blog.ViewComponents
{
    public class _SingleBlogTopComponentPartial : ViewComponent
    {
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
