using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.BlogDtos;
using Project.UI.Models;

namespace Project.UI.Areas.Blog.Controllers
{
	[Area("Blog")]
	public class BlogController : Controller
	{
		readonly IHttpClientFactory _httpClientFactory;
		readonly ApiSettings _apiSettings;

		public BlogController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
		{
			_httpClientFactory = httpClientFactory;
			_apiSettings = apiSettings.Value;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("SingleBlog/{id}")]
		public IActionResult SingleBlog(int id)
		{
			ViewBag.BlogId = id;	
			return View();
		}
	}
}
