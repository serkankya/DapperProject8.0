using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Blog.ViewComponents
{
	public class _SingleBlogAuthorComponentPartial : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
