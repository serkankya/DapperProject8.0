using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.BlogDtos;
using Project.UI.Models;

namespace Project.UI.Areas.Blog.ViewComponents
{
	public class _SingleBlogDetailComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
