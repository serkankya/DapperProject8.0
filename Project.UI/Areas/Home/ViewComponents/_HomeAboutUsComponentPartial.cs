using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Shared.DTOs.AboutUsDtos;

namespace Project.UI.Areas.Home.ViewComponents
{
	public class _HomeAboutUsComponentPartial : ViewComponent
	{
		readonly IHttpClientFactory _httpClientFactory;

		public _HomeAboutUsComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:44340/api/AboutUs/GetAboutUs");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<ResultAboutUsDto>(jsonData);
				return View(values);
			}

			return View();
		}
	}
}
