using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Blog.ViewComponents
{
	public class _SingleBlogSearchComponentPartial : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
