using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.CommentDtos;
using Project.UI.Models;

namespace Project.UI.Areas.Blog.ViewComponents
{
	public class _SingleBlogCommentsComponentPartial : ViewComponent
	{
		readonly IHttpClientFactory _httpClientFactory;
		readonly ApiSettings _apiSettings;

		public _SingleBlogCommentsComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
		{
			_httpClientFactory = httpClientFactory;
			_apiSettings = apiSettings.Value;
		}

		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			id = ViewBag.BlogId;

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
			var responseMessage = await client.GetAsync("Comment/GetCommentsByBlog/" + id);

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
				return View(values);
			}

			return View();
		}
	}
}
