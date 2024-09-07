using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Project.Shared.DTOs.BlogDtos;
using Project.UI.Models;

namespace Project.UI.Areas.Blog.ViewComponents
{
	public class _SingleBlogContentComponentPartial : ViewComponent
	{
		readonly IHttpClientFactory _httpClientFactory;
		readonly ApiSettings _apiSettings;

		public _SingleBlogContentComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
		{
			_httpClientFactory = httpClientFactory;
			_apiSettings = apiSettings.Value;
		}

		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			id = ViewBag.BlogId;

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_apiSettings.BaseHostUrl!);
			var responseMessage = await client.GetAsync("Blog/GetBlog/" + id);

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<ResultBlogDto>(jsonData);
				return View(values);
			}

			return View();
		}
	}
}
