using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.BlogDtos;
using Project.Shared.DTOs.CommentDtos;
using Project.UI.Models;
using System.Text;

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


		public async Task<IActionResult> InsertComment(InsertCommentDto insertCommentDto)
		{
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
			var jsonData = JsonConvert.SerializeObject(insertCommentDto);
			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("Comment/InsertComment", content);

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("SingleBlog", new { id = insertCommentDto.BlogId });
			}

			return View();
		}
	}
}
