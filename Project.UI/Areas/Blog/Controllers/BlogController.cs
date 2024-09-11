using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.CommentDtos;
using Project.UI.Models;
using Project.UI.Tools.FluentValidation;
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

		public IActionResult Index(string keyWord)
		{
			ViewBag.KeyWord = keyWord;
			return View();
		}

		[HttpGet("SingleBlog/{id}user{userId}")]
		public IActionResult SingleBlog(int id, int userId)
		{
			ViewBag.BlogId = id;
			ViewBag.UserId = userId;
			return View();
		}


		public async Task<IActionResult> InsertComment(InsertCommentDto insertCommentDto)
		{
			var validator = new InsertCommentValidator();
			var result = validator.Validate(insertCommentDto);

			if (!result.IsValid)
			{
				TempData["ErrorComment"] = result.Errors.Select(e => e.ErrorMessage).ToList();
				return RedirectToAction("SingleBlog", new { id = insertCommentDto.BlogId, userId = insertCommentDto.UserId });
			}

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
			var jsonData = JsonConvert.SerializeObject(insertCommentDto);
			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("Comment/InsertComment", content);

			if (responseMessage.IsSuccessStatusCode)
			{
				TempData["SuccessComment"] = "Yorumunuz başarıyla paylaşıldı.";
				return RedirectToAction("SingleBlog", new { id = insertCommentDto.BlogId, userId = insertCommentDto.UserId });
			}

			return View();
		}
	}
}
